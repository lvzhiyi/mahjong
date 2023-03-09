namespace platform.poker
{
    /// <summary> 房卡场 准备按钮管理界面 </summary>
    public class PKRoomCardWaitView : UnrealLuaSpaceObject
    {
        /// <summary> 准备 </summary>
        public UnrealScaleButton ready { get; set; }
        /// <summary> 分享 </summary>
        //UnrealScaleButton share;
        /// <summary> 邀请</summary>
        UnrealScaleButton invite;

        protected override void xinit()
        {
            ready = transform.Find("button").Find("ready").GetComponent<UnrealScaleButton>();
            ready.init();

            //share = transform.Find("button").Find("share").GetComponent<UnrealScaleButton>();
            //share.init();

            invite = transform.Find("button").Find("inviteorjoin").GetComponent<UnrealScaleButton>();
            invite.init();
        }

        protected override void xrefresh()
        {
            Room room = Room.room;
            ready.setVisible(!room.players[room.indexOf()].isReady());
            //share.setVisible(true);
            if (room.isType(Room.CLUB)|| room.isType(Room.ARENA))
            {
                invite.setVisible(true);
                //share.setVisible(true);
                if (room.roomRule.getGameNum() > -1)
                {
                    invite.setVisible(false);
                    //share.setVisible(false);
                }
            }
            else
            {
                invite.setVisible(false);
            }
            //if (room.roomRule.getGameNum() > -1 && !room.isType(Room.JBC))
            //{
            //    share.setVisible(false);
            //}
            //else
            //{
            //    share.setVisible(true);
            //}

            if ((!room.isType(Room.CLUB)&&!room.isType(Room.ARENA) )|| room.roomRule.getGameNum() > 0) return;
            int leaveTimeOut = room.getRule().getIntAtribute("leaveTimeout");
            if (leaveTimeOut == 0) return;
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
    }
}
