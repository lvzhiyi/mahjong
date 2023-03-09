using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 听牌信息
    /// </summary>
    public class TingCardsInfo:BytesSerializable
    {
        /// <summary>
        /// 普通0，最多胡牌，最大番数
        /// </summary>
        public static int NORMAL = 0, MAX_COUNT = 1, MAX_POINT = 2;
        /// <summary>
        /// 指定的牌
        /// </summary>
        public int card { get; set; }
        /// <summary>
        /// 听牌数量
        /// </summary>
        public int count;
        /// <summary>
        /// 最大番数
        /// </summary>
        public int maxpoint;
        /// <summary>
        /// 所听的牌
        /// </summary>
        protected ArrayList<TingCard> cards;
        /// <summary>
        /// 类型
        /// </summary>
        public int type;


        public TingCardsInfo()
        {
            this.cards = new ArrayList<TingCard>();
            type = NORMAL;
        }

        public void setTingCards(TingCard card)
        {
            this.cards.add(card);
            maxpoint = card.getMultiple() > maxpoint ? card.getMultiple() : maxpoint;
        }

        /// <summary>
        /// 增加听牌数量
        /// </summary>
        public void addCount(int count)
        {
            this.count += count;
        }

        public void resetCount()
        {
            this.count = 0;
        }

        public void setMaxPointType()
        {
            type = MAX_POINT;
        }

        public void setMaxCountType()
        {
            type = MAX_COUNT;
        }

        public ArrayList<TingCard> getTingList()
        {
            return cards;
        }

        public void removePointNotEnough(int point)
        {
            if (cards == null) return;
            for (int i = cards.count-1; i >=0; i--)
            {
                if (cards.get(i).fan < point)
                    cards.removeAt(i);
            }
        }
    }
}
