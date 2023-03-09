namespace platform.mahjong
{
    public class AYMJMatchKickPlayerRecording : Process
    {
        MJKickPlayerOperate operate;

        public AYMJMatchKickPlayerRecording(MJBaseOperate operate)
        {
            this.operate = (MJKickPlayerOperate)operate;
        }

        public override void execute()
        {
            operate.playOver();
        }
    }
}
