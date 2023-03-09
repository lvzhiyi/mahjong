using basef.player;
using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaAddLimitRightBar : ScorllTableBar
    {
        private PlayerCircleHeadView headView;

        private Text nickname;

        private Image image;

        public ArenaMutexMember member;

        private bool isshowimg;

        protected override void xinit()
        {
            headView = transform.Find("head").GetComponent<PlayerCircleHeadView>();
            headView.init();
            nickname = transform.Find("name").GetComponent<Text>();
            image = transform.Find("icon").GetComponent<Image>();
        }

        public void setArenaMember(ArenaMutexMember member, bool isshowimg)
        {
            this.member = member;
            this.isshowimg = isshowimg;
            headView.setHeadGray(!member.isExist);
        }

        public void showIcon(bool b)
        {
            image.gameObject.SetActive(b);
        }

        protected override void xrefresh()
        {
            headView.setData(member.head, member.userid);
            headView.refresh();
            nickname.text = member.nickname;
            image.gameObject.SetActive(isshowimg);
        }
    }
}
