namespace platform.poker
{
    /// <summary> 斗地主 头像管理 </summary>
    public class PKHeadView : UnrealLuaSpaceObject
    {
        public PKRoomMatchPlayerBar down { get; protected set; }

        public PKRoomMatchPlayerBar right { get; protected set; }

        public PKRoomMatchPlayerBar left { get; protected set; }

        public PKRoomMatchPlayerBar top { get; set; }

        protected override void xinit()
        {
            down = transform.Find("down").GetComponent<PKRoomMatchPlayerBar>();
            down.init();

            right = transform.Find("right").GetComponent<PKRoomMatchPlayerBar>();
            right.init();

            left = transform.Find("left").GetComponent<PKRoomMatchPlayerBar>();
            left.init();

            if (transform.Find("top")!=null)
            {
                top = transform.Find("top").GetComponent<PKRoomMatchPlayerBar>();
                top.init();
            }
           
        }

        public void hideall()
        {
            down.setVisible(false);
            right.setVisible(false);
            left.setVisible(false);
            if (top != null)
            {
                top.setVisible(false);
            }
        }

        protected override void xrefresh()
        {
            hideall();
            
            for (int i = 0; i < Room.room.roomRule.rule.playerCount; i++)
            {
                switch (PKGAME.GetIndex(i))
                {
                    case PKGAME.DOWN:
                        down.setMPlayer(Room.room.players[i]);
                        break;
                    case PKGAME.RIGHT:
                        right.setMPlayer(Room.room.players[i]);
                        break;
                    case PKGAME.LEFT:
                        left.setMPlayer(Room.room.players[i]);
                        break;
                    case PKGAME.TOP:
                        top.setMPlayer(Room.room.players[i]);
                        break;
                }
            }
        }

        /// <summary> 设置托管状态 </summary>
        public void setTrustee(int index, bool trustee)
        {
            switch (PKGAME.GetIndex(index))
            {
                case PKGAME.DOWN:
                    down.updateRobot(trustee);
                    break;
                case PKGAME.RIGHT:
                    right.updateRobot(trustee);
                    break;
                case PKGAME.LEFT:
                    left.updateRobot(trustee);
                    break;
                case PKGAME.TOP:
                    top.updateRobot(trustee);
                    break;
            }
        }

        /// <summary> 更新单个玩家游戏状态 </summary>
        public void updateOnLine(int index, int status)
        {
            switch (PKGAME.GetIndex(index))
            {
                case PKGAME.DOWN:
                    down.setOnLine(status);
                    break;
                case PKGAME.RIGHT:
                    right.setOnLine(status);
                    break;
                case PKGAME.LEFT:
                    left.setOnLine(status);
                    break;
                case PKGAME.TOP:
                    top.setOnLine(status);
                    break;
            }
        }

        /// <summary> 更新分数 </summary>
        public void updateSocre()
        {
            for (int i = 0; i < Room.room.roomRule.rule.playerCount; i++)
            {
                switch (PKGAME.GetIndex(i))
                {
                    case PKGAME.DOWN:
                        down.updateSocre();
                        break;
                    case PKGAME.RIGHT:
                        right.updateSocre();
                        break;
                    case PKGAME.LEFT:
                        left.updateSocre();
                        break;
                    case PKGAME.TOP:
                        top.updateSocre();
                        break;
                }
            }
        }
    }
}
