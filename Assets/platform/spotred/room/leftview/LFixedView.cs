using platform.spotred;
using UnityEngine;

namespace platform.spotred.room
{
    public class LFixedView: BaseFixedView
    {
        private UnrealDivTableContainer container;
        protected override void xinit()
        {
            base.xinit();
            this.container = this.transform.Find("group").GetComponent<UnrealDivTableContainer>();
            this.container.init();
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

            Vector2 pos=Vector2.one;

            for (int i = 0; i < fixedCards.Length; i++)
            {
                int[] ca = fixedCards[i].getCards();
                count += ca.Length;

                for (int j = 0; j < ca.Length; j++)
                {
                    LFixedBar bar = (LFixedBar)this.container.bars[indexs];
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
                if (count / GameConfig.line_max_left > 0)
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

            int line = 0;

            int indexs = 0;

            for (int i = 0; i < cards.Length; i++)
            {
                int[] ca = cards[i].getCards();
                count += ca.Length;
                for (int j = 0; j < ca.Length; j++)
                {
                    LFixedBar bar = (LFixedBar)this.container.bars[indexs];
                    bar.setData(ca[j], curr_count, line);
                    bar.index_space = indexs;
                    bar.refresh();
                    indexs++;
                }
                if (count / GameConfig.line_max_left > 0)
                {
                    line++;
                    curr_count += count;
                    count = 0;
                }
            }
        }
    }
}
