using cambrian.common;

namespace platform.spotred.room
{
    /// <summary>
    /// 报牌操作
    /// </summary>
    public class BaopaiOperate:BaseOperate
    {
        public int selfIndex;

        public int[] baocards;
        public BaopaiOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui,bool isRelay=false) : base(type, step, operates, stage, round, paidui, isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
            int len = data.readInt();
            baocards = new int[len];
            for (int i = 0; i < len; i++)
            {
                baocards[i] = data.readInt();
            }
        }

        public override long getWaittime()
        {
            return base.getWaittime();
        }
    }
}
