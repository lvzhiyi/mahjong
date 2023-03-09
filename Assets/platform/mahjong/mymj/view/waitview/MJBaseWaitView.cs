using platform.spotred;
using UnityEngine;

namespace platform.mahjong
{
    public class MJBaseWaitView : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 下方
        /// </summary>
        private MJWaitPlayerActionView down;
        /// <summary>
        /// 左方
        /// </summary>
        private MJWaitPlayerActionView left;
        /// <summary>
        /// 上方
        /// </summary>
        private MJWaitPlayerActionView top;
        /// <summary>
        /// 右方
        /// </summary>
        private MJWaitPlayerActionView right;
        /// <summary>
        /// 准备
        /// </summary>
        public UnrealTextButton ready { get; set; }
        /// <summary>
        /// 邀请
        /// </summary>
        UnrealTextButton inviteorjoin;

        /// <summary>
        /// 分享
        /// </summary>
        //UnrealScaleButton share;
        RectTransform space;

        protected override void xinit()
        {
            down = transform.Find("down").GetComponent<MJWaitPlayerActionView>();
            down.init();
            left = transform.Find("left").GetComponent<MJWaitPlayerActionView>();
            left.init();
            top = transform.Find("top").GetComponent<MJWaitPlayerActionView>();
            top.init();
            right = this.transform.Find("right").GetComponent<MJWaitPlayerActionView>();
            right.init();

            ready = transform.Find("button").Find("ready").GetComponent<UnrealTextButton>();
            inviteorjoin = transform.Find("button").Find("inviteorjoin").GetComponent<UnrealTextButton>();
            //share = transform.Find("button").Find("share").GetComponent<UnrealScaleButton>();
            //if (transform.Find("sharebg") != null)
            //{
            //    if (transform.Find("sharebg").Find("space") != null)
            //    {
            //        space = transform.Find("sharebg").Find("space").GetComponent<RectTransform>();
            //    }
            //}
        }

        protected override void xrefresh()
        {
            top.setReady(false);
            right.setReady(false);
            left.setReady(false);
            down.setReady(false);

            Room room = Room.room;

            MatchPlayer[] players = room.players;
            bool isReady = false;
            for (int i = 0; i < players.Length; i++)
            {
                isReady = players[i] == null ? false : players[i].isReady();
                setReady(MahJongPanel.getPlayerIndex(i), isReady);
            }

            if (room.isType(Room.CLUB)|| room.isType(Room.ARENA))
            {
                inviteorjoin.setVisible(true);
                if (space != null)
                    space.transform.localPosition = new Vector2(-120, -40);
                if (room.roomRule.getGameNum() > -1)
                {
                    inviteorjoin.setVisible(false);
                    if (space != null)
                        space.transform.localPosition = new Vector2(-10, -40);
                }
            }
            else
            {
                inviteorjoin.setVisible(false);
                if (space != null)
                    space.transform.localPosition = new Vector2(-10, -40);
            }

            //if (room.roomRule.getGameNum() > -1)
            //{
            //    share.setVisible(false);
            //}
            //else
            //{
            //    share.setVisible(true);
            //}

            ready.setVisible(!room.players[room.indexOf()].isReady());
            showMaster();

            int leaveTimeOut = room.getRule().getIntAtribute("leaveTimeout");
            if (leaveTimeOut == 0 || room.roomRule.getGameNum() > 0) return;
            long fulltime = room.fulltime;
            if (fulltime == 0)
            {
                ready.setVisible(false);
            }
            else
            {
                ready.setVisible(!room.players[room.indexOf()].isReady());
            }
        }

        public virtual void showMaster()
        {

        }

        public virtual void setReady(int index, bool b)
        {
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    down.setReady(b);
                    this.ready.setVisible(false);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    right.setReady(b);
                    break;
                case MahJongPanel.LOC_TOP:
                    top.setReady(b);
                    break;
                case MahJongPanel.LOC_LEFT:
                    left.setReady(b);
                    break;
            }
        }
    }
}
