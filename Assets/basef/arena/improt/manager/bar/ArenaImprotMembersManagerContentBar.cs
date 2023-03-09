using basef.player;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 竞赛场 导入成员 用户bar </summary>
    public class ArenaImprotMembersManagerContentBar : ScrollBar
    {
        ArenaImprotMembersManagerContentData data;

        /// <summary> 序号 </summary>
        UnrealTextField index;

        /// <summary> 玩家头像 </summary>
        PlayerHeadView userhead;

        /// <summary> 玩家名字 </summary>
        UnrealTextField username;

        /// <summary> 玩家ID </summary>
        UnrealTextField userid;

        /// <summary> 是否是成员 </summary>
        UnrealTextField member;

        protected override void xinit()
        {
            index = this.transform.Find("index").GetComponent<UnrealTextField>();
            index.init();

            userhead = this.transform.Find("head").GetComponent<PlayerHeadView>();
            userhead.init();

            username = this.transform.Find("name").GetComponent<UnrealTextField>();
            username.init();

            userid = this.transform.Find("userid").GetComponent<UnrealTextField>();
            userid.init();

            member = this.transform.Find("member").GetComponent<UnrealTextField>();
            member.init();
        }

        protected override void xrefresh()
        {
            if (data == null) return;
            index.text = index_space.ToString();

            username.text = data.name.ToString();

            userid.text = data.userid.ToString();

            if (data.arenaMember)
                member.text = "是";
            else member.text = "否";

            userhead.setData(data.headurl,data.userid);
            userhead.refresh();
        }

        public void setData(ArenaImprotMembersManagerContentData data)
        {
            this.data = data;
        }

        public ArenaImprotMembersManagerContentData getData()
        {
            return data;
        }
    }
}
