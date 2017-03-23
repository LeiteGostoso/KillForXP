using System;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Rocket.Unturned.Events;
using SDG.Unturned;
using Rocket.API.Collections;
using UnityEngine;

namespace KillForXP
{
    public class Plugin : RocketPlugin<Configuration>
    {
        private DateTime lastCalled = DateTime.Now;

        protected override void Load()
        {
            Rocket.Core.Logging.Logger.Log("Kill for XP loaded successfully\n(I hope)");
            UnturnedPlayerEvents ev = new UnturnedPlayerEvents();
            ev.OnDeath += ev_OnDeath;
        }

        public void FixedUpdate()
        {
        }
        public static Plugin Instance;
        void ev_OnDeath(UnturnedPlayer player, SDG.Unturned.EDeathCause cause, SDG.Unturned.ELimb limb, Steamworks.CSteamID murderer)
        {
            UnturnedPlayer killer = UnturnedPlayer.FromCSteamID(murderer);
            if (limb == ELimb.SKULL)
                GiveXP(Instance.Configuration.Instance.Skull_XP, killer, player, Translate("skull_name"));
            if (limb == ELimb.LEFT_ARM || limb == ELimb.RIGHT_ARM)
                GiveXP(Instance.Configuration.Instance.Arm_XP, killer, player, Translate("arm_name"));
            if (limb == ELimb.LEFT_FOOT || limb == ELimb.RIGHT_FOOT)
                GiveXP(Instance.Configuration.Instance.Foot_XP, killer, player, Translate("foot_name"));
            if (limb == ELimb.RIGHT_FRONT || limb == ELimb.LEFT_FRONT)
                GiveXP(Instance.Configuration.Instance.Front_XP, killer, player, Translate("front_name"));
            if (limb == ELimb.SPINE)
                GiveXP(Instance.Configuration.Instance.Spine_XP, killer, player, Translate("spine_name"));
            if (limb == ELimb.LEFT_LEG || limb == ELimb.RIGHT_LEG)
                GiveXP(Instance.Configuration.Instance.Leg_XP, killer, player, Translate("leg_name"));
            if (limb == ELimb.LEFT_BACK || limb == ELimb.RIGHT_BACK)
                GiveXP(Instance.Configuration.Instance.Back_XP, killer, player, Translate("back_name"));
        }

        void GiveXP(uint amount, UnturnedPlayer player, UnturnedPlayer killed, string limbName)
        {
            player.Experience += amount;
            Color c = new Color();
            c.r = Instance.Configuration.Instance.plrMsg_r;
            c.g = Instance.Configuration.Instance.plrMsg_g;
            c.b = Instance.Configuration.Instance.plrMsg_b;
            UnturnedChat.Say(player, Translate("msg_playermsg", amount, killed, limbName), c);
            ConsoleColor c2 = ConsoleColor.White;
            try
            {
                c2 = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), Instance.Configuration.Instance.consoleMsgColor, true);
                Rocket.Core.Logging.Logger.Log(Translate("msg_consolemsg"), c2);
            }
            catch
            {
                Rocket.Core.Logging.Logger.Log("WRONG COLOR SYNTAX FOR CONSOLE IN CONFIGURATIONS");
                Rocket.Core.Logging.Logger.Log(Translate("msg_consolemsg"), ConsoleColor.Red);
            }
        }

        public new TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList(){
                    {"msg_playermsg","You received {0} experience by killing {1} in the {2}."},
                    {"msg_consolemsg","{0} received {1} experience by killing {2} in the {3}."},
                    {"spine_name", "spine"},
                    {"arm_name", "arm"},
                    {"leg_name", "leg"},
                    {"back_name", "back"},
                    {"front_name", "front"},
                    {"foot_name", "foot"},
                    {"skull_name", "skull"}
                };
            }
        }
    }
}
