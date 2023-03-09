using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 金豆明细 内容管理与筛选 </summary>
    public class ArenaAccountsGoldContainerView : UnrealLuaSpaceObject
    {
        ScrollContainer container;

        ArrayList<ArenaAccountsGoldContentData> list = new ArrayList<ArenaAccountsGoldContentData>();

        protected override void xinit()
        {
            container = this.GetComponent<ScrollContainer>();
            container.init();
        }

        public void setData(ArrayList<ArenaAccountsGoldContentData> obj)
        {
            list.clear();
            list.add(obj.toArray());
            isMoreData = obj.count == 20;
        }

        protected override void xrefresh()
        {
            container.updateData(updateScrollData);
            container.resize(list.count);
        }

        protected void updateScrollData(ScrollBar bars,int index)
        {
            ArenaAccountsGoldContentBar bar = (ArenaAccountsGoldContentBar)bars;
            bar.setData(list.get(index));
            bar.index_space = index;
            bar.refresh();

            command(index);
        }

        /*--------------------------下拉获取更多数据--------------------------*/

        ArenaAccountsGoldPanel panel;

        /// <summary> 是否还有更多数据 </summary>
        bool isMoreData;

        /// <summary> 当前数据类型 </summary>
        int type;

        /// <summary> 是否查看金豆流水不足 </summary>
        bool checkgoldminus;

        /// <summary> UUID </summary>
        long uuid;

        private void command(int index)
        {
            if (index + 1 == list.count && isMoreData)
            {
                if (panel == null) panel = getRoot<ArenaAccountsGoldPanel>();
                type = panel.getType();
                checkgoldminus = panel.getCheckGoldminus();
                uuid = list.getLast().uuid;
                CommandManager.addCommand(new GetArenaAccountsGoldListCommand(panel.userid,type,checkgoldminus,uuid),back);
            }
        }

        private void back(object obj)
        {
            if (obj == null) return;
            ArrayList<ArenaAccountsGoldContentData> data = (ArrayList<ArenaAccountsGoldContentData>)obj;
            list.add(data.toArray());
            container.incrResize(list.count);
        }
    }
}
