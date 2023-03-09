using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 报杠的牌
    /// </summary>
    public class AYMJPlayerBaoKongBar : MJBasePlayerFixedBar
    {
        /// <summary>
        /// 报杠的牌
        /// </summary>
        int baoKongCard;

        protected override void xinit()
        {
            card1 = transform.Find("card").GetComponent<Image>();
            card2 = transform.Find("card2").GetComponent<Image>();
            card3 = transform.Find("card3").GetComponent<Image>();
            bg1 = transform.Find("bg").GetComponent<Image>();
            bg2 = transform.Find("bg2").GetComponent<Image>();
            bg3 = transform.Find("bg3").GetComponent<Image>();
        }
       

        public void setCard(int card,int postion)
        {
            baoKongCard = card;
            this.postion = postion;
        }

        protected override void xrefresh()
        {
            card1.gameObject.SetActive(false);
            card2.gameObject.SetActive(false);
            card3.gameObject.SetActive(false);

            switch (postion)
            {
                case MahJongPanel.LOC_DOWN:
                    down();
                    break;
                case MahJongPanel.LOC_RIGHT:
                    right();
                    break;
                case MahJongPanel.LOC_TOP:
                    top();
                    break;
                case MahJongPanel.LOC_LEFT:
                    left();
                    break;
            }
        }

        public override void down()
        {
            card1.sprite = MJWidgetManager.getInstance().getCard(baoKongCard);
            card2.sprite = MJWidgetManager.getInstance().getCard(baoKongCard);
            card3.sprite = MJWidgetManager.getInstance().getCard(baoKongCard);
            card1.gameObject.SetActive(true);
            card2.gameObject.SetActive(true);
            card3.gameObject.SetActive(true);
            bg1.sprite = MJWidgetManager.getInstance().getCardBg(21);
            bg2.sprite = MJWidgetManager.getInstance().getCardBg(21);
            bg3.sprite = MJWidgetManager.getInstance().getCardBg(21);
            float x = intervalwidth * index_space;
            DisplayKit.setLocalX(this, x);
        }

        public override void right()
        {
            float y = intervalwidth * index_space;
            DisplayKit.setLocalY(this, y);
            bg1.sprite = MJWidgetManager.getInstance().getCardBg(12);
            bg2.sprite = MJWidgetManager.getInstance().getCardBg(12);
            bg3.sprite = MJWidgetManager.getInstance().getCardBg(12);
            if (baoKongCard == MJCard.CNO) return;
            bg1.sprite = MJWidgetManager.getInstance().getCardBg(11);
            bg2.sprite = MJWidgetManager.getInstance().getCardBg(11);
            bg3.sprite = MJWidgetManager.getInstance().getCardBg(11);
            card1.sprite = MJWidgetManager.getInstance().getCard(baoKongCard);
            card2.sprite = MJWidgetManager.getInstance().getCard(baoKongCard);
            card3.sprite = MJWidgetManager.getInstance().getCard(baoKongCard);
            card1.gameObject.SetActive(true);
            card2.gameObject.SetActive(true);
            card3.gameObject.SetActive(true);
        }

        public override void top()
        {
            float x = -intervalwidth * index_space;
            DisplayKit.setLocalX(this, x);
            bg1.sprite = MJWidgetManager.getInstance().getCardBg(22);
            bg2.sprite = MJWidgetManager.getInstance().getCardBg(22);
            bg3.sprite = MJWidgetManager.getInstance().getCardBg(22);
            if (baoKongCard == MJCard.CNO) return;
            bg1.sprite = MJWidgetManager.getInstance().getCardBg(21);
            bg2.sprite = MJWidgetManager.getInstance().getCardBg(21);
            bg3.sprite = MJWidgetManager.getInstance().getCardBg(21);
            card1.sprite = MJWidgetManager.getInstance().getCard(baoKongCard);
            card2.sprite = MJWidgetManager.getInstance().getCard(baoKongCard);
            card3.sprite = MJWidgetManager.getInstance().getCard(baoKongCard);
            card1.gameObject.SetActive(true);
            card2.gameObject.SetActive(true);
            card3.gameObject.SetActive(true);
        }

        public override void left()
        {
            float y = -intervalwidth * index_space;
            DisplayKit.setLocalY(this, y);
            bg1.sprite = MJWidgetManager.getInstance().getCardBg(12);
            bg2.sprite = MJWidgetManager.getInstance().getCardBg(12);
            bg3.sprite = MJWidgetManager.getInstance().getCardBg(12);
            if (baoKongCard == MJCard.CNO) return;
            bg1.sprite = MJWidgetManager.getInstance().getCardBg(11);
            bg2.sprite = MJWidgetManager.getInstance().getCardBg(11);
            bg3.sprite = MJWidgetManager.getInstance().getCardBg(11);
            card1.sprite = MJWidgetManager.getInstance().getCard(baoKongCard);
            card2.sprite = MJWidgetManager.getInstance().getCard(baoKongCard);
            card3.sprite = MJWidgetManager.getInstance().getCard(baoKongCard);
            card1.gameObject.SetActive(true);
            card2.gameObject.SetActive(true);
            card3.gameObject.SetActive(true);
        }
    }
}
