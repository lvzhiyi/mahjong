using cambrian.game;

namespace platform.poker
{
    /// <summary> 斗地主 玩家状态管理类 </summary>
    public class PPDKPlayerStatusView : PKPlayerStatusView
    {
        protected override void xinit()
        {
            base.xinit();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            hideBanker();
            hideCardWarning();
        }

        public override void hideBanker()
        {
            down.showBanker(false);
            right.showBanker(false);
            left.showBanker(false);
            
            down.setVisible(false);
            right.setVisible(false);
            left.setVisible(false);
            if (top != null)
            {
                top.showBanker(false);
                top.setVisible(false);
            }
        }

        public void hideCardWarning()
        {
            down.showCardWarning(false);
            right.showCardWarning(false);
            left.showCardWarning(false);
            if (top != null)
            {
                top.showCardWarning(false);
            }
        }

        public override void showBanker(int pos)
        {
            hideBanker();
            switch (PKGAME.GetIndex(pos))
            {
                case PKGAME.DOWN:
                    down.showBanker(true);
                    down.setVisible(true);
                    break;
                case PKGAME.RIGHT:
                    right.showBanker(true);
                    right.setVisible(true);
                    break;
                case PKGAME.LEFT:
                    left.showBanker(true);
                    left.setVisible(true);
                    break;
                case PKGAME.TOP:
                    top.showBanker(true);
                    top.setVisible(true);
                    break;
            }
        }

        public override void setCardNum(int pos, int num)
        {
            switch (PKGAME.GetIndex(pos))
            {
                case PKGAME.DOWN:
                    down.setCardNum(num);
                    down.setVisible(true);
                    break;
                case PKGAME.LEFT:
                    left.setCardNum(num);
                    left.setVisible(true);
                    break;
                case PKGAME.RIGHT:
                    right.setCardNum(num);
                    right.setVisible(true);
                    break;
                case PKGAME.TOP:
                    top.setCardNum(num);
                    top.setVisible(true);
                    break;
            }
        }

        public override void showCardWarning(int pos, int num)
        {
            switch (PKGAME.GetIndex(pos))
            {
                case PKGAME.DOWN:
                    down.showCardWarning(num <= CardsWarningNum);
                    break;
                case PKGAME.LEFT:
                    left.showCardWarning(num <= CardsWarningNum);
                    break;
                case PKGAME.RIGHT:
                    right.showCardWarning(num <= CardsWarningNum);
                    break;
                case PKGAME.TOP:
                    top.showCardWarning(num <= CardsWarningNum);
                    break;
            }
            if (num == 1)
                SoundManager.playPKOther(SoundManager.baojing1, Room.room.players[pos].player.sex);
        }
    }
}
