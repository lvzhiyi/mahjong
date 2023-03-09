using cambrian.common;

namespace platform.poker
{
    public class CardSort
    {
        public static int[] desktopSortLTS(DDZCardInfo cards)
        {
            return desktopSortLTS(cards.cards, cards.type);
        }

        public static int[] desktopSortLTS(PDKCardInfo cards)
        {
            return desktopSortLTS(cards.cards, cards.type);
        }

        public static int[] desktopSortLTS(int[] cards, int type)
        {
            if (cards == null || cards.Length == 0) return cards;
            cards = LTSCards(cards);
            switch (type)
            {
                case PKCardType.TYPE_4_1:
                case PKCardType.TYPE_4_1_1:
                case PKCardType.TYPE_4_1_1_1:
                case PKCardType.TYPE_4_2_2:
                    return SortTYPE_Four_Sort(cards);//四连排序

                case PKCardType.TYPE_3_1:
                case PKCardType.TYPE_3_2:
                case PKCardType.TYPE_FEIJI_1:
                case PKCardType.TYPE_FEIJI_2:
                case PKCardType.TYPE_CARDS_PLANE:
                    return SortTYPE_Three_Sore(cards);//三连排序

                default: return cards;
            }
        }

        private static int[] SortTYPE_Four_Sort(int[] cards)
        {
            ArrayList<int> list = new ArrayList<int>(cards.Length);
            ArrayList<int> card = new ArrayList<int>(cards.Length);
            card.add(cards);
            for (int i = 0; i < cards.Length - 2; i++)
            {
                if (Poker.getValue(cards[i]) == Poker.getValue(cards[i + 1]) &&
                   Poker.getValue(cards[i]) == Poker.getValue(cards[i + 2]) &&
                   Poker.getValue(cards[i]) == Poker.getValue(cards[i + 3]))
                {
                    list.add(cards[i]);
                    list.add(cards[i + 1]);
                    list.add(cards[i + 2]);
                    list.add(cards[i + 3]);
                    card.removeAt(i);
                    card.removeAt(i);
                    card.removeAt(i);
                    card.removeAt(i);
                    break;
                }
            }
            for (int i = 0; i < card.count; i++)
            {
                list.add(card.get(i));
            }
            card = null;
            return list.toArray();
        }

        private static int[] SortTYPE_Three_Sore(int[] cards)
        {
            ArrayList<int> list = new ArrayList<int>(cards.Length);
            ArrayList<int> card = new ArrayList<int>(cards.Length);
            card.add(cards);
            for (int i = 0; i < cards.Length - 2; i++)
            {
                if (Poker.getValue(cards[i]) == Poker.getValue(cards[i + 1]) &&
                    Poker.getValue(cards[i]) == Poker.getValue(cards[i + 2]))
                {
                    if (i < cards.Length - 3 && Poker.getValue(cards[i]) == Poker.getValue(cards[i + 3])) continue;
                    list.add(cards[i]);
                    list.add(cards[i + 1]);
                    list.add(cards[i + 2]);
                    card.remove(cards[i]);
                    card.remove(cards[i + 1]);
                    card.remove(cards[i + 2]);
                }
            }
            int len = list.count;
            if (list.count > 3)
            {
                ArrayList<int> lists = new ArrayList<int>();
                for (int i = 0; i < len; i += 3)
                {
                    if (i > 0 && i < list.count - 3)
                    {
                        if (Poker.DDZ_GetSortValueByCard(list.get(i)) == Poker.DDZ_GetSortValueByCard(list.get(i + 3)) + 1) continue;
                        if (Poker.DDZ_GetSortValueByCard(list.get(i)) == Poker.DDZ_GetSortValueByCard(list.get(i - 1)) - 1) continue;
                    }
                    else if (i == 0)
                    {
                        if (Poker.DDZ_GetSortValueByCard(list.get(i)) == Poker.DDZ_GetSortValueByCard(list.get(i + 3)) + 1) continue;
                    }
                    else if (i >= list.count - 3)
                    {
                        if (Poker.DDZ_GetSortValueByCard(list.get(i)) == Poker.DDZ_GetSortValueByCard(list.get(i - 1)) - 1) continue;
                    }
                    card.add(list.get(i));
                    card.add(list.get(i + 1));
                    card.add(list.get(i + 2));
                    lists.add(list.get(i));
                    lists.add(list.get(i + 1));
                    lists.add(list.get(i + 2));
                }
                for (int i = 0; i < lists.count; i++)
                {
                    list.remove(lists.get(i));
                }
                lists = null;
            }
            cards = LTSCards(card.toArray());
            var temp = (int[])LTSCards(list.toArray()).Clone();
            list.removeAll();
            list.add(temp);
            for (int i = 0; i < cards.Length; i++)
            {
                list.add(cards[i]);
            }
            card = null; cards = null;
            return list.toArray();
        }

        public static int[] STLCards(int[] sortcards)
        {
            if (sortcards.Length <= 1) return sortcards;
            for (int i = 0; i < sortcards.Length; i++)
            {
                for (int j = 0; j < sortcards.Length - i - 1; j++)
                {
                    if (Poker.getSortValue(sortcards[j]) > Poker.getSortValue(sortcards[j + 1]))
                    {
                        sortcards[j] = sortcards[j] ^ sortcards[j + 1];
                        sortcards[j + 1] = sortcards[j] ^ sortcards[j + 1];
                        sortcards[j] = sortcards[j] ^ sortcards[j + 1];
                    }
                    else if (Poker.getSortValue(sortcards[j]) == Poker.getSortValue(sortcards[j + 1]))
                    {
                        if (Poker.getType(sortcards[j]) > Poker.getType(sortcards[j + 1]))
                        {
                            sortcards[j] = sortcards[j] ^ sortcards[j + 1];
                            sortcards[j + 1] = sortcards[j] ^ sortcards[j + 1];
                            sortcards[j] = sortcards[j] ^ sortcards[j + 1];
                        }
                    }
                }
            }
            return sortcards;
        }

        public static int[] LTSCards(int[] sortcards)
        {
            if (sortcards.Length <= 1) return sortcards;
            if (sortcards[0] == 0xffff) return sortcards;
            int sorta, storb;
            for (int i = 0; i < sortcards.Length; i++)
            {
                for (int j = 0; j < sortcards.Length - i - 1; j++)
                {
                    sorta = Poker.getSortValue(sortcards[j]);
                    storb = Poker.getSortValue(sortcards[j + 1]);
                    if (sorta < storb)
                    {
                        sortcards[j + 1] = sortcards[j + 1] ^ sortcards[j];
                        sortcards[j] = sortcards[j + 1] ^ sortcards[j];
                        sortcards[j + 1] = sortcards[j + 1] ^ sortcards[j];
                    }
                    else if (sorta == storb)
                    {
                        if (Poker.getType(sortcards[j]) > Poker.getType(sortcards[j + 1]))
                        {
                            sortcards[j + 1] = sortcards[j + 1] ^ sortcards[j];
                            sortcards[j] = sortcards[j + 1] ^ sortcards[j];
                            sortcards[j + 1] = sortcards[j + 1] ^ sortcards[j];
                        }
                    }
                }
            }
            return sortcards;
        }
    }
}
