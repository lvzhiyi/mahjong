using cambrian.game;

namespace platform.poker
{
    public class PDDZPlayerStatusView : PKPlayerStatusView
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
        }

        public void hideCardWarning()
        {
            down.showCardWarning(false);
            right.showCardWarning(false);
            left.showCardWarning(false);
        }

        public override void showBanker(int pos)
        {
            hideBanker();
            switch (PKGAME.GetIndex(pos))
            {
                case PKGAME.DOWN:
                    down.showBanker(true);
                    break;
                case PKGAME.RIGHT:
                    right.showBanker(true);
                    break;
                case PKGAME.LEFT:
                    left.showBanker(true);
                    break;
            }
        }

        public override void setCardNum(int pos, int num)
        {
            switch (PKGAME.GetIndex(pos))
            {
                case PKGAME.DOWN:
                    down.setCardNum(num);
                    break;
                case PKGAME.LEFT:
                    left.setCardNum(num);
                    break;
                case PKGAME.RIGHT:
                    right.setCardNum(num);
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
            }
            if (num == 1)
                SoundManager.playPKOther(SoundManager.baojing1, Room.room.players[pos].player.sex);
            else if (num == 2)
                SoundManager.playPKOther(SoundManager.baojing2, Room.room.players[pos].player.sex);
        }
    }
}
