using scene.game;
using platform.spotred;
using UnityEngine.UI;

namespace platform.spotred.room
{
    public class RFixedBar:UnrealLuaSpaceObject
    {
        private Image card_img;

        public int card;
        /// <summary>
        /// 该行的数量
        /// </summary>
        private int count;

        private int line;

        protected override void xinit()
        {
            this.card_img = transform.Find("bg").GetComponent<Image>();
            DisplayKit.setLocalScaleXY(transform,0.9f);
        }

        public void setData(int card, int count, int line)
        {
            this.card = card;
            this.count = count;
            this.line = line;
        }


        protected override void xrefresh()
        {
            if (card==Card.INVISIBLE)
            {
                this.card_img.sprite = DeskTopManager.instance.getSlipCardStyle();
            }
            else
            {

                this.card_img.sprite = WidgetManager.getInstance().getShowCard(card);
            }
           // this.card_img.sprite = WidgetManager.getInstance().getShowCard(Card.getName(card));
            DisplayKit.setLocalXY(this.transform, -line*45, getHeight() * (index_space-count));

        }
    }
}
