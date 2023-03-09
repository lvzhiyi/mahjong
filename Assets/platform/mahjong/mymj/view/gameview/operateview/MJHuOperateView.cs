using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 胡牌操作视图
    /// </summary>
    public class MJHuOperateView:UnrealLuaSpaceObject
    {
        protected Image cardicon;

        private Image cardbg;

        Text fanshu;

        protected MJOperateInfoBean bean;

        protected override void xinit()
        {
            base.xinit();
            cardicon = transform.Find("card").GetComponent<Image>();
            fanshu = transform.Find("text").GetComponent<Text>();
            cardbg = transform.Find("bg").GetComponent<Image>();
        }

        public void setBean(MJOperateInfoBean bean)
        {
            this.bean = bean;
        }


        protected override void xrefresh()
        {
            base.xrefresh();
            cardicon.sprite = MJWidgetManager.getInstance().getCard(bean.cards[0].card);
            fanshu.text = bean.cards[0].fanshu.ToString();
            cardbg.sprite = MJWidgetManager.getInstance().getCardBg(1);
        }
    }
}
