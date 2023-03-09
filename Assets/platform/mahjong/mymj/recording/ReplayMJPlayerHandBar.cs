namespace platform.mahjong
{
    public class ReplayMJPlayerHandBar:MJPlayerHandBar
    {
        /// <summary>
        /// 设置牌
        /// </summary>
        /// <param name="card"></param>
        /// <param name="postion">位置</param>
        /// <param name="isMoCard">是否是摸牌</param>
        public override void setCard(bool isHu, int card, int postion, MJMOCard moCard, bool isSelect, TingCardsInfo tinginfo, int distype, bool hasDistypeCard,int sign_tang_count,int sign_index)
        {
            totalnum = 0;
            replacenum = 0;
            this.isHu = isHu;

            this.postion = postion;
            this.moCard = moCard;
            if (moCard != null)
                card = moCard.card;
            cardValue = card;
            sign_tang_num = sign_tang_count;
            this.sign_index = sign_index;
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
            this.totalnum = total;
            this.replacenum = replacenum;
            this.moCard = null;
        }


        protected override void xrefresh()
        {
            huDirection.gameObject.SetActive(false);

            if (MJCard.isSigned(cardValue, MJCard.SIGN_TANG))
            {
                tangimg.gameObject.SetActive(true);
                card.sprite = MJWidgetManager.getInstance().getCard(MJCard.cancelSign(cardValue, MJCard.SIGN_TANG));
            }
            else
            {
                tangimg.gameObject.SetActive(false);
                card.sprite = MJWidgetManager.getInstance().getCard(cardValue);
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

        /// <summary> 
        protected override void down()
        {
            card_bg.sprite = MJWidgetManager.getInstance().getCardBg(1);
            float x = index_space * 76;
            float y = 0;
            if (moCard != null)
                x += 21;
            if (isSelect)
                y = 30;

            int sycardnum = (index_space - (totalnum - replacenum));
            if (replacenum == 0 || totalnum == 0)
                sycardnum = -1;
            if (sycardnum >= 0)
            {
                x += 21;
            }


            if (MJCard.isSigned(cardValue, MJCard.SIGN_TANG))
            {
                x += 10;
            }

            DisplayKit.setLocalY(card.gameObject, cardy);
            if (moCard != null && moCard.isHu())
            {
                huDirection.gameObject.SetActive(true);
                DisplayKit.setLocalRoateZ(huDirection.transform, getdirection());
            }

            DisplayKit.setLocalXY(transform, x, y);


        }

        protected override void right()
        {
            float y = index_space * 28;

            if (moCard != null)
            {
                y += 18;
                if (moCard.isHu())
                {
                    huDirection.gameObject.SetActive(true);
                    DisplayKit.setLocalRoateZ(huDirection.transform, getdirection());
                }
            }

            if (MJCard.isSigned(cardValue, MJCard.SIGN_TANG))
            {
                y += 10;
            }

            card_bg.sprite = MJWidgetManager.getInstance().getCardBg(11);
            DisplayKit.setLocalY(this, y);
            transform.SetAsLastSibling();
        }

        protected override void top()
        {
            float x = -index_space * 41;

            if (moCard != null)
            {
                x -= 12;
                if (moCard.isHu())
                {
                    huDirection.gameObject.SetActive(true);
                    DisplayKit.setLocalRoateZ(huDirection.transform, getdirection());
                }
            }

            if (MJCard.isSigned(cardValue, MJCard.SIGN_TANG))
            {
                x -= 10;
            }
            card_bg.sprite = MJWidgetManager.getInstance().getCardBg(21);
            DisplayKit.setLocalX(this, x);
        }

        protected override void left()
        {
            float y = -index_space * 28;
            if (moCard != null)
            {
                y -= 18;
                if (moCard.isHu())
                {
                    y = -index_space * 28 - 14;
                    huDirection.gameObject.SetActive(true);
                    DisplayKit.setLocalRoateZ(huDirection.transform, getdirection());
                }
            }

            if (MJCard.isSigned(cardValue, MJCard.SIGN_TANG))
            {
                y -= 10;
            }
            card_bg.sprite = MJWidgetManager.getInstance().getCardBg(31);
            DisplayKit.setLocalY(this, y);
        }
    }
}
