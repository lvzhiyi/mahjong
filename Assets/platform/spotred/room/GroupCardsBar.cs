using scene.game;
using UnityEngine.UI;

namespace platform.spotred.room
{
    /// <summary>
    /// 单局结算界面固定牌
    /// </summary>
    public class GroupCardsBar:UnrealLuaSpaceObject
    {
        private Image img;

        private int card;

        private int count;

        protected override void xinit()
        {
            this.img = transform.Find("bg").GetComponent<Image>();

        }

        protected override void xrefresh()
        {
            this.img.sprite = WidgetManager.getInstance().getJieSanShowCard(card);
            DisplayKit.setLocalXY(this.transform,getWidth()*(index_space+count),0);
        }

        public void setCard(int card,int count)
        {
            this.card = card;
            this.count = count;
        }

        /// <summary>
        /// 显示牌堆剩余牌
        /// </summary>
        public void showRemainingCards()
        {
            this.img.sprite = WidgetManager.getInstance().getHandCard(card);
            DisplayKit.setLocalXY(this.transform, getWidth() * (index_space + count), 0);
        }

    }
}
