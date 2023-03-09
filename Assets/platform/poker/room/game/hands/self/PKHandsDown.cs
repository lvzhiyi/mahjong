namespace platform.poker
{
    /// <summary> 当前客户端玩家手牌控制 </summary>
    public class PKHandsDown : UnrealLuaSpaceObject
    {
        private UnrealDivTableContainer container;
        private float downSpace = 0;
        private int[] cards;
        private float point;
        private bool mingpai;

        protected override void xinit()
        {
            point = 0;
            mingpai = false;
            cards = new int[0];
            container = transform.Find("single").GetComponent<UnrealDivTableContainer>();
            container.init();
            DisplayKit.setLocalScaleX(transform, 1);
        }

        protected override void xrefresh()
        {
            int len = cards.Length;
            container.resize(len);
            setPoint(len);
            for (int i = 0; i < cards.Length; i++)
            {
                handBarInit((PKSelfHandBar)container.bars[i], i);
            }
            container.resizeDelta();
            if (!gameObject.activeInHierarchy) setVisible(true);
        }

        /// <summary> 手牌属性初始化 </summary>
        private void handBarInit(PKSelfHandBar bar, int i)
        {
            bar.resetSelect();
            bar.setCard(cards[i]);
            bar.index_space = i;
            bar.posx = (int)(point + (i * downSpace));
            if (mingpai) bar.setMingPai(i == cards.Length - 1);
            else bar.setMingPai(false);
            bar.refresh();
        }

        /// <summary> 设置手牌中心点 </summary>
        private void setPoint(int cardLen)
        {
            if (cards.Length > 17)
                downSpace = 50;
            else if (cards.Length > 16)
                downSpace = 59;
            else if (cards.Length > 14)
                downSpace = 63;
            else if (cards.Length > 9)
                downSpace = 73;
            else
                downSpace = 76;
            cardLen = cardLen - 1;
            point = -(cardLen / 2) * downSpace;
        }

        /// <summary> 设置手牌值 </summary>
        public void setCards(int[] cards)
        {
            this.cards = cards;
        }

        /// <summary> 设置明牌状态 </summary>
        public void setMingPai(bool mingpai)
        {
            this.mingpai = mingpai;
        }

        /// <summary> 取消选择的手牌 </summary>
        public void deselectHandsCard()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                PKSelfHandBar bar = (PKSelfHandBar)container.bars[i];
                bar.resetSelect();
                bar.refresh();
            }
        }

        /// <summary> 取消选中遮罩 </summary>
        public void hideAllHandsMask()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                PKSelfHandBar bar = (PKSelfHandBar)container.bars[i];
                if (bar.mask)
                {
                    bar.Select();
                    bar.setMask(false);
                }
                bar.refresh();
            }
        }

        /// <summary> 刷新遮罩 </summary>
        public void refreshAllHandsMask(int start, int end)
        {
            int max = start > end ? start : end;
            int min = start > end ? end : start;
            for (int i = 0; i < cards.Length; i++)
            {
                var bar = (PKSelfHandBar)container.bars[i];
                if (i >= min && i <= max)
                {
                    if (!bar.mask) bar.setMask(true);
                }
                else if (bar.mask) bar.setMask(false);
                bar.refresh();
            }
        }

        /// <summary> 提示牌 </summary>
        public void tipsCards(int[] cards)
        {
            if (cards == null) return;
            PKRoomPanel.Panel.claerSelectHands();
            deselectHandsCard();
            for (int i = 0; i < this.cards.Length; i++)
            {
                var bar = (PKSelfHandBar)container.bars[i];

                for (int j = 0; j < cards.Length; j++)
                {
                    if (cards[j] == this.cards[i])
                    {
                        bar.isSelect(true);
                    }
                }
                bar.refresh();
            }
        }
    }
}
