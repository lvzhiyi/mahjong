using scene.game;
using UnityEngine.UI;

namespace platform.spotred.room
{
    public class OperateCardsBar:UnrealLuaSpaceObject
    {
        private Image card;
        protected override void xinit()
        {
            base.xinit();
            this.card = transform.Find("card").GetComponent<Image>();
            DisplayKit.setLocalRoateXYZ(card.transform,0,0,90);
        }

        public void setCard(int card)
        {
            this.card.sprite = WidgetManager.getInstance().getHandCard(card);
        }
    }
}
