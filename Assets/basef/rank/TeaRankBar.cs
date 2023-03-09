using basef.player;
using cambrian.unreal.scroll;
using cambrian.uui;
using UnityEngine.UI;

namespace basef.rank
{
    public class TeaRankBar : ScrollBar
    {

        //UI
        /// <summary>
        /// 昵称
        /// </summary>
        private Text nickname;

        /// <summary>
        /// 局数
        /// </summary>
        private Text jushu;

        /// <summary>
        /// 之后排名
        /// </summary>
        private UnrealTextField later;

        private Text id;

        private TeaRank rank;

        /// <summary>
        /// 前三名奖牌
        /// </summary>
        private SpritesList top3icon;

        /// <summary>
        /// 头像
        /// </summary>
        private PlayerHeadView playerHeadView;

        protected override void xinit()
        {
            this.nickname = this.transform.Find("name").GetComponent<Text>();
            this.jushu = this.transform.Find("jushu").GetComponent<Text>();

            this.later = this.transform.Find("later").GetComponent<UnrealTextField>();
            if (this.transform.Find("id") != null)
                this.id = this.transform.Find("id").GetComponent<Text>();
            this.top3icon = this.transform.Find("rank3").GetComponent<SpritesList>();
            this.playerHeadView = this.transform.Find("info").GetComponent<PlayerHeadView>();
        }

        public void setData(int index,TeaRank rank)
        {
            this.rank = rank;
            this.nickname.text = rank.clubname;
            this.jushu.text = rank.match+"";

            playerHeadView.setData(rank.masterhead, rank.clubid);
            playerHeadView.refresh();

            showtop3icon(index);
        }
        private void showtop3icon(int index)
        {
            this.later.setVisible(false);
            this.top3icon.setVisible(false);
            if (index < 3)
            {
                this.top3icon.setVisible(true);
                this.top3icon.ShowIndex(index);
            }
            else
            {
                this.later.text = "NO."+(index + 1);
                this.later.setVisible(true);
            }
        }
    }
}
