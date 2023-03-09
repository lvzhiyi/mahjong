using UnityEngine;
using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 玩家手牌bar
    /// </summary>
    public class MJPlayerHandBar : MJBasePlayerHandBar
    {
        protected Image tangimg;

        /// <summary>
        /// 总的牌的数量(用于显示换完牌后，盖牌)
        /// </summary>
        protected int totalnum;
        /// <summary>
        /// 替换数量(用于显示换完牌后，盖牌)
        /// </summary>
        protected int replacenum;

        /// <summary>
        /// 标记躺牌的数量
        /// </summary>
        protected int sign_tang_num;
        /// <summary>
        /// 第几个标记躺牌
        /// </summary>
        protected int sign_index;
        /// <summary>
        /// 点炮图标
        /// </summary>
        Image dp;

        protected override void xinit()
        {
            base.xinit();
            tangimg = transform.Find("tang").GetComponent<Image>();

            if (transform.Find("pao") != null)
                dp = transform.Find("pao").GetComponent<Image>();
        }

        /// <summary>
        /// 是否是背景牌
        /// </summary>
        bool isUnKnow;

        /// <summary>
        /// 设置牌
        /// </summary>
        /// <param name="card"></param>
        /// <param name="postion">位置</param>
        /// <param name="isMoCard">是否是摸牌</param>
        public override void setCard(bool isHu, int card, int postion, MJMOCard moCard, bool isSelect, TingCardsInfo tinginfo, int distype, bool hasDistypeCard, int sign_tang_count, int sign_index)
        {
            totalnum = 0;
            replacenum = 0;
            this.isHu = isHu;

            this.postion = postion;
            this.moCard = moCard;
            if (moCard != null)
                card = moCard.card;
            cardValue = card;
            isUnKnow = (card == MJCard.CIN);
            this.isSelect = isSelect;
            this.tinginfo = tinginfo;
            this.distype = distype;
            this.hasDistypeCard = hasDistypeCard;
            sign_tang_num = sign_tang_count;
            this.sign_index = sign_index;
        }

        /// <summary>
        /// 总的牌数量
        /// </summary>
        /// <param name="count"></param>
        public override void setTotalNum(int count)
        {
            totalnum = count;
        }
        /// <summary>
        /// 换牌阶段调用该方法
        /// </summary>
        /// <param name="card"></param>
        /// <param name="postion"></param>
        public override void setCardRePlace(int card, int postion, int total, int replacenum)
        {
            isHu = false;
            cardValue = card;
            this.postion = postion;
            isUnKnow = (card == MJCard.CIN);
            this.isSelect = false;
            this.tinginfo = null;
            this.distype = MJCard.CNO;
            this.hasDistypeCard = false;
            this.totalnum = total;
            this.replacenum = replacenum;
            this.moCard = null;
        }


        protected override void xrefresh()
        {
            base.xrefresh();
            card.gameObject.SetActive(!isUnKnow);
            huDirection.gameObject.SetActive(false);
            tangimg.gameObject.SetActive(false);
            if (dp != null)
                dp.gameObject.SetActive(false);
            if (!isUnKnow)
            {
                if (MJCard.isSigned(cardValue, MJCard.SIGN_TANG))
                {
                    card.sprite = MJWidgetManager.getInstance().getCard(MJCard.cancelSign(cardValue, MJCard.SIGN_TANG));
                    tangimg.gameObject.SetActive(true);
                }
                else
                {
                    card.sprite = MJWidgetManager.getInstance().getCard(cardValue);
                }
            }

            switch (postion)
            {
                case MahjongRoomPanel.LOC_DOWN:
                    down();
                    break;
                case MahjongRoomPanel.LOC_RIGHT:
                    right();
                    break;
                case MahjongRoomPanel.LOC_TOP:
                    top();
                    break;
                case MahjongRoomPanel.LOC_LEFT:
                    left();
                    break;
            }
        }

        public override void pongRefreshHand()
        {
            card.gameObject.SetActive(!isUnKnow);
            huDirection.gameObject.SetActive(false);
            tangimg.gameObject.SetActive(false);
            if (dp != null)
                dp.gameObject.SetActive(false);
            if (!isUnKnow)
            {
                if (MJCard.isSigned(cardValue, MJCard.SIGN_TANG))
                {
                    card.sprite = MJWidgetManager.getInstance().getCard(MJCard.cancelSign(cardValue, MJCard.SIGN_TANG));
                    tangimg.gameObject.SetActive(true);
                }
                else
                {
                    card.sprite = MJWidgetManager.getInstance().getCard(cardValue);
                }
            }
            down();
            if (totalnum == index_space + 1)
            {
                DisplayKit.setLocalX(gameObject, transform.localPosition.x+15);
            }
        }


        protected override  void down()
        {
            float x = index_space * 77;
            float y = 0;
            if (moCard!=null)
                x += 21;
            else if (cardValue != MJCard.CIN && MJCard.isSigned(cardValue, MJCard.SIGN_TANG))
            {
                x = index_space * 77 + 21;
                if (sign_index > 0)
                    x -= sign_index * 7;
            }

            if (!isHu&&cardValue != MJCard.CIN && !MJCard.isSigned(cardValue, MJCard.SIGN_TANG)&&MJMatch.match.isMineRound())
            {
                int[] cards=MJMatch.match.getDianPaoCard();
                if (cards != null)
                {
                  
                    for (int i=0;i<cards.Length;i++)
                    {
                        if (cards[i] == cardValue)
                        {
                            dp.gameObject.SetActive(true);
                            break;
                        }
                    }
                }
            }

            if (isSelect)
                y = 30;

            int sycardnum = (index_space - (totalnum - replacenum));
            if (replacenum == 0 || totalnum == 0)
                sycardnum = -1;
            if (sycardnum >= 0)
            {
                x += 21;
                card_bg.sprite = MJWidgetManager.getInstance().getCardBg(2);
            }
            else
            {
                card_bg.sprite = MJWidgetManager.getInstance().getCardBg(0);
            }

            DisplayKit.setLocalY(card.gameObject, cardy);
            if (moCard != null&&moCard.isBrightCard())
            {
                card_bg.sprite = MJWidgetManager.getInstance().getCardBg(1);
                DisplayKit.setLocalY(card.gameObject, cardy+16);
                huDirection.gameObject.SetActive(true);
                DisplayKit.setLocalRoateZ(huDirection.transform, getdirection());

                DisplayKit.setWH(card_bg.transform, 70, 101);
                DisplayKit.setWH(card.transform, 58, 75);
                DisplayKit.setLocalY(card,15);
            }
            else if (cardValue!=MJCard.CIN&&MJCard.isSigned(cardValue, MJCard.SIGN_TANG))//躺下的牌
            {
                card_bg.sprite = MJWidgetManager.getInstance().getCardBg(1);
                DisplayKit.setLocalY(card.gameObject, cardy + 16);
                DisplayKit.setWH(card_bg.transform, 70, 101);
                DisplayKit.setWH(card.transform, 58, 75);
                DisplayKit.setLocalY(card, 15);
            }
            else
            {
                DisplayKit.setWH(card_bg.transform, 76, 118);
                DisplayKit.setWH(card.transform, 70, 91);
                DisplayKit.setLocalY(card, -6f);
            }

            DisplayKit.setLocalXY(transform, x, y);

            if (tinginfo != null && ting != null)
            {
                ting.ShowIndex(tinginfo.type);
                ting.setVisible(true);
            }
            else
                ting.setVisible(false);

            bool isshow = false;
            if (distype != MJCard.CNO && hasDistypeCard && MJCard.getType(cardValue) != distype)
                isshow = true;
            mask.gameObject.SetActive(isshow);
        }



        protected override void right()
        {
            float y = index_space * 16;
            if (cardValue != MJCard.CIN && MJCard.isSigned(cardValue, MJCard.SIGN_TANG))//躺下的牌
            {
                if (isHu)
                {
                    y = index_space * 28;
                    y += 10;
                }
                else
                {
                    if (index_space != sign_index)
                        y += 18;
                    if (sign_index > 0)
                        y += sign_index * 11;
                }
              
                card_bg.sprite = MJWidgetManager.getInstance().getCardBg(11);
                DisplayKit.setWH(card_bg.transform, 43, 41);
            }
            else if (moCard == null && isHu)
            {
                card_bg.sprite = MJWidgetManager.getInstance().getCardBg(12);
                DisplayKit.setWH(card_bg.transform, 43, 41);
                y = index_space * 28;
            }
            else
            {
                card_bg.sprite = MJWidgetManager.getInstance().getCardBg(10);
                DisplayKit.setWH(card_bg.transform, 23, 42);
            }
          
           
            if (moCard!=null)
            {
                 y += 18;
                if (sign_tang_num > 0)
                    y += sign_tang_num * 11+15;


                if (moCard.isBrightCard())
                {
                    y = index_space * 28 + 14;
                    if (sign_tang_num > 0)
                        y += 3;
                    card_bg.sprite = MJWidgetManager.getInstance().getCardBg(11);
                    huDirection.gameObject.SetActive(true);
                    DisplayKit.setLocalRoateZ(huDirection.transform,getdirection());
                    DisplayKit.setWH(card_bg.transform, 43, 41);
                }
                else if (isHu)
                {
                    y = index_space * 28 + 14;
                    if (sign_tang_num > 0)
                        y += 3;
                    card_bg.sprite = MJWidgetManager.getInstance().getCardBg(12);
                    DisplayKit.setWH(card_bg.transform, 43, 41);
                }
                else
                {
                    DisplayKit.setWH(card_bg.transform, 23, 42);
                }
            }
            DisplayKit.setLocalY(this, y);
            transform.SetAsLastSibling();
        }

        protected override void top()
        {
            float x = -index_space * 41;
            if (cardValue != MJCard.CIN && MJCard.isSigned(cardValue, MJCard.SIGN_TANG)) //躺下的牌
            {
                x = -index_space * 41 - 10;
                card_bg.sprite = MJWidgetManager.getInstance().getCardBg(21);
                DisplayKit.setWH(card_bg.transform, 41, 60);

            }
            else if (moCard == null && isHu)
            {
                card_bg.sprite = MJWidgetManager.getInstance().getCardBg(22);
                DisplayKit.setWH(card_bg.transform, 41, 60);
                //x += 5*index_space;
            }
            else
            {
                card_bg.sprite = MJWidgetManager.getInstance().getCardBg(20);
                DisplayKit.setWH(card_bg.transform, 41, 62);
            }

            if (moCard != null)
            {
                x -= 12;

                if (sign_tang_num > 0)
                    x -= sign_tang_num * 3;

                if (moCard.isBrightCard())
                {
                    card_bg.sprite = MJWidgetManager.getInstance().getCardBg(21);
                    huDirection.gameObject.SetActive(true);
                    DisplayKit.setLocalRoateZ(huDirection.transform,getdirection());
                    DisplayKit.setWH(card_bg.transform, 41, 60);
                    //x += 5 * index_space;
                }
                else if (isHu)
                {
                    card_bg.sprite = MJWidgetManager.getInstance().getCardBg(22);
                    DisplayKit.setWH(card_bg.transform, 41, 60);
                }
                else
                {
                    DisplayKit.setWH(card_bg.transform, 41, 62);
                }
            }
            DisplayKit.setLocalX(this, x);
        }

        protected override void left()
        {
            float y = -index_space * 16;

            if (cardValue != MJCard.CIN && MJCard.isSigned(cardValue, MJCard.SIGN_TANG))//躺下的牌
            {
                if (isHu)
                {
                    y = -index_space * 28;
                    y -= 10;
                }
                else
                {

                    if (index_space != sign_index)
                    {
                        y -= 18;
                    }
                       
                    if (sign_index > 0)
                        y -= sign_index * 11;
                }

                card_bg.sprite = MJWidgetManager.getInstance().getCardBg(31);
                DisplayKit.setWH(card_bg.transform, 43, 41);
            }

            else if (moCard == null&&isHu)
            {
                card_bg.sprite = MJWidgetManager.getInstance().getCardBg(32);
                DisplayKit.setWH(card_bg.transform, 43, 41);
                y = -index_space * 28;
            }
            else
            {
               
                card_bg.sprite = MJWidgetManager.getInstance().getCardBg(30);
                DisplayKit.setWH(card_bg.transform,23,42);
            }

            if (moCard != null)
            {
                y -= 18;
               
                if (sign_tang_num > 0)
                    y -= sign_tang_num * 8 + 30;

                if (moCard.isBrightCard())
                {
                    y = -index_space * 28-14;
                    if (sign_tang_num > 0)
                        y -= 3;
                    card_bg.sprite = MJWidgetManager.getInstance().getCardBg(31);
                    DisplayKit.setWH(card_bg.transform, 43, 41);
                    huDirection.gameObject.SetActive(true);
                    DisplayKit.setLocalRoateZ(huDirection.transform, getdirection());
                }
                else if (isHu)
                {
                    y = -index_space * 28 - 14;
                    if (sign_tang_num > 0)
                        y -= 3;
                    card_bg.sprite = MJWidgetManager.getInstance().getCardBg(32);
                    DisplayKit.setWH(card_bg.transform, 43, 41);
                }
                else
                {
                    
                    DisplayKit.setWH(card_bg.transform, 23, 42);
                }
            }
            DisplayKit.setLocalY(this, y);
        }
    }
}
