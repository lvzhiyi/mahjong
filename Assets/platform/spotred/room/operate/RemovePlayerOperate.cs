namespace platform.spotred.room
{
    public class RemovePlayerOperate:BaseOperate
    {

        public long userid;

        public RemovePlayerOperate(long userid,int type = RecvCPMatchMsg.MSG_REMOVE_PLAYER, int step = 0, int[] operates = null, int selfIndex = 0, int stage = 0, int round = 0, int paidui = 0, bool isRelay = false) : base(type, step, operates, stage, round, paidui, isRelay)
        {
            this.userid = userid;
        }


        public override long getWaittime()
        {
            return 0;
        }
    }
}
