using cambrian.common;

namespace basef.rank
{
    /// <summary>
    /// 金币排行榜左边视图
    /// </summary>
    public class GoldRankLeftView:UnrealLuaSpaceObject
    {
        private UnrealListContainer container;

        private ArrayList<GoldRankGroup> groups;

        protected override void xinit()
        {
            base.xinit();
            this.container = this.transform.Find("center").Find("mask").Find("container").GetComponent<UnrealListContainer>();
            this.container.init();
        }

        public void setGroups(ArrayList<GoldRankGroup> groups)
        {
            this.groups = groups;
        }

        protected override void xrefresh()
        {
            this.container.resize(groups.count);
            for (int i = 0; i < groups.count; i++)
            {
                GoldRankTypeBar bar=(GoldRankTypeBar)this.container.bars[i];
                bar.setGoldRandGroup(groups.get(i),i==0);
                bar.index_space = i;
                bar.refresh();
            }
            this.container.relayout();
            this.container.resizeDelta();
        }

        public void refreshSelected(int index)
        {
            for (int i = 0; i < groups.count; i++)
            {
                GoldRankTypeBar bar = (GoldRankTypeBar)this.container.bars[i];
                bar.setGoldRandGroup(groups.get(i), i == index);
                bar.refresh();
            }
        }
    }
}
