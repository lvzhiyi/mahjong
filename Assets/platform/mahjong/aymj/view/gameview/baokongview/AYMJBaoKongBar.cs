using UnityEngine.UI;

namespace platform.mahjong
{
    public class AYMJBaoKongBar:UnrealLuaSpaceObject
    {
        public int card;

        Image card1;
        Image card2;
        Image card3;

        public UnrealCheckBox box;

        protected override void xinit()
        {
            card1 = transform.Find("card_1").GetComponent<Image>();
            card2 = transform.Find("card_2").GetComponent<Image>();
            card3 = transform.Find("card_3").GetComponent<Image>();
            box = transform.Find("checkbox").GetComponent<UnrealCheckBox>();
            box.init();
        }

        public void setCard(int card)
        {
            this.card = card;
        }

        protected override void xrefresh()
        {
            card1.sprite = MJWidgetManager.getInstance().getCard(card);
            card2.sprite = MJWidgetManager.getInstance().getCard(card);
            card3.sprite = MJWidgetManager.getInstance().getCard(card);

            box.setState(UnrealCheckObject.NORMAL);
        }

        public void setAction(bool state)
        {
            box.setState(state);
        }
    }
}
