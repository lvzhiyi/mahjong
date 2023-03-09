using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 统计视图
    /// </summary>
    public class ArenaStatisticsView : UnrealLuaSpaceObject
    {
        private UnrealTableContainer container;

        private ArrayList<ArenaStatistcsBarData> list = new ArrayList<ArenaStatistcsBarData>();

        protected override void xinit()
        {
            container = transform.Find("scroll").Find("mask").Find("container").GetComponent<UnrealTableContainer>();
            container.init();
        }

        protected override void xrefresh()
        {
            container.resize(list.count);
            for (int i = 0; i < list.count; i++)
            {
                ArenaStatistcsBar bar = (ArenaStatistcsBar)container.bars[i];
                bar.setData(list.get(i));
                bar.index_space = i;
                bar.refresh();
            }
        }

        public void setData(ArrayList<ArenaStatistcsBarData> list)
        {
            this.list = list;
        }
    }
}
