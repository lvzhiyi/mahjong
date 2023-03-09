using scene.game;
using platform.spotred;
using UnityEngine.UI;

namespace platform.spotred.room
{
    public class TouCardBar: UnrealLuaSpaceObject
    {
        private Image card_img;

        private int card;
        protected override void xinit()
        {
            this.card_img = this.transform.Find("card").GetComponent<Image>();
            DisplayKit.setLocalRoateXYZ(this.card_img.transform,0,0,90);
        }


        public void setCard(int card)
        {
            this.card = card;
        }

        protected override void xrefresh()
        {
            if (card==Card.INVISIBLE)
            {
                this.card_img.sprite = DeskTopManager.instance.getBackCardStyle();
            }
            else
            {
                this.card_img.sprite = WidgetManager.getInstance().getHandCard(card);
            }
        }
    }
}
