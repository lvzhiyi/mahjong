using cambrian.common;

namespace platform.spotred.room
{
    public class ChowOperate:BaseOperate
    {
        public int selfIndex;
        public int destindex; //吃哪个人的牌
        public int card; //用哪张牌吃
        public int destCard; //吃的哪张牌


        public int[] disableCard; //自己不能出的牌
        public ChowOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui,isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
            destindex = data.readInt(); //吃哪个人的牌
            card = data.readInt(); //用哪张牌吃
            destCard = data.readInt(); //吃的哪张牌
            disableCard = new int[0]; //自己不能出的牌
            if (!isRelay&&StatusKit.hasStatus(operates[selfIndex], OperateView.CAN_DISCARD)) //出牌
            {
                int len = data.readInt();
                disableCard = new int[len];
                for (int i = 0; i < len; i++)
                {
                    disableCard[i] = data.readInt();
                }
            }
        }

        public override long getWaittime()
        {
            return base.getWaittime();
        }
    }
}
