using cambrian.common;
using UnityEngine;

namespace platform.spotred.room
{
    /// <summary>
    /// 手牌区
    /// </summary>
    public class HandView : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 手牌有多少列集合
        /// </summary>
        [HideInInspector] public UnrealDivTableContainer column;

        //临时这样做
        public int[] handCard_1;
        public int[] disableCard_1;
        public int[] dulichu_1;

        //每张牌中心点的位置Y：Screen.heigth/2+24-bar.heigth;
        //每张牌中心点的位置X: MathKit.abs(hand.x)-bar.x  hand.x 是负数 

        private Queue<Transform> queue;

        public bool noReplay;

        protected override void xinit()
        {
            this.column = transform.Find("single").GetComponent<UnrealDivTableContainer>();
            this.column.init();
        }

        protected override void xrefresh()
        {
            this.column.resize(0);
        }

        //
        public void DealCard(int[] handCard)
        {
            bool b = Room.room.roomRule.rule.getRuleAtribute("is25single");
            ArrayList<int[]> cards = CardsManager.getHand(handCard, b);


            float height = MathKit.abs(this.transform.localPosition.y);

            int hand_x = MathKit.abs((int) transform.localPosition.x);

            this.column.resize(cards.count);


            queue = new Queue<Transform>(handCard.Length);

            for (int i = 0; i < cards.count; i++)
            {
                HandColumBar bar = (HandColumBar) column.bars[i];
                bar.index_space = i;
                bar.showColumn(cards.get(i), new int[0]);
                bar.refresh();
                bar.setVisible(false);

                Transform[] cardsImg = bar.getCardImage();
                for (int j = 0; j < cardsImg.Length; j++)
                {
                    Transform cimg = cardsImg[j];

                    DisplayKit.setLocalXY(cimg, hand_x - bar.transform.localPosition.x,
                        height - cimg.parent.localPosition.y+56);
                    cimg.localScale=new Vector3(0.1f,0.1f,1);
                    queue.add(cimg);
                }
            }

            for (int i = 0; i < cards.count; i++)
            {
                HandColumBar bar = (HandColumBar) this.column.bars[i];
                bar.setVisible(true);
            }
        }


        /// <summary>
        /// 刷新手牌
        /// </summary>
        /// <param name="handCard"></param>
        public virtual void showHandCard(int[] handCard, int[] disableCard)
        {
            ArrayList<int[]> cards = CardsManager.getHand(handCard, CPMatch.match.getRoomRule().rule.getRuleAtribute("is25single"));

            this.column.resize(cards.count);
            for (int i = 0; i < cards.count; i++)
            {
                HandColumBar bar = (HandColumBar) this.column.bars[i];
                bar.index_space = i;
                bar.total_colum = cards.count;
                bar.showColumn(cards.get(i), disableCard);
                bar.refresh();
            }
        }

        /// <summary>
        /// 刷新手牌 dulichu:这个是独立出来的牌
        /// </summary>
        /// <param name="handCard"></param>
        public virtual void showHandCard(int[] handCard, int[] disableCard, int[] dulichu)
        {
            this.handCard_1 = handCard;
            this.disableCard_1 = disableCard;
            this.dulichu_1 = dulichu;

            ArrayList<int[]> cards = CardsManager.getHand(handCard, CPMatch.match.getRoomRule().rule.getRuleAtribute("is25single"));

            int len = cards.count + 2;

            this.column.resize(len);

            for (int i = 0; i < len; i++)
            {
                HandColumBar bar = (HandColumBar) this.column.bars[i];
                bar.index_space = i;
                bar.total_colum = len;
                if (i < cards.count)
                {
                    bar.showColumn(cards.get(i), disableCard);
                }
                else if (i == len - 1)
                {
                    bar.showColumn(dulichu, disableCard);
                }
                else
                {
                    bar.showColumn(new int[0], disableCard);
                }

                bar.refresh();
            }
        }


        /// <summary>
        /// 出index不隐藏外,其余都隐藏
        /// </summary>
        /// <param name="column"></param>
        /// <param name="index"></param>
        public void hideCheck(int column, int index)
        {
            ((HandColumBar) this.column.bars[column]).hideCheck(index);
        }

        /// <summary>
        /// 展开某列的牌(当index=-1时,全部复原)
        /// </summary>
        public void UpColumnCard(int index)
        {
            for (int i = 0; i < this.column.count; i++)
            {
                HandColumBar bar = (HandColumBar) this.column.bars[i];
                bar.resetSlibingIndex();
                bar.UpCard(index);
            }
        }

        

        public int disOverCount;


        public Vector3 dest=new Vector3(0,-56,1);

        void Update()
        {
            if (queue == null) return;
            if (!queue.isEmpty())
            {
                Transform img = queue.remove();
                img.GetComponent<PositionTweener>().start(dest, 2000, () =>
                {
                    disOverCount++;
                }, (Transform t) =>
                {
                    Vector3 scale= t.localScale;
                    scale.x += 0.8f;
                    scale.y += 0.8f;

                    if (scale.x >= 1)
                    {
                        scale.x = 1;
                        scale.y = 1;
                    }
                    t.localScale = scale;
                });
            }
        }
    }
}
