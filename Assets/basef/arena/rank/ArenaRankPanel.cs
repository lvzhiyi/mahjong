using System.Runtime.InteropServices;
using basef.player;
using basef.rank;
using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.arena
{
    public class ArenaRankPanel : UnrealLuaPanel
    {
        /// <summary> 内容 </summary>
        private ScrollContainer container;

        /// <summary> 时间 </summary>
        private ScrollContainer leftContainer;

        string[] nametype = new string[]
        {
            "今日","本周","上周"
        };

        int[] ranktype = new int[]
        {
            0,1,2
        };

        private RankList rankList;

        private int selecttype = 0;

        private ArenaRankSelfView selfView;

        protected override void xinit()
        {
            base.xinit();
            container = content.Find("content").Find("container").GetComponent<ScrollContainer>();
            container.init();
            leftContainer = content.Find("content").Find("left").GetComponent<ScrollContainer>();
            leftContainer.init();
            selfView= content.Find("content").Find("selfview").GetComponent<ArenaRankSelfView>();
            selfView.init();
        }

        public void setData(RankList list)
        {
            this.rankList = list;
        }

        protected override void xrefresh()
        {
            leftContainer.updateData(leftUpdatData);
            leftContainer.resize(ranktype.Length);
            container.updateData(updateRightData);
            container.resize(rankList.getRankList().Length);
            selfView.setData(getRank(), MathKit.getDecimal(rankList.getJueShu()));
            selfView.refresh();
        }

        public void refreshRight()
        {
            container.resize(rankList.getRankList().Length);
            selfView.setData(getRank(), MathKit.getDecimal(rankList.getJueShu()));
            selfView.refresh();
        }

        public void updateRightData(ScrollBar bar,int index)
        {
            RankBar rankBar = (RankBar)bar;
            rankBar.setData(rankList.getRankList()[index],index);
            rankBar.refresh();
        }

        public void setSelectType(int index,int type)
        {
            selecttype = type;
            for (int i = 0; i < ranktype.Length; i++)
            {
                ArenaRankLeftBar bar = (ArenaRankLeftBar)leftContainer.bars.get(i);
                bar.setSelected(type);
                bar.refresh();
            }
        }

        public void leftUpdatData(ScrollBar bar,int index)
        {
            ArenaRankLeftBar leftBar = (ArenaRankLeftBar)bar;
            leftBar.setData(nametype[index],ranktype[index],selecttype);
            leftBar.index_space = index;
            leftBar.refresh();
        }

        public int getRank()
        {
            RankPlayer[] players = rankList.getRankList();
            if (players.Length == 0) return -1;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].getUserId() == Player.player.userid) return i;
            }

            return -1;
        }
    }
}
