using scene.game;
using platform.spotred;
using UnityEngine.UI;

namespace platform.spotred.room
{
    public class FixedBar: UnrealLuaSpaceObject
    {
        private Image card_img;

        private int card;
        /// <summary>
        /// 该行的数量
        /// </summary>
        private int count;

        private int line;


        protected override void xinit()
        {
            this.card_img = transform.Find("bg").GetComponent<Image>();
        }

        public void setData(int card,int count,int line)
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
            DisplayKit.setLocalXY(this.transform, getHeight() * (index_space-count), line*45);
        }
    }
}
