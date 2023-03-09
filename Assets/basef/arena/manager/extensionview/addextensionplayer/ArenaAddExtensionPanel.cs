using basef.player;
using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 增加推广员面板
    /// </summary>
    public class ArenaAddExtensionPanel : UnrealLuaPanel
    {
        [HideInInspector] public UnrealInputTextField searchid;

        private Transform playerView;

        private PlayerHeadView headView;

        private Text nickname;

        private Text uuid;

        public ArenaMember member;

        private ArrayList<ArenaMember> members;
        

        protected override void xinit()
        {
            base.xinit();
            searchid = content.transform.Find("content").Find("searchinput").GetComponent<UnrealInputTextField>();
            searchid.init();
            searchid.valueChange = searchidvaluechange;

            playerView = content.transform.Find("content").Find("player");
            headView = playerView.Find("head").GetComponent<PlayerHeadView>();
            headView.init();
            nickname = playerView.Find("name").GetComponent<Text>();
            uuid = playerView.Find("id").GetComponent<Text>();
        }

        public void searchidvaluechange(string str)
        {
        }

        public void setData(ArenaMember[] members)
        {

            if (this.members == null)
                this.members = new ArrayList<ArenaMember>();
            else
                this.members.clear();
            this.members.add(members);
        }

        public void setSearchMemeber(ArenaMember member)
        {
            headView.setData(member.head, member.userid);
            headView.refresh();
            nickname.text = member.name;
            uuid.text = "ID:" + member.userid;
            playerView.gameObject.SetActive(true);
            this.member = member;
        }

        public void remove(ArenaMember member)
        {
            members.remove(member);
            this.member = null;
        }

        protected override void xrefresh()
        {
            searchid.text = "";
            playerView.gameObject.SetActive(false);
        }

    }
}
