using scene.game;
using UnityEngine.UI;

namespace platform.spotred.room
{
    /// <summary>
    /// 底牌bar
    /// </summary>
    public class ReMainCardBar:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 牌图片
        /// </summary>
        Image img;

        /// <summary>
        /// 牌
        /// </summary>
        int card;


        protected override void xinit()
        {
            this.img = this.transform.Find("card").GetComponent<Image>();
        }


        public void setCard(int card)
        {
            this.card = card;
        }


        protected override void xrefresh()
        {
            this.img.sprite = WidgetManager.getInstance().getJieSanShowCard(card);
        }
    }
}
