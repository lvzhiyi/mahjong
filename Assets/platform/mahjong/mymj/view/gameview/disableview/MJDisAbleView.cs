namespace platform.mahjong
{
    /// <summary>
    /// 出牌区的牌
    /// </summary>
    public class MJDisAbleView:UnrealLuaSpaceObject
    {
        protected UnrealDivTableContainer top;

        protected UnrealDivTableContainer left;

        protected UnrealDivTableContainer down;

        protected UnrealDivTableContainer right;

        protected override void xinit()
        {
            top = transform.Find("top").GetComponent<UnrealDivTableContainer>();
            top.init();
            left = transform.Find("left").GetComponent<UnrealDivTableContainer>();
            left.init();
            down = transform.Find("down").GetComponent<UnrealDivTableContainer>();
            down.init();
            right = transform.Find("right").GetComponent<UnrealDivTableContainer>();
            right.init();

            //DisplayKit.setLocalScaleX(right.transform, 1 + UnrealRoot.root.addScaleX);
            //DisplayKit.setLocalScaleX(left.transform, 1 + UnrealRoot.root.addScaleX);
            //DisplayKit.setLocalScaleX(top.transform, 1 + UnrealRoot.root.addScaleX);
            //DisplayKit.setLocalScaleX(down.transform, 1 + UnrealRoot.root.addScaleX);
        }

        protected override void xrefresh()
        {
            top.resize(0);
            left.resize(0);
            down.resize(0);
            right.resize(0);
        }


        public MJBaseDisAbleBar getLastBar(int index)
        {
            int len = 0;
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    len = down.size-1;
                    if (len < 0) return null;
                    return (MJBaseDisAbleBar)down.bars[len];
                case MahJongPanel.LOC_RIGHT:
                    len = right.size-1;
                    if (len < 0) return null;
                    return (MJBaseDisAbleBar)right.bars[len];
                case MahJongPanel.LOC_TOP:
                    len = top.size-1;
                    if (len < 0) return null;
                    return (MJBaseDisAbleBar)top.bars[len];
                case MahJongPanel.LOC_LEFT:
                    if (len < 0) return null;
                    len = left.size-1;
                    return (MJBaseDisAbleBar)left.bars[len];
            }
            return null;
        }

        public void refreshDisableCards(int index,int[] cards)
        {
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    reDown(index,cards);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    reRight(index, cards);
                    break;
                case MahJongPanel.LOC_TOP:
                    reTop(index, cards);
                    break;
                case MahJongPanel.LOC_LEFT:
                    reLeft(index, cards);
                    break;
            }
        }

        /// <summary>
        /// 显示被选中的牌
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        public void refreshExistCard(int index,int card)
        {
            int len = 0;
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    len=down.count;
                    for (int i = 0; i < len; i++)
                    {
                        MJBaseDisAbleBar bar = (MJBaseDisAbleBar)down.bars[i];
                        bar.setDataSelect(card);
                    }
                    break;
                case MahJongPanel.LOC_RIGHT:
                    len = right.count;
                    for (int i = 0; i < len; i++)
                    {
                        MJBaseDisAbleBar bar = (MJBaseDisAbleBar)right.bars[i];
                        bar.setDataSelect(card);
                    }
                    break;
                case MahJongPanel.LOC_TOP:
                    len = top.count;
                    for (int i = 0; i < len; i++)
                    {
                        MJBaseDisAbleBar bar = (MJBaseDisAbleBar)top.bars[i];
                        bar.setDataSelect(card);
                    }
                    break;
                case MahJongPanel.LOC_LEFT:
                    len = left.count;
                    for (int i = 0; i < len; i++)
                    {
                        MJBaseDisAbleBar bar = (MJBaseDisAbleBar)left.bars[i];
                        bar.setDataSelect(card);
                    }
                    break;
            }
        }

        private void reDown(int postion, int[] cards, int selectCard = MJCard.CNO)
        {
            int len = cards.Length;
            down.resize(len);
            int line = 0;
            for (int i = 0; i < len; i++)
            {
                MJBaseDisAbleBar bar = (MJBaseDisAbleBar) down.bars[i];
                line = i / bar.linecount;
                bar.setData(cards[i], postion);
                bar.index_space = i;
                bar.refresh();
                if (line < 3)
                {
                    bar.transform.SetSiblingIndex((len - 1) - i);
                } 
                else
                {
                    int c = (i - line * bar.linecount);
                    bar.transform.SetSiblingIndex(len+c);
                }
            }
        }

        private void reRight(int postion, int[] cards, int selectCard = MJCard.CNO)
        {
            right.resize(cards.Length);
            int line = 0;
            int len = cards.Length;
            for (int i = 0; i < cards.Length; i++)
            {
                MJBaseDisAbleBar bar = (MJBaseDisAbleBar)right.bars[i];
                line = i / bar.linecount;
                bar.setData(cards[i], postion);
                bar.index_space = i;
                bar.refresh();
                if (line < 3)
                {
                    bar.transform.SetSiblingIndex((len - 1) - i);
                }
                else
                {
                    int c=(i-line* bar.linecount);
                    bar.transform.SetSiblingIndex(len-c-1);
                }
            }
        }

        private void reTop(int postion, int[] cards, int selectCard = MJCard.CNO)
        {
            top.resize(cards.Length);
            for (int i = 0; i < cards.Length; i++)
            {
                MJBaseDisAbleBar bar = (MJBaseDisAbleBar)top.bars[i];
                bar.setData(cards[i], postion);
                bar.index_space = i;
                bar.refresh();
            }
        }

        private void reLeft(int postion, int[] cards, int selectCard = MJCard.CNO)
        {
            left.resize(cards.Length);
            for (int i = 0; i < cards.Length; i++)
            {
                MJBaseDisAbleBar bar = (MJBaseDisAbleBar)left.bars[i];
                bar.setData(cards[i], postion);
                bar.index_space = i;
                bar.refresh();
            }
        }
    }
}
