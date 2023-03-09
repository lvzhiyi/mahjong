namespace platform.poker
{
    public class PPDKAllHandView : PKAllHandView
    {
        protected override void xinit()
        {
            base.xinit();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
        }

        protected override void refreshLeft(int[] cards, bool isShow, bool dizhu)
        {
            if (isShow)
            {
                left.setDiZhu(dizhu);
                left.setMingPai(isShow);
                left.setCards(cards);
                left.refresh();
            }
            left.setVisible(isShow);
        }

        protected override void refreshRight(int[] cards, bool isShow, bool dizhu)
        {
            if (isShow)
            {
                right.setDiZhu(dizhu);
                right.setMingPai(isShow);
                right.setCards(cards);
                right.refresh();
            }
            right.setVisible(isShow);
        }

        protected override void refreshDown(int[] cards, bool isShow, bool dizhu)
        {
            down.setCards(cards);
            down.setMingPai(isShow);
            down.refresh();
            down.setVisible(true);
        }

        protected override void refreshTop(int[] cards, bool isShow, bool dizhu)
        {
            if (isShow)
            {
                top.setCards(cards);
                top.setMingPai(isShow);
                top.refresh();
            }
            top.setVisible(isShow);
        }

        public override void refresHands(int pos, int[] cards)
        {
            switch (PKGAME.GetIndex(pos))
            {
                case PKGAME.DOWN:
                    down.setCards(cards);
                    down.refresh();
                    break;
                case PKGAME.LEFT:
                    left.setCards(cards);
                    left.refresh();
                    break;
                case PKGAME.RIGHT:
                    right.setCards(cards);
                    right.refresh();
                    break;
                case PKGAME.TOP:
                    top.setCards(cards);
                    top.refresh();
                    break;
            }
        }

        public override void refresHands(int pos, int[] cards, bool isShow, bool dizhu)
        {
            switch (PKGAME.GetIndex(pos))
            {
                case PKGAME.DOWN:
                    refreshDown(cards, isShow, dizhu);
                    break;
                case PKGAME.RIGHT:
                    refreshRight(cards, isShow, dizhu);
                    break;
                case PKGAME.LEFT:
                    refreshLeft(cards, isShow, dizhu);
                    break;
                case PKGAME.TOP:
                    refreshTop(cards,isShow,dizhu);
                    break;
            }
        }

        public override void refresHands(bool isShow, int pos, int[] cards)
        {
            switch (PKGAME.GetIndex(pos))
            {
                case PKGAME.DOWN:
                    refreshDown(cards);
                    break;
                case PKGAME.RIGHT:
                    refreshRight(cards);
                    break;
                case PKGAME.LEFT:
                    refreshLeft(cards);
                    break;
                case PKGAME.TOP:
                    refreshTop(cards);
                    break;
            }
        }

        protected override void refreshLeft(int[] cards)
        {
            left.setDiZhu(false);
            left.setMingPai(false);
            left.setCards(cards);
            left.refresh();
            left.setVisible(true);
        }

        protected override void refreshRight(int[] cards)
        {
            right.setDiZhu(false);
            right.setMingPai(false);
            right.setCards(cards);
            right.refresh();
            right.setVisible(true);
        }

        protected override void refreshDown(int[] cards)
        {
            down.setCards(cards);
            down.refresh();
            down.setVisible(true);
        }

        protected override void refreshTop(int[] cards)
        {
            top.setCards(cards);
            top.refresh();
            top.setVisible(true);
        }

        public override void resetMingPai()
        {
            down.setMingPai(false);
            right.setMingPai(false);
            left.setMingPai(false);
            if (top != null)
            {
                top.setVisible(false);
            }
        }

        public override void cancelAllSelect()
        {
            down.deselectHandsCard();
        }

        public override void resetMask()
        {
            down.hideAllHandsMask();
        }

        public override void refreshSelectHands(int start, int end)
        {
            down.refreshAllHandsMask(start, end);
        }

        public override void tipsCards(int[] cards)
        {
            down.tipsCards(cards);
        }
    }
}
