using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 内容管理 </summary>
    public class ArenaAccountsMedalContainerView : UnrealLuaSpaceObject
    {
        ScrollContainer container;

        ArrayList<ArenaMedalBill> list = new ArrayList<ArenaMedalBill>();

        private int maxLen = 50;


        protected override void xinit()
        {
            container = this.GetComponent<ScrollContainer>();
            container.init();
        }

        public void setData(ArrayList<ArenaMedalBill> obj)
        {
            list.clear();
            list.add(obj.toArray());
            isMoreData = (obj.count == maxLen);
        }

        protected override void xrefresh()
        {
            container.updateData(updateScrollData);
            container.resize(list.count);
        }

        protected void updateScrollData(ScrollBar bars,int index)
        {
            ArenaAccountsMedalContentBar bar = (ArenaAccountsMedalContentBar)bars;
            bar.setData(list.get(index));
            bar.index_space = index;
            bar.refresh();

            command(index);
        }


        /*--------------------------下拉获取更多数据--------------------------*/

        ArenaAccountsMedalPanel panel;

        /// <summary> 是否还有更多数据 </summary>
        bool isMoreData;

        /// <summary> 当前数据类型 </summary>
        int type;

        /// <summary> UUID </summary>
        long uuid;

        private void command(int index)
        {
            if (index + 1 == list.count && isMoreData)
            {
                if (panel == null) panel = getRoot<ArenaAccountsMedalPanel>();
                type = panel.getType();
                uuid = list.getLast().uuid;
                CommandManager.addCommand(new GetArenaAccountsMedalListCommand(panel.userid,type,uuid),back);
            }
        }

        private void back(object obj)
        {
            if (obj == null) return;
            ArrayList<ArenaMedalBill> data = (ArrayList<ArenaMedalBill>)obj;
            list.add(data.toArray());
            isMoreData = (data.count == maxLen);
            container.incrResize(list.count);
        }
    }
}
