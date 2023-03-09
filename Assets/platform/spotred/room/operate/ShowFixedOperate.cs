using cambrian.common;
using platform.spotred;

namespace platform.spotred.room
{
    public class ShowFixedOperate:BaseOperate
    {
        public int selfIndex;

        public FixedCards[][] cards;

        public int[] disableCard;
        public ShowFixedOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui,isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {

            cards = new FixedCards[data.readInt()][]; //四家偷的牌
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = new FixedCards[data.readInt()];
                for (int j = 0; j < cards[i].Length; j++)
                {
                    FixedCards c = new FixedCards();
                    c.bytesRead(data);
                    cards[i][j] = c;
                }
            }

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
