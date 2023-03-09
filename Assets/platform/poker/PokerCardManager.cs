using System.Collections;
using scene.game;
using UnityEngine;

namespace platform.poker
{
    public class PokerCardManager
    {
        public static PokerCardManager instance
        {
            get
            {
                if (getInstance == null)
                    getInstance = new PokerCardManager();
                return getInstance;
            }
        }

        static PokerCardManager getInstance;

        private Hashtable table;

        public static int pokerBack { get; private set; }

        public PokerCardManager()
        {
            this.table = new Hashtable();
            initTable();
        }

        private void initTable()
        {
            pokerBack = -1;
            table.Add(Poker.INVISIBLE, 0);
            int count = 1;
            for (int i = 0; i < Poker.CARDS.Length; i++)
            {
                int[] cards = Poker.CARDS[i];
                for (int j = 0; j < cards.Length; j++)
                {
                    this.table.Add(cards[j], count);
                    count++;
                }
            }
            table.Add(0, count);
            table.Add(-1, count + 1);
            table.Add(-2, count + 2);
            table.Add(-3, count + 3);
            table.Add(-4, count + 4);
        }

        public Sprite getHandPoker(int index)
        {
            int pokerindex = (int)this.table[(index == Poker.INVISIBLE) ? pokerBack : index];
            return CacheManager.pokercard[pokerindex];
        }
    }
}
