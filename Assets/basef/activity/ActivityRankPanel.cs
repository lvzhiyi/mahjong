using basef.player;
using basef.rank;
using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.activity
{
    public class ActivityRankPanel:UnrealLuaPanel
    {
        private ScrollContainer container;

        /// <summary> 排行 </summary>
        private RankList list;

        /// <summary> 排行信息 </summary>
        private Text info;

        protected override void xinit()
        {
            this.container = this.transform.Find("Canvas").Find("content").Find("container").GetComponent<ScrollContainer>();
            this.container.init();

            this.info = this.transform.Find("Canvas").Find("content").Find("top").Find("info").GetComponent<Text>();
        }

        protected override void xrefresh()
        {
            this.container.updateData(updateBarData);

            int len = list.getRankList().Length;

            this.container.resize(len);

            int rank_index = getRank();
            if (rank_index != -1)
            {
                this.info.text = "你在排行榜中第" + (rank_index + 1) + "名";
            }
            else
            {
                this.info.text = "你暂未进入排行榜";
            }
        }

        public void addRandList(RankList list)
        {
            this.list = list;
        }

        public void updateBarData(ScrollBar bar, int index)
        {
            ((RankBar)bar).setData(list.getRankList()[index], index);
        }

        /// <summary> 获取排名 </summary>
        public int getRank()
        {
            RankPlayer[] players = list.getRankList();
            if (players.Length == 0) return -1;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].getUserId() == Player.player.userid) return i;
            }

            return -1;
        }
    }
}
