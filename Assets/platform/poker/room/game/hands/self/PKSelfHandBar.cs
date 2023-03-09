using UnityEngine.UI;

namespace platform.poker
{
    /// <summary> 玩家手牌bar </summary>
    public class PKSelfHandBar : UnrealLuaSpaceObject
    {
        /* 核心数据 */
        protected int card;                           //牌值

        /* 显示相关 状态值 */
        private bool dizhu;                         //是否显示 地主
        private bool mingpai;                       //是否显示 明牌
        protected bool selected;                      //是否选择
        public bool mask;                           //是否被选中
        public float posx;                          //位置

        /* 显示相关 组件 */
        private Image Cards;                        //图片
        private Image DiZhu;                        //状态 地主身份
        private Text MingPai;                       //状态 明牌身份
        private Image CardMask;                     //状态 选中状态

        protected override void xinit()
        {
            mask = false;
            dizhu = false;
            mingpai = false;
            selected = false;

            CardMask = transform.Find("mask").GetComponent<Image>();
            Cards = transform.Find("card").GetComponent<Image>();
            DiZhu = transform.Find("dizhu").GetComponent<Image>();
            MingPai = transform.Find("mingpai").GetComponent<Text>();
        }

        protected override void xrefresh()
        {
            if (PKGAME.RULESID == PKGAME.DDZ)
            {
                dizhu = isLastCard() && (DDZMatch.match.diZhuIndex == DDZMatch.match.mindex);
            }
            else dizhu = false;

            refreshHight();
            DisplayKit.setLocalX(this, posx);

            DiZhu.gameObject.SetActive(dizhu);
            CardMask.gameObject.SetActive(mask);
            MingPai.gameObject.SetActive(mingpai);
            Cards.sprite = PokerCardManager.instance.getHandPoker(card);
        }

        protected virtual void refreshHight()
        {
            if (selected)
            {
                if (!PKRoomPanel.Panel.selectCard.contain(card))
                {
                    PKRoomPanel.Panel.selectCard.add(card);
                    DisplayKit.setLocalY(this, PKHandsBean.UpHight);
                }
            }
            else
            {
                PKRoomPanel.Panel.selectCard.remove(card);
                DisplayKit.setLocalY(this, 0);
            }
        }

        /// <summary> 遮罩状态 </summary>
        public void setMask(bool selcet)
        {
            this.mask = selcet;
        }

        /// <summary> 设置显示明牌状态 </summary>
        public void setMingPai(bool mingpai)
        {
            this.mingpai = mingpai;
        }

        /// <summary> 选中状态 </summary>
        public void Select()
        {
            selected = !selected;
        }

        /// <summary> 设置手牌值 </summary> 根据值取卡牌图片
        public void setCard(int card)
        {
            this.card = card;
        }

        /// <summary> 选择重置 </summary>
        public virtual void resetSelect()
        {
            selected = false;
            DisplayKit.setLocalY(this, 0);
        }

        /// <summary> 判断牌是当前玩家最后一张牌 </summary>
        private bool isLastCard()
        {
            return card == DDZMatch.match.getSelfHandMaxCard();
        }

        /// <summary> 只有提示調用 </summary>
        public void isSelect(bool selected)
        {
            this.selected = selected;
        }
    }
}
