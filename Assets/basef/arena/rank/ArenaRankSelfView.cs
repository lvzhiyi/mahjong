using basef.player;
using UnityEngine.UI;

namespace basef.rank
{
    public class ArenaRankSelfView : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 昵称
        /// </summary>
        private Text nickname;

        /// <summary>
        /// 头像
        /// </summary>
        private PlayerCircleHeadView head;

        /// <summary>
        /// 局数
        /// </summary>
        private Text jushuText;

        /// <summary>
        /// 之后排名
        /// </summary>
        private UnrealTextField later;

        private Text id;

        private int rankIndex;

        private string jushu;

        protected override void xinit()
        {
            this.nickname = this.transform.Find("name").GetComponent<Text>();
            this.head = this.transform.Find("head").GetComponent<PlayerCircleHeadView>();
            this.head.init();
            this.jushuText = this.transform.Find("jushu").GetComponent<Text>();

            this.later = this.transform.Find("later").GetComponent<UnrealTextField>();
            this.id = this.transform.Find("id").GetComponent<Text>();
        }

        public void setData(int index,string jushu)
        {
            this.rankIndex = index;
            this.jushu = jushu;
        }

        protected override void xrefresh()
        {
            this.nickname.text = Player.player.name;
            this.id.text = Player.player.userid + "";
            head.setData(Player.player.head, Player.player.userid);
            head.refresh();

            if (rankIndex != -1)
            {
                this.later.text = rankIndex + 1 + "";
            }
            else
            {
                this.later.text = "未上榜";
            }

            this.jushuText.text = jushu + "局";
        }
    }
}
