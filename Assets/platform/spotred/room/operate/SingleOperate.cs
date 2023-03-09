using cambrian.common;

namespace platform.spotred.room
{
    public class SingleOperate:BaseOperate
    {
        public int selfIndex;

        public int card;

        public bool ishandcard;

        public SingleOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui,isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
            card = data.readInt();
            ishandcard = data.readBoolean();
        }

        public override long getWaittime()
        {
            return base.getWaittime();
        }
    }
}
