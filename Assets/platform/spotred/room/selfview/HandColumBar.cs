using UnityEngine;

namespace platform.spotred.room
{
    /// <summary>
    /// 手牌区每列
    /// </summary>
    public class HandColumBar: UnrealLuaSpaceObject
    {
        [HideInInspector]public UnrealDivTableContainer container;


        private int[] cards;

        private int[] disablecard;

        public int total_colum;

        protected override void xinit()
        {
            this.container = transform.Find("single_1").GetComponent<UnrealDivTableContainer>();
            this.container.init();
        }

        protected override void xrefresh()
        {
            int midle = total_colum / 2;

            if (total_colum % 2 >= 1)//奇数
            {
                midle++;
                if (index_space < midle)
                    DisplayKit.setLocalXY(this.transform, -this.getWidth() * (midle - 1 - index_space), 0);
                else if (index_space >= midle)
                    DisplayKit.setLocalXY(this.transform, this.getWidth() * (index_space - midle + 1), 0);
                else
                    DisplayKit.setLocalXY(this.transform, 0, 0);
            }
            else
            {
                if (index_space < midle)
                    DisplayKit.setLocalXY(this.transform, -this.getWidth() * (midle - index_space) + this.getWidth() / 2, 0);
                else if (index_space >= midle)
                    DisplayKit.setLocalXY(this.transform, this.getWidth() * (index_space - midle) + this.getWidth() / 2, 0);
            }


            //DisplayKit.setLocalXY(this.transform, this.getWidth() * index_space, 0);




            container.resize(cards.Length);

            for (int i = 0; i < cards.Length; i++)
            {
                HandCardBar bar = (HandCardBar)this.container.bars[i];
                bar.index_space = i;
                bar.column = this.index_space;
                bar.refreshCard(cards[i], cards.Length, isChu(cards[i]));
                bar.refresh();
                bar.setLocalRotation(0);
            }
        }

        /// <summary>
        /// 发牌时需要用
        /// </summary>
        /// <returns></returns>
        public Transform[] getCardImage()
        {
            Transform[] images=new Transform[this.container.size];
            for (int i = 0; i < images.Length; i++)
            {
                images[i] = ((HandCardBar)this.container.bars[i]).getCardImge();
            }
            return images;
        }

        public void hideCheck(int index)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                HandCardBar bar = (HandCardBar)this.container.bars[i];
                if(i!=index)  bar.hideCheck();
            }
        }


        /// <summary>
        /// 此牌能不能出
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool isChu(int card)
        {
            for (int i = 0; i < disablecard.Length; i++)
            {
                if (card == disablecard[i]) return false;
            }

            return true;
        }

        public void showColumn(int[] cards,int[] disablecard)
        {
            this.cards = cards;
            this.disablecard = disablecard;
        }

        /// <summary>
        /// 展开牌(index,如果不是则复原)
        /// </summary>
        /// <param name="index"></param>
        public void UpCard(int index)
        {
            if (cards == null || cards.Length == 0) return;
            for (int i = 0; i < this.cards.Length; i++)
            {
                HandCardBar bar = (HandCardBar)this.container.bars[i];
                Vector3 location = this.transform.localPosition;
               
                if (index == this.index_space)
                {
                    bar.UpCard();
                }
                else
                {
                    bar.resize();
                }
            }
        }
    }
}
