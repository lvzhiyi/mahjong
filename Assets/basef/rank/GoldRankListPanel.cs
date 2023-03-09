using cambrian.common;
using UnityEngine;

namespace basef.rank
{
    public class GoldRankListPanel:UnrealLuaPanel
    {
        private GoldRankLeftView leftView;

        private GoldRankRightView rightView;
        /// <summary>
        /// 左边菜单列表
        /// </summary>
        private ArrayList<GoldRankGroup> groups;

        private RankList rankList;

        /// <summary>
        /// 选中的排行类型
        /// </summary>
        [HideInInspector]
        public int selectedGroupIndex { set; get; }

        protected override void xinit()
        {
            base.xinit();
            this.leftView = this.content.Find("content").Find("left").GetComponent<GoldRankLeftView>();
            this.leftView.init();
            this.rightView = this.content.Find("content").Find("right").GetComponent<GoldRankRightView>();
            this.rightView.init();
        }

        public void setGroups(ArrayList<GoldRankGroup> groups)
        {
            this.groups = groups;
        }

        public void setRankList(RankList rankList)
        {
            this.rankList = rankList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">左边的索引</param>
        public void refreshRightView(int index)
        {
            this.selectedGroupIndex = index;
            this.rightView.setData(groups.get(index), this.rankList);
            this.rightView.refresh();
        }

        public void refreshRightViewDayIndex(int index)
        {
            this.rightView.selectedDayIndex(index);
        }

        public void refreshLeftView(int index)
        {
            this.leftView.refreshSelected(index);
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            this.leftView.setGroups(groups);
            this.leftView.refresh();
            this.rightView.setData(groups.get(0),this.rankList);
            this.rightView.refresh();
        }
    }
}
