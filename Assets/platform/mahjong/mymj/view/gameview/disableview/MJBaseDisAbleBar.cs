using UnityEngine;
using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJBaseDisAbleBar:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 每行显示的数量
        /// </summary>
        public int linecount = 10;
        /// <summary>
        /// 牌之间的宽度
        /// </summary>
        public int cardwidth;
        /// <summary>
        /// 牌之间的高度
        /// </summary>
        public float cardheight;
        /// <summary>
        /// 位置(上,下，左，右)
        /// </summary>
        protected int postion;
        /// <summary>
        /// 麻将牌
        /// </summary>
        protected Image cardicon;
        /// <summary>
        /// 牌背景
        /// </summary>
        protected Image card_bg;
        /// <summary>
        /// 遮罩
        /// </summary>
        protected Image mask;
        /// <summary>
        /// 牌
        /// </summary>
        protected int card;
        /// <summary>
        /// 自己选中的牌
        /// </summary>
        protected int selectcard;
        /// <summary>
        /// 初始坐标
        /// </summary>
        protected Vector3 initvector3;

        public Color normalColor = new Color(255, 255, 255, 255);

        public Color waitColor = new Color(255, 255, 255, 180);

        protected override void xinit()
        {
            if (cardicon != null) return;
            cardicon = transform.Find("card").GetComponent<Image>();
            card_bg = transform.Find("bg").GetComponent<Image>();
            mask = transform.Find("mask").GetComponent<Image>();
            mask.gameObject.SetActive(false);
            initvector3 = transform.localPosition;
        }

        public virtual void setData(int card, int postion, int selectcard = MJCard.CNO)
        {
            this.card = card;
            this.postion = postion;
            this.selectcard = selectcard;
        }

        public virtual void setDataSelect(int selectcard)
        {
            mask.gameObject.SetActive(selectcard == MJCard.cancelSign(card, MJCard.SIGN_HU));
        }

        /// <summary>
        /// 第几行
        /// </summary>
        protected int line;
        /// <summary>
        /// 第几列
        /// </summary>
        protected int cols;

        protected override void xrefresh()
        {
            line = (index_space / linecount);
            cols = index_space % linecount;
            mask.gameObject.SetActive(selectcard == card);

            card_bg.sprite = MJWidgetManager.getInstance().getCardBg(1);

            if (MJCard.isSigned(card, MJCard.SIGN_HU))
            {
                card_bg.color = waitColor;
                int cards = MJCard.cancelSign(card, MJCard.SIGN_HU);
                cardicon.sprite = MJWidgetManager.getInstance().getCard(cards);
            }
            else
            {
                // cardicon.color = normalColor;
                card_bg.color = normalColor;
                cardicon.sprite = MJWidgetManager.getInstance().getCard(card);
            }


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


        public virtual void down()
        {
            float x = initvector3.x + cols * cardwidth;


            int l = line % 3;
            float y = initvector3.y + l * cardheight;

            if (line >= 3)
            {
                y += 18;
            }


            DisplayKit.setLocalXY(gameObject, x, y);
        }

        public virtual void right()
        {
            float y = initvector3.y + cols * cardheight;
            int l = line % 3;
            float x = initvector3.x - l * cardwidth;
            if (line >= 3)
            {
                y += 12;
            }

            DisplayKit.setLocalXY(gameObject, x, y);
        }

        public virtual void top()
        {
            float x = initvector3.x - cols * cardwidth;
            int l = line % 3;
            float y = initvector3.y - l * cardheight;
            DisplayKit.setLocalXY(gameObject, x, y);
        }

        public virtual void left()
        {
            float y = initvector3.y - cols * cardheight;

            int l = line % 3;

            float x = initvector3.x + l * cardwidth;
            if (line >= 3)
            {
                y += 12;
            }

            DisplayKit.setLocalXY(gameObject, x, y);
        }
    }
}
