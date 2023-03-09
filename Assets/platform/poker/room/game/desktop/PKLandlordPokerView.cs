using UnityEngine;

namespace platform.poker
{
    /// <summary> 地主牌显示 </summary>
    public class PKLandlordPokerView : UnrealLuaSpaceObject
    {
        public float space = 32.5f;

        private UnrealDivTableContainer container;

        private int[] cards = new int[0];

        protected override void xinit()
        {
            container = transform.Find("single").GetComponent<UnrealDivTableContainer>();
            container.init();
        }

        public void setData(int[] cards)
        {
            this.cards = CardSort.LTSCards(cards);
            xrefresh();
        }

        protected override void xrefresh()
        {
            if (cards.Length > 0)
            {
                container.resize(cards.Length);
                for (int i = 0; i < cards.Length; i++)
                {
                    var bar = (PKShowPlayCardBar)container.bars[i];
                    bar.index_space = (Mathf.FloorToInt(i * space));
                    bar.setImg(cards[i]);
                    bar.setDiZhu(false, -1);
                    bar.refresh();
                    DisplayKit.setLocalX(bar, bar.index_space);
                    bar.setVisible(true);
                }
            }
            setVisible(cards.Length > 0);
        }
    }
}
