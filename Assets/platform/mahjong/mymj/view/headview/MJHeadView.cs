using DG.Tweening;
using platform.spotred;
using platform.spotred.room;
using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 麻将头像视图
    /// </summary>
    public class MJHeadView : HeadView
    {
        Vector3 bottompos;
        Vector3 rightpos;
        Vector3 toppos;
        Vector3 leftpos;


        protected override void xinit()
        {
            bottom = transform.Find("down").GetComponent<MJMatchPlayerBar>();
            bottom.init();
            right = transform.Find("right").GetComponent<MJMatchPlayerBar>();
            right.init();
            top = transform.Find("top").GetComponent<MJMatchPlayerBar>();
            top.init();
            left = transform.Find("left").GetComponent<MJMatchPlayerBar>();
            left.init();

            bottompos = bottom.transform.localPosition;
            rightpos = right.transform.localPosition;
            toppos = top.transform.localPosition;
            leftpos = left.transform.localPosition;

        }

        protected override void xrefresh()
        {
            bottom.setVisible(false);
            right.setVisible(false);
            top.setVisible(false);
            left.setVisible(false);
        }

        /// <summary>
        /// 等待界面时的位置
        /// </summary>
        public void waitViewPostion()
        {
            bottom.setLocalXY(0, -235);
            right.setLocalXY(300, 0);
            top.setLocalXY(0, 200);
            left.setLocalXY(-300, 0);
        }

        public void gameViewPostion()
        {
            bottom.transform.DOLocalMove(bottompos, 0.5f);
            right.transform.DOLocalMove(rightpos, 0.5f);
            top.transform.DOLocalMove(toppos, 0.5f);
            left.transform.DOLocalMove(leftpos, 0.5f);
            if (Room.room.roomRule.getGameNum() == 0)
            {
                bottom.transform.DOLocalMove(bottompos, 0.5f);
                right.transform.DOLocalMove(rightpos, 0.5f);
                top.transform.DOLocalMove(toppos, 0.5f);
                left.transform.DOLocalMove(leftpos, 0.5f);
            }
            else
            {
                bottom.setLocalVector3(bottompos);
                right.setLocalVector3(rightpos);
                top.setLocalVector3(toppos);
                left.setLocalVector3(leftpos);
            }
        }

        public void showMatchPlayers(int banker)
        {
            Room room = Room.room;
            MatchPlayer[] player = room.players;
            int index = room.indexOf();
            int count = room.roomRule.rule.playerCount;


            for (int i = 0; i < count; i++)
            {
                refreshplayer(MahJongPanel.getPlayerIndex(i), player[i], i == banker);
            }
        }


        public void refreshplayer(int index, MatchPlayer player, bool b)
        {
            bool isshow = (player == null);
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:

                    bottom.setVisible(!isshow);
                    if (isshow) return;
                    bottom.setPlayer(player);
                    bottom.refresh();
                    if (b)
                        bottom.showDang();
                    break;
                case MahJongPanel.LOC_RIGHT:
                    right.setVisible(!isshow);
                    if (isshow) return;
                    right.setPlayer(player);
                    right.refresh();
                    if (b)
                        right.showDang();
                    break;
                case MahJongPanel.LOC_TOP:
                    top.setVisible(!isshow);
                    if (isshow) return;
                    top.setPlayer(player);
                    top.refresh();
                    if (b)
                        top.showDang();
                    break;
                case MahJongPanel.LOC_LEFT:
                    left.setVisible(!isshow);
                    if (isshow) return;
                    left.setPlayer(player);
                    left.refresh();
                    if (b)
                        left.showDang();
                    break;
            }
        }
    }
}
