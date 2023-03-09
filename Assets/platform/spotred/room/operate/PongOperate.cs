using cambrian.common;

namespace platform.spotred.room
{
    public class PongOperate:BaseOperate
    {
        public int selfIndex;

      

        public int card;

        public int destindex;

        public int count;
        public PongOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui,isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
            destindex = data.readInt(); //碰哪个人的牌
            card = data.readInt();
            count = data.readInt(); //从手牌中拿出的数量
        }

        public override long getWaittime()
        {
            return base.getWaittime();
        }
    }
}
