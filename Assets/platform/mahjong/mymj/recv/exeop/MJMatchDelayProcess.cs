namespace platform.mahjong
{
    public class MJMatchDelayProcess: Process
    {
        MJMatchDelayOperate operate;

        int selfOperate;

        public MJMatchDelayProcess(MJBaseOperate operate)
        {
            this.operate = (MJMatchDelayOperate)operate;
        }

        public override void execute()
        {
            operate.playOver();
        }
    }
}
