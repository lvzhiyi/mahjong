using basef.player;
using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 增加推广员面板
    /// </summary>
    public class ArenaAddExtensionBar: ScorllTableBar
    {
        private PlayerHeadView headView;

        private Text nickname;

        private Text id;

        public ArenaMember member;


        protected override void xinit()
        {
            headView = transform.Find("head").GetComponent<PlayerHeadView>();
            headView.init();
            nickname = transform.Find("name").GetComponent<Text>();
            id = transform.Find("id").GetComponent<Text>();
        }

        public void setArenaMember(ArenaMember member)
        {
            this.member = member;
        }

        protected override void xrefresh()
        {
            headView.setData(member.head, member.userid);
            headView.refresh();
            nickname.text = member.name;
            id.text = "ID:" + member.userid;
        }
    }
}
