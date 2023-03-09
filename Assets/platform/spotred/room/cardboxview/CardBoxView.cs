using UnityEngine.UI;

namespace platform.spotred.room
{
    /// <summary>
    /// 牌堆view
    /// </summary>
    public class CardBoxView:UnrealLuaSpaceObject
    {
       
        /// <summary>
        /// 牌堆容器
        /// </summary>
        private UnrealDivTableContainer container;
        /// <summary>
        /// 剩余牌数量
        /// </summary>
        private UnrealTextField cardsnum;
        /// <summary>
        /// 剩余4张牌时提醒
        /// </summary>
        private Image shengyucard;

        private int cardboxnum=5;

        protected override void xinit()
        {
           
            this.shengyucard = this.transform.Find("bg_1").GetComponent<Image>();
            this.cardsnum = this.transform.Find("cardsnum").GetComponent<UnrealTextField>();
            this.cardsnum.init();
            this.container = this.transform.Find("container").GetComponent<UnrealDivTableContainer>();
            this.container.init();
        }
       

        public void setCardNum(int num)
        {
            this.cardboxnum = num;
            refreshCardNum();
        }

        private bool b = false;
        /// <summary>
        /// 刷新剩余的牌
        /// </summary>
        public void refreshCardNum()
        {
            cardsnum.text = this.cardboxnum+"";
            this.container.resize(cardboxnum / 5!=0?cardboxnum/5:1);
            for (int i = 0; i < container.size; i++)
            {
                CardBoxBar bar = (CardBoxBar)this.container.bars[i];
                bar.index_space = i;
                bar.refresh();
            }

            if (this.cardboxnum < 5&&!b)
            {
                this.shengyucard.gameObject.SetActive(true);
                InvokeRepeating("tixingcard", 1, 1);
                b = true;
            }
        }

        void tixingcard()
        {
            this.shengyucard.gameObject.SetActive(!this.shengyucard.gameObject.activeSelf);
        }


        protected override void xrefresh()
        {
           
            this.shengyucard.gameObject.SetActive(false);
           
            CancelInvoke("tixingcard");
            b = false;
        }
    }
}
