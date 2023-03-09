using platform.spotred;
using UnityEngine;
using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJOverFixedBar:UnrealLuaSpaceObject
    {
        Image card;

        Image card1;

        Image card2;

        Image card3;

        Image cardbg;

        Image cardbg1;

        Image cardbg2;

        Image cardbg3;

        MJFixedCards cards;
        /// <summary>
        /// 巴杠图片
        /// </summary>
        private Image rao;
        /// <summary>
        /// 正常颜色
        /// </summary>
        public Color normalColor = new Color(255, 255, 255, 255);
        /// <summary>
        /// 被胡颜色
        /// </summary>
        public Color huColor = new Color(255, 255, 255, 120);

        protected override void xinit()
        {
            card = transform.Find("card").GetComponent<Image>();
            card1 = transform.Find("card1").GetComponent<Image>();
            card2 = transform.Find("card2").GetComponent<Image>();
            card3 = transform.Find("card3").GetComponent<Image>();
            cardbg = transform.Find("bg").GetComponent<Image>();
            cardbg1 = transform.Find("bg1").GetComponent<Image>();
            cardbg2 = transform.Find("bg2").GetComponent<Image>();
            cardbg3 = transform.Find("bg3").GetComponent<Image>();
            rao = transform.Find("rao").GetComponent<Image>();
        }

        public void setFixedCards(MJFixedCards cards)
        {
            this.cards = cards;
        }

        protected override void xrefresh()
        {
            int[] c= cards.cards;
            card.sprite = MJWidgetManager.getInstance().getCard(c[0]);
            card1.sprite = MJWidgetManager.getInstance().getCard(c[1]);
            if (MJCard.isSigned(c[2], MJCard.SIGN_HU))
            {
                card2.sprite = MJWidgetManager.getInstance().getCard(MJCard.cancelSign(c[2], MJCard.SIGN_HU));
                card2.color = huColor;
                cardbg2.color = huColor;
            }
            else
            {
                card2.sprite = MJWidgetManager.getInstance().getCard(c[2]);
                card2.color = normalColor;
                cardbg2.color = normalColor;
            }
               
            card3.sprite = MJWidgetManager.getInstance().getCard(c[0]);

            card.gameObject.SetActive(true);
            card1.gameObject.SetActive(true);
            card2.gameObject.SetActive(true);
            card3.gameObject.SetActive(true);

            cardbg.gameObject.SetActive(true);
            cardbg1.gameObject.SetActive(true);
            cardbg2.gameObject.SetActive(true);
            cardbg3.gameObject.SetActive(true);

            cardbg.sprite = MJWidgetManager.getInstance().getCardBg(1);
            cardbg1.sprite = MJWidgetManager.getInstance().getCardBg(1);
            cardbg2.sprite = MJWidgetManager.getInstance().getCardBg(1);
            cardbg3.sprite = MJWidgetManager.getInstance().getCardBg(1);

            switch (cards.type)
            {
                case FixedCards.MJ_BAO_KONG:
                case FixedCards.MJPONG:

                    cardbg3.gameObject.SetActive(false);
                    card3.gameObject.SetActive(false);
                    break;
                case FixedCards.MJKONG_PRI:
                    card.gameObject.SetActive(false);
                    card1.gameObject.SetActive(false);
                    card2.gameObject.SetActive(false);

                    cardbg.sprite = MJWidgetManager.getInstance().getCardBg(2);
                    cardbg1.sprite = MJWidgetManager.getInstance().getCardBg(2);
                    cardbg2.sprite = MJWidgetManager.getInstance().getCardBg(2);
                    break;
                case FixedCards.MJKONG_SUP:
                    if (MJCard.isSigned(c[3], MJCard.SIGN_HU))
                    { 
                        card3.sprite = MJWidgetManager.getInstance().getCard(MJCard.cancelSign(c[3], MJCard.SIGN_HU));
                        card3.color = huColor;
                        cardbg3.color = huColor;
                    }
                    else
                    {
                        card2.sprite = MJWidgetManager.getInstance().getCard(c[3]);
                        card3.color = normalColor;
                        cardbg3.color = normalColor;
                    }
                        
                    break;
            }
            rao.gameObject.SetActive(cards.type == FixedCards.MJKONG_SUP);
        }

    }
}
