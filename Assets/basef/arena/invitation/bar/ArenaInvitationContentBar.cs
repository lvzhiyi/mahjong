using basef.player;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 邀请界面 用户信息 内容bar </summary>
    public class ArenaInvitationContentBar : ScrollBar
    {
        /// <summary> 用户头像 </summary>
        PlayerHeadView userhead;

        /// <summary> 用户名字 </summary>
        UnrealTextField username;

        /// <summary> 用户ID </summary>
        UnrealTextField userid;

        SimplePlayer player;

        protected override void xinit()
        {
            userhead = transform.Find("head").GetComponent<PlayerHeadView>();
            userhead.init();

            username = transform.Find("name").GetComponent<UnrealTextField>();
            username.init();

            userid = transform.Find("userid").GetComponent<UnrealTextField>();
            userid.init();
        }

        public void setData(SimplePlayer player)
        {
            this.player = player;
        }


        protected override void xrefresh()
        {
            if (player.head == null)
            {
                userhead.refresh();
                return;
            }
            
            userhead.setData(player.head, player.userid);
            userhead.refresh();
            username.text = player.name;
            userid.text = player.userid.ToString();
        }
		
        public long getUserSid()
        {
            return player.userid;
        }
    }
}
