namespace platform.mahjong
{
    public class AYMJMatchDelayRecording : Process
    {
        MJMatchDelayOperate operate;

        int selfOperate;

        public AYMJMatchDelayRecording(MJBaseOperate operate)
        {
            this.operate = (MJMatchDelayOperate)operate;
        }

        public override void execute()
        {
            operate.playOver();
        }
    }
}
