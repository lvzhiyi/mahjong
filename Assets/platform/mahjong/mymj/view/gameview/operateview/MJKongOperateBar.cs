using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJKongOperateBar:UnrealLuaSpaceObject
    {

        public MJOperationCardBean bean;

        protected Image icon;

        private Image cardbg;
       

        protected override void xinit()
        {
            base.xinit();
            icon = transform.Find("card").GetComponent<Image>();
            cardbg = transform.Find("cardbg").GetComponent<Image>();
        }

        public void setBean(MJOperationCardBean bean)
        {
            this.bean = bean;
        }

        protected override void xrefresh()
        {
            icon.sprite = MJWidgetManager.getInstance().getCard(bean.card);
            cardbg.sprite = MJWidgetManager.getInstance().getCardBg(1);
        }
    }
}
