using basef.player;

namespace basef.lobby
{
    public class LobbyPlayerInfoView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 头像
        /// </summary>
        private PlayerCircleHeadView playerHeadView;
        /// <summary>
        /// 昵称
        /// </summary>
        protected UnrealTextField nickname;
        /// <summary>
        /// userid
        /// </summary>
        protected UnrealTextField id;

        protected SimplePlayer player;

        protected override void xinit()
        {
            this.playerHeadView = this.transform.Find("info").GetComponent<PlayerCircleHeadView>();
            this.playerHeadView.init();
            this.nickname = this.transform.Find("text").GetComponent<UnrealTextField>();
            this.id = this.transform.Find("id").GetComponent<UnrealTextField>();
        }

        public void setSimleplayer(SimplePlayer player)
        {
            this.player=player;
        }

        protected override void xrefresh()
        {
            this.nickname.text = player.name;
            this.id.text = ":" + player.userid;
            this.playerHeadView.setData(Player.player.head, Player.player.userid);
            this.playerHeadView.refresh();
        }
    }
}
