namespace platform.spotred.room
{
    public class DealCardsOperate: BaseOperate
    {
        public DealCardsOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui,isRelay)
        {
            
        }

        public override long getWaittime()
        {
            return 1000;
        }
    }
}
