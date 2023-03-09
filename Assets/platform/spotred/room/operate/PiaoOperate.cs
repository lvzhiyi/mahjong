using cambrian.common;

namespace platform.spotred.room
{
    /// <summary>
    /// 接收飘操作
    /// </summary>
    public class PiaoOperate:BaseOperate
    {
        public int selfIndex;
        public PiaoOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui, isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();//某人飘
        }
    }
}
