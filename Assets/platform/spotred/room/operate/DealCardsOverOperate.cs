using cambrian.common;

namespace platform.spotred.room
{
    public class DealCardsOverOperate:BaseOperate
    {
        public int[][] cards;

        public int[] disablecard;

        public int selfIndex;

        public DealCardsOverOperate(int type, int step, int[] operates,int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui,isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            cards = new int[data.readInt()][]; //每个人的牌
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = new int[data.readInt()];
                for (int j = 0; j < cards[i].Length; j++)
                {
                    cards[i][j] = data.readInt();
                }
            }

            disablecard = new int[0];
            if (!isRelay&&StatusKit.hasStatus(operates[selfIndex], OperateView.CAN_DISCARD)) //出牌
            {
                int len = data.readInt();
                disablecard = new int[len];
                for (int i = 0; i < len; i++)
                {
                    disablecard[i] = data.readInt();
                }
            }
        }

        public override long getWaittime()
        {
            return 1000;
        }
    }
}
