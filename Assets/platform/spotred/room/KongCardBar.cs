using scene.game;
using cambrian.unreal.display;
using platform.spotred;
using UnityEngine.UI;

namespace platform.spotred.room
{
    /// <summary>
    /// 小家巴牌
    /// </summary>
    public class KongCardBar:UnrealLuaSpaceObject
    {
        private Image img;

        public int card;
        protected override void xinit()
        {
            this.img = transform.Find("bg").GetComponent<Image>();
        }

        public void setData(int card)
        {
            this.card = card;
        }

        protected override void xrefresh()
        {
            this.img.sprite = WidgetManager.getInstance().getNowHandCard(card);
            DisplayKit.setLocalXY(this.transform, getWidth() * index_space + index_space * 10, 0);
        }
    }
}
