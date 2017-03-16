using Rocket.API;

namespace KillForXP
{
    public class Configuration : IRocketPluginConfiguration
    {
        public uint Skull_XP, Arm_XP, Leg_XP, Body_XP, Foot_XP, Spine_XP, Front_XP, Back_XP;
        public bool ConsoleMsgEnabled, PlayerMsgEnabled;
        public float plrMsg_r, plrMsg_g, plrMsg_b;
        public string consoleMsgColor;
        public void LoadDefaults()
        {
            Skull_XP = 75;
            Arm_XP = 30;
            Leg_XP = 20;
            Body_XP = 50;
            Foot_XP = 10;
            Spine_XP = 40;
            Front_XP = 20;
            Back_XP = 20;
            ConsoleMsgEnabled = true;
            PlayerMsgEnabled = true;
            plrMsg_r = 255;
            plrMsg_g = 0;
            plrMsg_b = 0;
            consoleMsgColor = "red";
        }
    }
}