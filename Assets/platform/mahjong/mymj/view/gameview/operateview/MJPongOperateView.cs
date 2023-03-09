using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 胡牌操作视图
    /// </summary>
    public class MJPongOperateView : UnrealLuaSpaceObject
    {
        protected Image cardicon;

        private Image cardbg;

        protected MJOperateInfoBean bean;

        protected override void xinit()
        {
            base.xinit();
            cardicon = transform.Find("card").GetComponent<Image>();
            cardbg = transform.Find("bg").GetComponent<Image>();
        }

        public void setCard(MJOperateInfoBean bean)
        {
            this.bean = bean;
        }


        protected override void xrefresh()
        {
            cardicon.sprite = MJWidgetManager.getInstance().getCard(bean.cards[0].card);
            cardbg.sprite = MJWidgetManager.getInstance().getCardBg(1);
        }
    }
}
