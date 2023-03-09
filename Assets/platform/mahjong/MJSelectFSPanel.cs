namespace platform.mahjong
{
    /// <summary>
    /// 快速开始，选择房数
    /// </summary>
    public class MJSelectFSPanel : UnrealLuaPanel
    {
        /// <summary>
        /// 当前玩家人数
        /// </summary>
        private int playerCount;

        protected override void xinit()
        {
            base.xinit();
        }

        public void setPlayerCount(int playerCount)
        {
            this.playerCount = playerCount;
        }

        public int getPlayerCount()
        {
            return playerCount;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
        }
    }
}
