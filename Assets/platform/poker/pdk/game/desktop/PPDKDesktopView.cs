using System.Collections;
using UnityEngine;

namespace platform.poker
{
    public class PPDKDesktopView : PKDesktopView
    {
        protected PPDKRoomPanel panel;

        protected override void xinit()
        {
            base.xinit();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
        }

        public override void showCards(int pos, int[] cards)
        {
            base.showCards(pos, cards);
        }

        public override void hideCards(bool isDelay = false)
        {
            if (isDelay) StartCoroutine("delayHideCards");
            else for (int i = 0; i < Room.room.getPlayerCount(); i++)
                    hideCards(i);
        }

        public override void hideCards(int pos)
        {
            if (PDKMatch.match.replay)
            {
                UnrealRoot.root.getDisplayObject<PPDKReplayRoomPanel>().gameView.stage.hidePStatus(pos);
            }
            else UnrealRoot.root.getDisplayObject<PDDZRoomPanel>().gameView.stage.hidePStatus(pos);

            switch (PKGAME.GetIndex(pos))
            {
                case PKGAME.DOWN:
                    down.setVisible(false);
                    down.setCards(null);
                    down.refresh();
                    break;
                case PKGAME.RIGHT:
                    right.setVisible(false);
                    right.setCards(null);
                    right.refresh();
                    break;
                case PKGAME.LEFT:
                    left.setVisible(false);
                    left.setCards(null);
                    left.refresh();
                    break;
            }
        }

        protected override IEnumerator delayHideCards()
        {
            yield return new WaitForSeconds(delayTime);
            for (int i = 0; i < Room.room.getPlayerCount(); i++)
            {
                hideCards(i);
            }
            yield break;
        }
    }
}
