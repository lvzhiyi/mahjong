using UnityEngine.UI;

namespace platform.poker
{
    /// <summary> 其他玩家手牌 </summary>
    public class PKOtherHandBar : UnrealLuaSpaceObject
    {
        private Image cardImg;                     //图片
        private Image dizhuImg;                    //状态 地主身份
        private Text mingpaiImg;                   //状态 明牌身份

        private int card;                          //牌值
        private bool dizhu;                        //是否显示 地主
        private bool mingpai;                      //是否显示 明牌

        protected override void xinit()
        {
            dizhu = false;
            mingpai = false;

            cardImg = transform.Find("card").GetComponent<Image>();
            dizhuImg = transform.Find("dizhu").GetComponent<Image>();
            mingpaiImg = transform.Find("mingpai").GetComponent<Text>();
        }

        protected override void xrefresh()
        {
            dizhuImg.gameObject.SetActive(dizhu);
            mingpaiImg.gameObject.SetActive(mingpai);

            cardImg.sprite = PokerCardManager.instance.getHandPoker(card);

            computePos(index_space);
        }

        public void setCard(int card)
        {
            this.card = card;
        }

        public void setDiZhu(bool dizhu)
        {
            this.dizhu = dizhu;
        }

        public void setMingPai(bool mingpai)
        {
            this.mingpai = mingpai;
        }

        /*------------------设置牌的位置------------------*/

        const int point = -220;
        const int acrossnum = 12;
        const float vertical = 40;
        const float horizontal = 40;

        float pointx;
        float pointy;

        protected virtual void computePos(int index)
        {
            if (index < acrossnum)
            {
                pointx = point + index * horizontal;
                pointy = vertical;
            }
            else
            {
                pointx = point + (index - acrossnum) * horizontal;
                pointy = -vertical;
            }
            DisplayKit.setLocalX(this,pointx);
            DisplayKit.setLocalY(this,pointy);
        }
    }
}
