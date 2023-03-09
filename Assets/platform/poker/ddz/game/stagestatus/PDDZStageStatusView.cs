using System.Collections;

namespace platform.poker
{
    public class PDDZStageStatusView : PKStageStatusView
    {
        protected override void xinit()
        {
            base.xinit();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
        }

        public override void showStageStatus(int pos, int posType, bool replay = false)
        {
            if (replay)
            {
                UnrealRoot.root.getDisplayObject<PDDZReplayRoomPanel>().removeDeskCard(pos);
            }
            else PKRoomPanel.Panel.removeDeskCard(pos);
            switch (PKGAME.GetIndex(pos))
            {
                case PKGAME.DOWN:
                    down.showText(posType, pos);
                    if (!replay)
                    {
                        StartCoroutine("delayDwonHideStatus");
                        down.setVisible(false);
                    }
                    else down.setVisible(true);
                    break;
                case PKGAME.RIGHT:
                    right.showText(posType, pos);
                    right.setVisible(true);
                    if (!replay) StartCoroutine("delayRightHideStatus");
                    break;
                case PKGAME.LEFT:
                    left.showText(posType, pos);
                    left.setVisible(true);
                    if (!replay) StartCoroutine("delayLeftHideStatus");
                    break;
            }
        }

        public override void hidePStatus(bool isDelay = false)
        {
            if (isDelay)
            {
                StopAllCoroutines();
                StartCoroutine("delayHideStatus");
            }
            else for (int i = 0; i < Room.room.getPlayerCount(); i++)
                {
                    hidePStatus(i);
                }
        }

        private IEnumerator delayDwonHideStatus()
        {
            yield return new UnityEngine.WaitForSeconds(delay * 10f);
            hidePStatus(PKGAME.DOWN);
            yield break;
        }

        private IEnumerator delayLeftHideStatus()
        {
            yield return new UnityEngine.WaitForSeconds(delay * 10f);
            hidePStatus(PKGAME.LEFT);
            yield break;
        }

        private IEnumerator delayRightHideStatus()
        {
            yield return new UnityEngine.WaitForSeconds(delay * 10f);
            hidePStatus(PKGAME.RIGHT);
            yield break;
        }

        private new IEnumerator delayHideStatus()
        {
            yield return new UnityEngine.WaitForSeconds(delay);
            for (int i = 0; i < Room.room.getPlayerCount(); i++)
            {
                hidePStatus(i);
            }
            yield break;
        }

        public override void hidePStatus(int pos)
        {
            switch (PKGAME.GetIndex(pos))
            {
                case PKGAME.DOWN:
                    down.setVisible(false);
                    break;
                case PKGAME.RIGHT:
                    right.setVisible(false);
                    break;
                case PKGAME.LEFT:
                    left.setVisible(false);
                    break;
            }
        }
    }
}
