using cambrian.common;
using platform.spotred;

namespace platform.poker
{ 
    public class DDZPlayerCards : PlayerCards
    {
        public DDZPlayerCards()
        {
            handcards = new ArrayList<int>();
            count = new SpotRedCount();
        }

        public override ArrayList<int> delHandCards(int[] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = handcards.count - 1; j >= 0; j--)
                {
                    if (handcards.get(j) == Poker.INVISIBLE || cards[i] == handcards.get(j))
                    {
                        handcards.removeAt(j);
                        break;
                    }
                }
            }
            return handcards;
        }

        /// <summary> 手牌 从大到小 </summary>
        public override int[] getSortHandCards()
        {
            return CardSort.LTSCards(handcards.toArray());
        }

        /// <summary> 增加固定牌 </summary>
        public override void addFixCard(int type,int[] card)
        {
            FixedCards cards = new FixedCards(type,card);
            fixCards.add(cards);
        }

        /// <summary> 获取明牌状态 </summary>
        public bool getMingPaiStatus()
        {
            return hasStatus(PlayerStatus.MINGPAI);
        }

        /// <summary> 获取全部相同的牌 </summary>
        public override FixedCards getSameFixedCards(int card,int count)
        {
            for (int i = 0; i < fixCards.count; i++)
            {
                FixedCards fixs = fixCards.get(i);
                if (fixs.isSameCard(card,count)) return fixs;
            }
            return null;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            for (int i = 0; i < len; i++)
            {
                handcards.add(data.readInt());
            }
        }

        public override PlayerCards clone(PlayerCards pc)
        {
            var cards = new DDZPlayerCards();
            cards.count = pc.count;
            cards.data = pc.data;
            cards.disableCards = pc.disableCards;
            cards.handcards = pc.handcards;
            cards.status = pc.status;
            cards.point = pc.point;
            return cards;
        }
    }
}
