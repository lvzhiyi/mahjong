using basef.player;
using cambrian.unreal.scroll;

namespace basef.rank
{
    /// <summary>
    /// 金币排行榜右边视图
    /// </summary>
    public class GoldRankRightView:UnrealLuaSpaceObject
    {
        private ScrollContainer container;

        private UnrealRadioList times;

        private GoldRankGroup group;

        private RankList list;

        private GoldBottomView bottomView;


        protected override void xinit()
        {
            base.xinit();
            this.container = this.transform.Find("container").GetComponent<ScrollContainer>();
            this.container.init();
            this.times = this.transform.Find("titles").Find("time").GetComponent<UnrealRadioList>();
            this.times.init();
            this.bottomView = this.transform.Find("bottom").GetComponent<GoldBottomView>();
            this.bottomView.init();
        }

        public void setData(GoldRankGroup group, RankList list)
        {
            this.group = group;
            this.list = list;
        }

        protected override void xrefresh()
        {
            this.container.updateData(callBack);
            this.container.resize(list.getRankList().Length);
            this.times.resize(group.rankNames.Length);
            for (int i = 0; i < this.times.size; i++)
            {
                GoldRankDayBar bar=(GoldRankDayBar)this.times.bars[i];
                bar.index_space = i;
                bar.setTitle(this.group.rankNames[i],@group.rankTypes[i]);
            }
            this.times.selectedIndex(0);
            this.bottomView.setData(list.getJueShu(), getRank(), @group.id);
            this.bottomView.refresh();  
        }

        /// <summary>
        /// 获取自己排名
        /// </summary>
        /// <returns></returns>
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

        public void selectedDayIndex(int index)
        {
            this.times.selectedIndex(index);
        }

        public void callBack(ScrollBar bar, int index)
        {
            GoldRankBar rank = (GoldRankBar) bar;
            rank.setData(list.getRankList()[index], index, @group.id);
        }
    }
}
