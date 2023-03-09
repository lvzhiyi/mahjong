using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJTingCardBar:UnrealLuaSpaceObject
    {
        protected Image cardimg;

        private Image cardbg;

        UnrealTextField fanshu;

        protected UnrealTextField num;

        protected TingCard tingcard;

        protected override void xinit()
        {
            cardimg = transform.Find("card").Find("card").GetComponent<Image>();
            cardbg = transform.Find("card").Find("bg").GetComponent<Image>();
            fanshu = transform.Find("fan").GetComponent<UnrealTextField>();
            fanshu.init();
            num = transform.Find("num").GetComponent<UnrealTextField>();
            num.init();
        }

        public void setTingCard(TingCard ting)
        {
            tingcard = ting;
        }

        protected override void xrefresh()
        {
            cardimg.sprite = MJWidgetManager.getInstance().getCard(tingcard.card);
            cardbg.sprite = MJWidgetManager.getInstance().getCardBg(1);
            fanshu.text = tingcard.fan.ToString();
            num.text = tingcard.num.ToString();
        }
    }
}
