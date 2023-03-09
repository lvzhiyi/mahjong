using basef.notice;
using basef.player;
using cambrian.common;
using cambrian.game;
using cambrian.uui;
using scene.game;

namespace basef.lobby
{
    public class SpotLobbyPanel : UnrealLuaPanel
    {
        /// <summary>
        /// 房卡
        /// </summary>
        private UnrealTextField roomcard;

        private LobbyPlayerInfoView playerBar;
        public NoticeListView noticeList;
        public bool isShow = true;

        public ShaderEffect effect;
        protected override void xinit()
        {
            base.xinit();
            roomcard = top.Find("card").Find("num").GetComponent<UnrealTextField>();
            playerBar = top.Find("player").GetComponent<LobbyPlayerInfoView>();
            playerBar.init();
            noticeList = content.Find("notices").GetComponent<NoticeListView>();
            noticeList.init();
            
            RecvPlayerCardCommand.addEvent(updateCard);
        }

        public void updateCard()
        {
            roomcard.text = PlayerToken.instance.token+"";
        }

        protected override void xrefresh()
        {
            base.xrefresh();
           
            roomcard.text = PlayerToken.instance.token + "";
            playerBar.setSimleplayer(Player.player.getSimplePlayer());
            playerBar.refresh();
            
        }
      

        public override void setVisible(bool b)
        {
            base.setVisible(b);
            if (b && SpotRedRoot.roots.isStart)
            {
                if (!StatusKit.hasStatus(User.user.status,User.STATUS_LINK_MOBILE))
                {
                    //UnrealRoot.root.getDisplayObject<AuthNamePanel>().refresh();
                    //UnrealRoot.root.getDisplayObject<AuthNamePanel>().setCVisible(true);
                }
                SpotRedRoot.roots.isStart = false;
            }

            if (b)
            {
                new OpenScrollNoticeListPanelProcess().execute();
            }
        }
      

        public override void OnDestroy()
        {
            RecvPlayerCardCommand.removeEvent(updateCard);
        }
    }
}
