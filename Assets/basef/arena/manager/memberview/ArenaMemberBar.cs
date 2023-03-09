using basef.player;
using cambrian.unreal.scroll;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaMemberBar:ScrollBar
    {
        public ArenaMember member;

        private Text number;
        /// <summary>
        /// 上级
        /// </summary>
        private Text sup;
        /// <summary>
        /// 职务
        /// </summary>
        private Text post;

        private Text jushu;

        private Text gold;

        private Text nickname;

        private Text id;

        private PlayerHeadView headView;

        private UnrealScaleButton btn;
        /// <summary>
        /// 黑名单状态
        /// </summary>
        private Image ft;

        protected override void xinit()
        {
            number = transform.Find("number").GetComponent<Text>();
            sup = transform.Find("sup").GetComponent<Text>();
            post = transform.Find("post").GetComponent<Text>();
            jushu = transform.Find("scenecount").GetComponent<Text>();
            gold = transform.Find("gold").GetComponent<Text>();
            nickname = transform.Find("nickname").GetComponent<Text>();
            id = transform.Find("id").GetComponent<Text>();
            ft = transform.Find("ft").GetComponent<Image>();
            headView = transform.Find("head").GetComponent<PlayerHeadView>();
            headView.init();
            btn = transform.Find("btn").GetComponent<UnrealScaleButton>();
        }

        public void setData(ArenaMember member)
        {
            this.member = member;
        }

        protected override void xrefresh()
        {
            number.text = (index_space + 1) + "";
            nickname.text = member.name;
            id.text = "ID:"+member.userid;
            headView.setData(member.head,member.userid);
            headView.refresh();

            headView.setHeadGray(!member.hasStatus(ArenaMember.STATUS_ONLINE));

            ft.gameObject.SetActive(member.hasStatus(ArenaMember.STATUS_ARENA_LIMIT));

            sup.text = member.masterName;
            post.text = member.getLeveName();
            jushu.text = member.totaljushu.ToString();
            gold.text = member.getArenaGold().ToString();
            btn.setVisible(member.userid!=Player.player.userid);
        }

        public void callback()
        {
            xrefresh();
            ft.gameObject.SetActive(member.hasStatus(ArenaMember.STATUS_ARENA_LIMIT));

        }
    }
}
