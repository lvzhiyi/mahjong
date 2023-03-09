using System.Collections;
using cambrian.common;

namespace platform.poker
{
    /// <summary> 斗地主发牌动画 </summary>
    public class PKDealPokersView : UnrealLuaSpaceObject
    {
        protected ArrayList<int> leftcards, downcards, rightcards,topcards;

        /// <summary> 牌的张数 </summary> 记录牌发到第几张
        protected int cardnum;

        /// <summary> 全部牌 </summary>
        protected int[][] cards;

        /// <summary> 明牌 </summary>
        protected bool[] mingpai;

        /// <summary> 发牌间隙时间 </summary>
        protected float DealPokerTimeSpace = 0.15f;

        /// <summary> 发牌时间 </summary>
        protected float DealPokerTime = 3.6f;

        public virtual void deal(int[][] cards)
        {
            this.cards = cards;
            mingpai = new bool[cards.Length];
            cardnum = 0;
            leftcards = new ArrayList<int>();
            rightcards = new ArrayList<int>();
            downcards = new ArrayList<int>();
            topcards = new ArrayList<int>();
            DealPokerTime = 0.2f;
            StartCoroutine("dealCards");
        }

        /// <summary> 显示明牌 </summary>
        public virtual void dealMingPai(int pos, int[] cards, bool dizhu) { }

        /// <summary> 添加手牌 </summary>
        protected virtual ArrayList<int> addCardsHand(ArrayList<int> list, int[] cards)
        {
            list.clear();
            foreach (var item in list.toArray())
            {
                list.add(item);
            }
            return list;
        }

        protected virtual IEnumerator dealCards() { yield break; }

        /// <summary> 添加手牌 刷新手牌 </summary>
        protected virtual void addHandCards(int pos) { }

        /// <summary> 结束发牌 </summary>
        protected virtual void endDealCards() { }
    }
}
