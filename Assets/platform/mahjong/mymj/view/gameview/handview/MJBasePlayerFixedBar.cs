using platform.spotred;
using UnityEngine;
using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJBasePlayerFixedBar:UnrealLuaSpaceObject
    {
        public const int LEFT_DIRECTION = 0, RIGHT_DERECTION = 180, DOWN_DERECTION = 90, TOP_DERCTION = 270;
        /// <summary>
        /// 间隔宽度
        /// </summary>
        public float intervalwidth;
        /// <summary>
        /// 位置
        /// </summary>
        protected int postion;
        /// <summary>
        /// 牌面值左
        /// </summary>
        protected Image card1;
        /// <summary>
        /// 牌面值中
        /// </summary>
        protected Image card2;
        /// <summary>
        /// 牌面值右
        /// </summary>
        protected Image card3;
        /// <summary>
        /// 牌面值上
        /// </summary>
        protected Image card4;
        /// <summary>
        /// 牌面背景上
        /// </summary>
        protected Image bg1;
        /// <summary>
        /// 牌面背景上
        /// </summary>
        protected Image bg2;
        /// <summary>
        /// 牌面背景上
        /// </summary>
        protected Image bg3;
        /// <summary>
        /// 牌面背景上
        /// </summary>
        protected Image bg4;
        /// <summary>
        /// 方向指向图片
        /// </summary>
        protected Image direction;
        /// <summary>
        /// 吧图标
        /// </summary>
        protected Image ba;
        /// <summary>
        /// 牌面值
        /// </summary>
        protected MJFixedCards cardValue;

        public Color normalColor = new Color(255, 255, 255, 255);

        public Color waitColor = new Color(255, 255, 255, 120);

        protected override void xinit()
        {
            card1 = transform.Find("card").GetComponent<Image>();
            card2 = transform.Find("card2").GetComponent<Image>();
            card3 = transform.Find("card3").GetComponent<Image>();
            card4 = transform.Find("card4").GetComponent<Image>();
            bg1 = transform.Find("bg").GetComponent<Image>();
            bg2 = transform.Find("bg2").GetComponent<Image>();
            bg3 = transform.Find("bg3").GetComponent<Image>();
            bg4 = transform.Find("bg4").GetComponent<Image>();
            direction = transform.Find("direc").Find("direction").GetComponent<Image>();
            ba = transform.Find("direc").Find("ba").GetComponent<Image>();
        }

        public virtual void setCardValue(MJFixedCards cardValue, int postion)
        {
            this.cardValue = cardValue;
            this.postion = postion;
        }

        protected override void xrefresh()
        {
            card1.sprite = MJWidgetManager.getInstance().getCard(cardValue.cards[0]);
            card2.sprite = MJWidgetManager.getInstance().getCard(cardValue.cards[1]);
            card3.sprite = MJWidgetManager.getInstance().getCard(cardValue.cards[2]);

            card4.gameObject.SetActive(true);
            bg4.gameObject.SetActive(true);

            direction.gameObject.SetActive(true);

            card1.gameObject.SetActive(true);
            card2.gameObject.SetActive(true);
            card3.gameObject.SetActive(true);

            if (cardValue.type == FixedCards.MJPONG)
            {
                card4.gameObject.SetActive(false);
                bg4.gameObject.SetActive(false);
            }
            else if (cardValue.type == FixedCards.MJKONG_PRI)//暗杠
            {
                card1.gameObject.SetActive(false);
                card2.gameObject.SetActive(false);
                card3.gameObject.SetActive(false);
                direction.gameObject.SetActive(false);
                int c = cardValue.cards[3];
                card4.sprite = MJWidgetManager.getInstance().getCard(c);
            }
            else
            {
                int c = cardValue.cards[3];
                card4.sprite = MJWidgetManager.getInstance().getCard(c);
            }

            if (cardValue.type == FixedCards.MJONG_SUP_WAIT)
            {
                card4.color = waitColor;
                bg4.color = waitColor;
            }
            else
            {
                card4.color = normalColor;
                bg4.color = normalColor;
            }

            ba.gameObject.SetActive(cardValue.type == FixedCards.MJKONG_SUP);

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
            DisplayKit.setLocalRoateXYZ(direction.transform, 0, 0, getdirection());
        }

        public virtual int getdirection()
        {
            switch (MahJongPanel.getPlayerIndex(cardValue.source))
            {
                case MahJongPanel.LOC_DOWN:
                    return DOWN_DERECTION;
                case MahJongPanel.LOC_RIGHT:
                    return RIGHT_DERECTION;
                case MahJongPanel.LOC_TOP:
                    return TOP_DERCTION;
                case MahJongPanel.LOC_LEFT:
                    return LEFT_DIRECTION;
            }
            return 0;
        }

        public virtual void down()
        {
            showPribg();
            float x = intervalwidth * index_space;
            DisplayKit.setLocalX(this, x);
        }

        public virtual void right()
        {
            showPribg();
            float y = intervalwidth * index_space;
            DisplayKit.setLocalY(this, y);
        }

        public virtual void top()
        {
            showPribg();
            float x = -intervalwidth * index_space;
            DisplayKit.setLocalX(this, x);
        }

        public virtual void left()
        {
            showPribg();
            float y = -intervalwidth * index_space;
            DisplayKit.setLocalY(this, y);
        }


        public virtual void showPribg()
        {
            int index = postion * 10;

            bg4.sprite = MJWidgetManager.getInstance().getCardBg(index + 1);

            if (cardValue.type == FixedCards.MJKONG_PRI)
            {
                index += 2;
                bg1.sprite = MJWidgetManager.getInstance().getCardBg(index);
                bg2.sprite = MJWidgetManager.getInstance().getCardBg(index);
                bg3.sprite = MJWidgetManager.getInstance().getCardBg(index);
            }
            else
            {
                index += 1;
                bg1.sprite = MJWidgetManager.getInstance().getCardBg(index);
                bg2.sprite = MJWidgetManager.getInstance().getCardBg(index);
                bg3.sprite = MJWidgetManager.getInstance().getCardBg(index);
            }
        }
    }
}
