using scene.game;
using UnityEngine.UI;

namespace platform.spotred.room
{
    /// <summary>
    /// 显示打的牌
    /// </summary>
    public class PlayCardView:UnrealLuaSpaceObject
    {
        public Image card;

        private PositionTweener postionTweener;

        private RotationTweener rotationTweener;

        protected override void xinit()
        {
            this.card = this.transform.Find("card").GetComponent<Image>();

            this.postionTweener = this.GetComponent<PositionTweener>();
            this.rotationTweener = this.GetComponent<RotationTweener>();

            DisplayKit.setLocalScaleXY(transform,0.9f);
        }

        public void setCard(int card)
        {
            this.card.sprite = WidgetManager.getInstance().getHandCard(card);
        }

        public void clearAnmation()
        {
            if (this.postionTweener != null)
            {
                this.postionTweener.enabled = false;
                this.rotationTweener.enabled = false;
            }
        }

        public void startAnation()
        {
            if (this.postionTweener != null)
            {
                this.postionTweener.enabled = true;
                this.rotationTweener.enabled = true;
            }
        }
    }
}
