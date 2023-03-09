using basef.player;
using UnityEngine.UI;

namespace basef.arena
{
    public class AreanLimitMemberBar:UnrealLuaSpaceObject
    {
        Text nickname;

        Text userid;

        PlayerCircleHeadView headView;

        private ArenaMutexMember member;

        protected override void xinit()
        {
            nickname = transform.Find("name").GetComponent<Text>();
            userid = transform.Find("id").GetComponent<Text>();
            headView = transform.Find("head").GetComponent<PlayerCircleHeadView>();
            headView.init();
        }

        public void setData(ArenaMutexMember member)
        {
            this.member = member;
            headView.setHeadGray(!member.isExist);
        }


        protected override void xrefresh()
        {
            nickname.text = member.nickname;
            userid.text = member.userid.ToString();
            headView.setData(member.head,member.userid);
            headView.refresh();
        }
    }
}
