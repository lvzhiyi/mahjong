namespace platform.mahjong
{
    public class AYMJMatchDelayProcess: Process
    {
        MJMatchDelayOperate operate;

        int selfOperate;

        public AYMJMatchDelayProcess(MJBaseOperate operate)
        {
            this.operate = (MJMatchDelayOperate)operate;
        }

        public override void execute()
        {
            operate.playOver();
        }
    }
}
