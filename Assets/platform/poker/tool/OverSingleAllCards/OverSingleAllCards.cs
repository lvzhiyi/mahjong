using UnityEngine;

namespace platform.poker
{

    public class OverSingleAllCards : UnrealLuaSpaceObject
    {
        /// <summary> 组件 </summary>
        private UnrealTableContainer handcards, discards;

        /// <summary> 数据 </summary>
        private int[] useCards, noUseCards;

        public void setData(int[] one, int[] two)
        {
            useCards = one;
            noUseCards = two;
        }

        protected override void xinit()
        {
            handcards = transform.Find("handcards").GetComponent<UnrealTableContainer>();
            handcards.init();

            discards = transform.Find("discards").GetComponent<UnrealTableContainer>();
            discards.init();
        }

        protected override void xrefresh()
        {
            handcards.cols = useCards.Length;
            handcards.resize(useCards.Length);
            useCards = CardSort.LTSCards(useCards);
            PokerOverRemainBar bar;
            for (int i = 0; i < useCards.Length; i++)                   //使用的牌
            {
                bar = (PokerOverRemainBar)handcards.bars[i];
                bar.setCard(useCards[i]);
                bar.refresh();
            }

            discards.cols = noUseCards.Length;
            discards.resize(noUseCards.Length);
            //noUseCards = CardSort.LTSCards(noUseCards);                  //未使用的牌 手牌
            for (int i = 0; i < noUseCards.Length; i++)
            {
                bar = (PokerOverRemainBar)discards.bars[i];
                bar.setCard(noUseCards[i]);
                bar.refresh();
            }
            var w = discards.bars[0].getWidth();
            handcards.GetComponent<RectTransform>().sizeDelta = new Vector2(w * (useCards.Length + 1), 100);
            discards.GetComponent<RectTransform>().sizeDelta = new Vector2(w * (noUseCards.Length + 1), 100);
            DisplayKit.setLocalX(discards.gameObject, w * (useCards.Length + 1) + 50);
        }

    }
}