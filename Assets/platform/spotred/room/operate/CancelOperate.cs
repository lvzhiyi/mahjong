using cambrian.common;

namespace platform.spotred.room
{
    public class CancelOperate: BaseOperate
    {
        public int selfIndex;
        public CancelOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui, isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            
        }

        public override long getWaittime()
        {
            return base.getWaittime();
        }

    }
}
