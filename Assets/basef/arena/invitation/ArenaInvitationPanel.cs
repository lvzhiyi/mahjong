using basef.player;
using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary> 竞赛场 邀请面板 </summary>
    public class ArenaInvitationPanel : UnrealLuaPanel
    {

        UnrealInputTextField searchid;

        private Transform playerView;

        private PlayerHeadView headView;

        private Text nickname;

        private Text uuid;

        [HideInInspector] public SimplePlayer player;

        protected override void xinit()
        {
            base.xinit();
            searchid = content.transform.Find("content").Find("searchinput").GetComponent<UnrealInputTextField>();
            searchid.init();

            playerView = content.transform.Find("content").Find("player");
            headView = playerView.Find("head").GetComponent<PlayerHeadView>();
            headView.init();
            nickname = playerView.Find("name").GetComponent<Text>();
            uuid = playerView.Find("id").GetComponent<Text>();
        }

        public void setData(SimplePlayer player)
        {
            this.player = player;
        }

        public string getUserSid()
        {
            return searchid.text;
        }

        protected override void xrefresh()
        {
            searchid.text = "";
            if (player == null)
                playerView.gameObject.SetActive(false);
            else
            {
                headView.setData(player.head, player.userid);
                headView.refresh();
                nickname.text = player.name;
                uuid.text = "ID:" + player.userid;
                playerView.gameObject.SetActive(true);
            }
        }
    }
}
