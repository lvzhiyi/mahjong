using UnityEngine.UI;

namespace platform.poker
{
    /// <summary> 玩家出的牌 </summary>
    public class PKShowPlayCardBar : UnrealLuaSpaceObject
    {
        private Image cardimg;
        private Image dizhu;

        protected override void xinit()
        {
            cardimg = transform.Find("card").GetComponent<Image>();
            dizhu = transform.Find("dizhu").GetComponent<Image>();
        }

        public void setImg(int index)
        {
            cardimg.sprite = PokerCardManager.instance.getHandPoker(index);
        }

        public void setDiZhu(bool isShow, int pos)
        {
            if (PKGAME.RULESID == PKGAME.DDZ && DDZMatch.match != null && pos == DDZMatch.match.dizhuIndexLocal)
                dizhu.gameObject.SetActive(isShow);
            else dizhu.gameObject.SetActive(false);
        }

        public void refreshPos()
        {
            DisplayKit.setLocalX(this, index_space);
        }
    }
}
