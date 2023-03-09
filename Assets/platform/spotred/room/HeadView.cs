using platform.spotred.waitroom;

namespace platform.spotred.room
{
    /// <summary>
    /// 头像视图
    /// </summary>
    public class HeadView : UnrealLuaSpaceObject
    {
        public MatchPlayerBar bottom;

        public MatchPlayerBar right;

        public MatchPlayerBar top;

        public MatchPlayerBar left;

        protected override void xinit()
        {
            base.xinit();
            this.bottom = transform.Find("down").GetComponent<MatchPlayerBar>();
            this.bottom.init();
            this.right = transform.Find("right").GetComponent<MatchPlayerBar>();
            this.right.init();
            this.top = transform.parent.Find("tophead").GetComponent<MatchPlayerBar>();
            this.top.init();
            this.left = transform.Find("left").GetComponent<MatchPlayerBar>();
            this.left.init();
        }

        protected override void xrefresh()
        {
            bottom.setVisible(false);
            right.setVisible(false);
            top.setVisible(false);
            left.setVisible(false);
        }

        public void hideAllPiao()
        {
            bottom.isShowPiao(false);
            top.isShowPiao(false);
            left.isShowPiao(false);
            right.isShowPiao(false);
        }

        /// <summary>
        /// 显示飘
        /// </summary>
        public void showPiao(int index, bool isshow)
        {
            switch (index)
            {
                case RoomPanel.LOC_DOWN:
                    bottom.isShowPiao(isshow);
                    break;
                case RoomPanel.LOC_RIGHT:
                    right.isShowPiao(isshow);
                    break;
                case RoomPanel.LOC_TOP:
                    top.isShowPiao(isshow);
                    break;
                case RoomPanel.LOC_LEFT:
                    left.isShowPiao(isshow);
                    break;
            }
        }

        /// <summary>
        /// 设置托管状态
        /// </summary>
        public void setTrustee(int index, bool trustee)
        {
            switch (index)
            {
                case RoomPanel.LOC_DOWN:
                    bottom.isShowRobotstauts(trustee);
                    break;
                case RoomPanel.LOC_RIGHT:
                    right.isShowRobotstauts(trustee);
                    break;
                case RoomPanel.LOC_TOP:
                    top.isShowRobotstauts(trustee);
                    break;
                case RoomPanel.LOC_LEFT:
                    left.isShowRobotstauts(trustee);
                    break;
            }
        }

        public void ShowAllMatchPlayer(Room room, int dangjia, int xiaojia)
        {
            MatchPlayer[] player = room.players;
            int index = room.indexOf();
            int count = room.roomRule.rule.playerCount;

            MatchPlayerBar playerbar=null;
            for (int i = 0; i < count; i++)
            {
                playerbar = null;
                int pos = RoomPanel.getPlayerIndex(i);
                switch (pos)
                {
                    case RoomPanel.LOC_DOWN:
                        playerbar = bottom;
                        break;
                    case RoomPanel.LOC_RIGHT:
                        playerbar = right;
                        break;
                    case RoomPanel.LOC_TOP:
                        playerbar = top;
                        break;
                    case RoomPanel.LOC_LEFT:
                        playerbar = left;
                        break;
                }
                playerbar.setVisible(true);
                playerbar.setPlayer(player[i]);
                playerbar.refresh();
                if (dangjia == i)
                {
                    playerbar.showDang();
                }
                if (i == xiaojia)
                {
                    playerbar.showXiaoJia();
                }
            }





            //int pos = 0;

            //int i = (index + pos++)% count;
            //bottom.setPlayer(player[i]);
            //bottom.refresh();
            //if (dangjia == i)
            //{
            //    bottom.showDang();
            //}
            //if (i == xiaojia)
            //{
            //    bottom.showXiaoJia();
            //}

            //if (count > 2)
            //{
            //    i = (index + pos++) % count;
            //    right.setPlayer(player[i]);
            //    right.refresh();
            //    if (dangjia == i)
            //    {
            //        right.showDang();
            //    }

            //    if (i == xiaojia)
            //    {
            //        right.showXiaoJia();
            //    }
            //    right.setVisible(true);
            //}
            //else
            //{
            //    right.setVisible(false);
            //}

            //i = (index + pos++) % count;
            //this.top.setPlayer(player[i]);
            //this.top.refresh();
            //if (dangjia == i)
            //{
            //    top.showDang();
            //}
            //if (i == xiaojia)
            //{
            //    top.showXiaoJia();
            //}

            //if (count < 4)
            //{
            //    left.setVisible(false);
            //    return;
            //}

            //i = (index + pos++) % count;
            //left.setPlayer(player[i]);
            //left.refresh();
            //left.setVisible(true);
            //if (dangjia == i)
            //{
            //    left.showDang();
            //}

            //if (i == xiaojia)
            //{
            //    left.showXiaoJia();
            //}
        }

    }
}
