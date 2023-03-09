using UnityEngine;

namespace platform.spotred.room
{
    /// <summary>
    /// 底牌视图
    /// </summary>
    public class RemaincardsView:UnrealLuaSpaceObject
    {

        UnrealTableContainer container;
        /// <summary>
        /// 底牌
        /// </summary>
        int[] cards;

        protected override void xinit()
        {
            this.container = this.transform.Find("center").Find("mask").Find("container").GetComponent<UnrealTableContainer>();
            this.container.init();
        }

        public void setCards(int[] cards)
        {
            this.cards = cards;
        }

        protected override void xrefresh()
        {
            this.container.cols = cards.Length;
            this.container.resize(cards.Length);
            for (int i=0;i<cards.Length;i++)
            {
                ReMainCardBar bar=(ReMainCardBar)this.container.bars[i];
                bar.setCard(cards[i]);
                bar.refresh();
            }
            this.container.resizeDelta();
        }
    }
}
