namespace platform.spotred.room
{
    public class RecvMatchStartProcess:Process
    {
        MatchStartOperate operate;
        public RecvMatchStartProcess(MatchStartOperate operate)
        {
            this.operate = operate;
        }

        public override void execute()
        {
            operate.isOver = true;
        }
    }
}
