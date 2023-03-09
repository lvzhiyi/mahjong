using cambrian.common;

namespace platform.poker
{
    public class CardRecorder : BytesSerializable
    {
        private int[] cardCounts;

        private bool[] mingPai;

        public CardRecorder() { reset(); }

        public void incrCount(int card)
        {
            int index = Poker.getCountIndex(card);
            cardCounts[index] += 1;
        }

        public void incrCount(int[] cards)
        {
            if (cards == null || cards.Length == 0) return;
            for (int i = 0, j = 0; i < cards.Length; i++)
            {
                j = Poker.getCountIndex(cards[i]);
                cardCounts[j] += 1;
            }
        }

        public void addMPUserIndex(int index)
        {
            mingPai[index] = true;
        }

        public void recorderClear()
        {
            for (int i = 0; i < cardCounts.Length; i++)
            {
                if (i == 0) cardCounts[i] = 1;
                else if (i == cardCounts.Length - 1) cardCounts[i] = 1;
                else cardCounts[i] = 4;
            }
        }

        public void reset()
        {
            cardCounts = Poker.getEmptyCounts();
            mingPai = new bool[Room.room.roomRule.rule.playerCount];
            for (int i = 0; i < mingPai.Length; i++)
            {
                mingPai[i] = false;
            }
        }

        public bool getMingPai(int index)
        {
            return mingPai[index];
        }

        public int[] getCounts()
        {
            return cardCounts;
        }

        public int getRemainCount(int index)
        {
            if (index == 0 || index == 14)
            {
                return 1 - cardCounts[index];
            }
            return 4 - cardCounts[index];
        }

        public int[] getRemainCount()
        {
            int[] cards = new int[cardCounts.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = getRemainCount(i);
            }
            return cards;
        }

        public override void bytesRead(ByteBuffer data)
        {
            for (int i = 0; i < cardCounts.Length; i++)
            {
                cardCounts[i] = data.readInt();
            }
        }

        public void clone(CardRecorder bean)
        {
            mingPai = new bool[bean.mingPai.Length];
            cardCounts = new int[bean.cardCounts.Length];
            for (int i = 0; i < bean.mingPai.Length; i++)
            {
                mingPai[i] = bean.mingPai[i];
            }
            for (int i = 0; i < bean.cardCounts.Length; i++)
            {
                cardCounts[i] = bean.cardCounts[i];
            }
        }
    }
}