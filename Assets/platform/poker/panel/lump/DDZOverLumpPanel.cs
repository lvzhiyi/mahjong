using cambrian.uui;

namespace platform.poker
{
    /// <summary> 斗地主 总结算 玩家结算信息 </summary>
    public class DDZOverLumpPanel : UnrealLuaPanel
    {
        SpritesList title;

        SpritesList back;

        UnrealTableContainer playerInfo;

        private long[] totalsores;

        protected override void xinit()
        {
            base.xinit();

            title = content.transform.Find("title").GetComponent<SpritesList>();
            back = content.transform.Find("content_bg").GetComponent<SpritesList>();

            playerInfo = content.transform.Find("content").GetComponent<UnrealTableContainer>();
            playerInfo.init();
        }

        protected override void xrefresh()
        {
            if (totalsores == null) return;
            playerInfo.resize(Room.room.getPlayerCount());
            for (int i = 0; i < Room.room.getPlayerCount(); i++)
            {
                DDZOverLumpUserInfoBar bar = (DDZOverLumpUserInfoBar)playerInfo.bars[i];
                bar.index_space = i;
                bar.setData(totalsores[i],Room.room.players[i].player);
                bar.refresh();
            }
        }

        public void setData(long[] totalsores)
        {
            this.totalsores = totalsores;
        }
    }
}
