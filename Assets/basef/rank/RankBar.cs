using cambrian.common;
using basef.player;
using cambrian.unreal.scroll;
using cambrian.uui;
using UnityEngine.UI;

namespace basef.rank
{
    public class RankBar : ScrollBar
    {
        private RankPlayer rankPlayer;
        /// <summary>
        /// 排行类型
        /// </summary>
        int type=-1;
        //UI
        /// <summary>
        /// 昵称
        /// </summary>
        private Text nickname;

        /// <summary>
        /// 普通头像
        /// </summary>
        private PlayerHeadView playerHeadView;
        /// <summary>
        /// 圆形头像
        /// </summary>
        private PlayerCircleHeadView playerCircleHeadView;

        /// <summary>
        /// 局数
        /// </summary>
        private Text jushu;

        /// <summary>
        /// 之后排名
        /// </summary>
        private UnrealTextField later;

        private Text id;

        /// <summary>
        /// 前三名奖牌
        /// </summary>
        private SpritesList top3icon;

        protected override void xinit()
        {
            this.nickname = this.transform.Find("name").GetComponent<Text>();

            this.playerHeadView = this.transform.Find("head").GetComponent<PlayerHeadView>();
            if (playerHeadView != null) this.playerHeadView.init();
            this.playerCircleHeadView = this.transform.Find("head").GetComponent<PlayerCircleHeadView>();
            if (playerCircleHeadView != null) this.playerCircleHeadView.init();

            this.jushu = this.transform.Find("jushu").GetComponent<Text>();

            this.later = this.transform.Find("later").GetComponent<UnrealTextField>();
            if (this.transform.Find("id") != null)
                this.id = this.transform.Find("id").GetComponent<Text>();
            this.top3icon = this.transform.Find("rank3").GetComponent<SpritesList>();
        }

        public void setType(int type)
        {
            this.type = type;
        }

        public void setData(RankPlayer rank, int index)
        {
            this.rankPlayer = rank;
            this.nickname.text = rank.getNickName();
            if (type == RankListPanel.CLUB_SCORE_DAY)
                this.jushu.text = RankListPanel.getRankValuleName(type) + MathKit.getRound2LongStr(rank.getMatchCount());
            else
                this.jushu.text = RankListPanel.getRankValuleName(type) + MathKit.getDecimal(rank.getMatchCount());
            if (this.id != null)
                this.id.text = "ID:" + rank.getUserId();

            showtop3icon(index);

            if(playerHeadView!=null)
            {
                playerHeadView.setData(rank.getHead(),rank.getUserId());
                playerHeadView.refresh();
            }

            if (playerCircleHeadView != null)
            {
                playerCircleHeadView.setData(rank.getHead(), rank.getUserId());
                playerCircleHeadView.refresh();
            }
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
                this.later.text = (index + 1) + "";
                this.later.setVisible(true);
            }
        }
    }
}
