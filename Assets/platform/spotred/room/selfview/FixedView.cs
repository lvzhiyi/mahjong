using System;
using platform.spotred;
using UnityEngine;

namespace platform.spotred.room
{
    /// <summary>
    /// 固定牌（这里是吃牌,对牌,偷牌）
    /// </summary>
    public class FixedView: BaseFixedView
    {
        private UnrealDivTableContainer container;
        protected override void xinit()
        {
            base.xinit();
            this.container = transform.Find("group").GetComponent<UnrealDivTableContainer>();
            this.container.init();
        }

        public override void setOperateCards(FixedCards[] fixedcards, Action action)
        {
            this.setSlibingIndex(5);
            base.setOperateCards(fixedcards, action);
        }

        /// <summary>
        /// 获取倒数第几张牌的位置
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public override Vector2 getLastCardsPostion(int index)
        {
            return this.container.bars[index].transform.localPosition;
        }

        public override Vector2 refreshCard(FixedCards[] fixedCards)
        {
            if (fixedCards == null)
            {
                this.container.resize(0);
                return Vector2.zero;
            }

            this.container.resize(getFixedCardsCount(fixedCards));

            int count = 0;

            int curr_count = 0;

            int line = 0;

            int indexs = 0;

            Vector2 pos=Vector2.zero;

            for (int i = 0; i < fixedCards.Length; i++)
            {
                int[] ca = fixedCards[i].getCards();
                count += ca.Length;

                for (int j = 0; j < ca.Length; j++)
                {
                    FixedBar bar = (FixedBar)this.container.bars[indexs];
                    bar.setData(ca[j], curr_count, line);
                    bar.index_space = indexs;
                    bar.refresh();
                    indexs++;
                    if (i == fixedCards.Length - 1)
                    {
                        bar.setVisible(false);
                    }

                    if (i == fixedCards.Length - 1 && j == 0)
                        pos = bar.transform.localPosition;
                }

                if (count / GameConfig.line_max_bottom > 0)
                {
                    line++;
                    curr_count += count;
                    count = 0;
                }
            }
            
            return pos;
        }

        protected override void xrefresh()
        {
            if (cards == null)
            {
                this.container.resize(0);
                return;
            }

            this.container.resize(getFixedCardsCount(cards));

            int count = 0;

            int curr_count = 0;

            int line=0;

            int indexs = 0;

            for (int i = 0; i < cards.Length; i++)
            {
                int[] ca= cards[i].getCards();
                count += ca.Length;

                for (int j=0;j<ca.Length;j++)
                {
                    FixedBar bar = (FixedBar)this.container.bars[indexs];
                    bar.setData(ca[j], curr_count, line);
                    bar.index_space = indexs;
                    bar.refresh();
                    bar.setVisible(true);
                    indexs++;
                }

                if (count / GameConfig.line_max_bottom > 0)
                {
                    line++;
                    curr_count += count;
                    count = 0;
                }
            }

            this.resetSlibingIndex();
        }
    }
}
