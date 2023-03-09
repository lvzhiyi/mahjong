using platform.spotred;

namespace platform.poker
{
    /// <summary> 头像管理 </summary> 斗地主
    public class PKRoomMatchPlayerBar : RoomMatchPlayerBar
    {
        protected override void xinit()
        {
            base.xinit();
        }

        protected override void xrefresh()
        {
            setVisible(player != null);
            if (player == null) return;
            base.xrefresh();
        }

        public virtual void setMPlayer(MatchPlayer player)
        {
            this.player = player;
            xrefresh();
        }

        public MatchPlayer getPlayer()
        {
            return player;
        }

        public void setOnLine(int status)
        {
            player.player.setStatus(status);
            updateOnline();
        }
    }
}
