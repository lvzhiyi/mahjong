using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 单张手牌bar
    /// </summary>
    public class MJOverHandBar : UnrealLuaSpaceObject
    {
        int card;

        /// <summary>
        /// 显示的牌
        /// </summary>
        Image img;

        /// <summary>
        /// 底牌
        /// </summary>
        private Image imgbg;

        /// <summary>
        /// 躺图标
        /// </summary>
        Image tang;

        bool isMocard;

        float initX;

        private float initimgx;

        protected override void xinit()
        {
            img = transform.Find("card").GetComponent<Image>();
            imgbg = transform.Find("bg").GetComponent<Image>();
            tang = transform.Find("tang").GetComponent<Image>();
            initX = tang.transform.localPosition.x;
            initimgx = img.transform.localPosition.x;
        }

        public void setCard(int card)
        {
            this.card = card;
        }

        protected override void xrefresh()
        {
            if (MJCard.isSigned(card, MJCard.SIGN_TANG))
            {
                img.sprite = MJWidgetManager.getInstance().getCard(MJCard.cancelSign(card, MJCard.SIGN_TANG));
                imgbg.sprite = MJWidgetManager.getInstance().getCardBg(1);
                tang.gameObject.SetActive(true);
                DisplayKit.setLocalXY(img.gameObject, initimgx + 10.0f, 8.0f);
                DisplayKit.setLocalX(imgbg.gameObject, initimgx + 10.0f);
                DisplayKit.setLocalX(tang.gameObject, initX + 10.0f);
            }
            else
            {
                img.sprite = MJWidgetManager.getInstance().getCard(card);
                imgbg.sprite = MJWidgetManager.getInstance().getCardBg(0);
                tang.gameObject.SetActive(false);
                DisplayKit.setLocalXY(img.transform, 0, -3.6f);
                DisplayKit.setLocalX(imgbg.gameObject, 0);
            }
        }
    }
}
