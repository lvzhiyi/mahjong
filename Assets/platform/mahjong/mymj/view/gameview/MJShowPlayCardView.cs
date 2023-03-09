using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 显示玩家打的牌视图
    /// </summary>
    public class MJShowPlayCardView:UnrealLuaSpaceObject
    {
        protected Image card;

        protected Image cardbg;

        protected AlphaTweener tweener;

        protected override void xinit()
        {
            card = transform.Find("card").GetComponent<Image>();
            cardbg = transform.Find("bg").GetComponent<Image>();
            tweener = transform.GetComponent<AlphaTweener>();
        }

        /// <summary>
        /// 显示牌
        /// </summary>
        public virtual void showCard(int card,int index)
        {
            cardbg.sprite = MJWidgetManager.getInstance().getCardBg(1);

            setVisible(true);
            tweener.reset();
            this.card.sprite = MJWidgetManager.getInstance().getCard(card);
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    DisplayKit.setLocalXY(transform,0,-100);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    DisplayKit.setLocalXY(transform, 230, 0);
                    break;
                case MahJongPanel.LOC_TOP:
                    DisplayKit.setLocalXY(transform, 0, 143);
                    break;
                case MahJongPanel.LOC_LEFT:
                    DisplayKit.setLocalXY(transform, -230, 0);
                    break;
            }
        }
    }
}
