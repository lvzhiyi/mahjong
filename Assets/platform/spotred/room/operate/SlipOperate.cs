using cambrian.common;
using platform.spotred;

namespace platform.spotred.room
{
    public class SlipOperate:BaseOperate
    {
        public int selfIndex;

        public FixedCards[] cards;

        public int[] draws;

        public int card;

        public int[] disableCard;
        public SlipOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui,isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {

            index = data.readInt();
            cards = new FixedCards[data.readInt()];
            for (int i = 0; i < cards.Length; i++)
            {

                FixedCards card = new FixedCards();
                card.bytesRead(data);
                cards[i] = card;
            }
            draws = new int[data.readInt()]; //剩余的手牌
            for (int i = 0; i < draws.Length; i++)
            {
                draws[i] = data.readInt();
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
