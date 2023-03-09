using UnityEngine.UI;

namespace platform.poker
{
    /// <summary>
    /// 扑克剩余的牌
    /// </summary>
    public class PokerOverRemainBar:UnrealLuaSpaceObject
    {

        Image cardicon;

        int card;

        protected override void xinit()
        {
            cardicon = transform.Find("card").GetComponent<Image>();
        }

        public void setCard(int card)
        {
            this.card = card;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            cardicon.sprite= PokerCardManager.instance.getHandPoker(card);
        }
    }
}
