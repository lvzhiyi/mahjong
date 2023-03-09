using cambrian.common;

namespace platform.spotred.room
{
    public class HuOperate:BaseOperate
    {
        public int selfIndex;

        public int[] indexs;

        public int hu_card;
        public HuOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui,isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            indexs = new int[len];
            for (int i = 0; i < indexs.Length; i++)
            {
                indexs[i] = data.readInt();
            }
            hu_card = data.readInt();
        }

        public override long getWaittime()
        {
            return 300;
        }
    }
}
