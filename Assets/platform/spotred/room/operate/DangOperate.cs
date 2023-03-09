using cambrian.common;

namespace platform.spotred.room
{
    /// <summary>
    /// 挡
    /// </summary>
    public class DangOperate : BaseOperate
    {
        public int selfIndex;

       
        public DangOperate(int type, int step, int[] operates,int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui,isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            index=data.readInt();
        }

        public override long getWaittime()
        {
            return base.getWaittime();
        }
    }
}
