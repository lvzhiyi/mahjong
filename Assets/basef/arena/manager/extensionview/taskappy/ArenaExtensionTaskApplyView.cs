using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    public class ArenaExtensionTaskApplyView:UnrealLuaSpaceObject
    {
        private ScrollContainer container;

        private ArrayList<ArenaEvent> list;

        protected override void xinit()
        {
            container = transform.Find("container").GetComponent<ScrollContainer>();
            container.init();
        }

        public void setList(ArrayList<ArenaEvent> list)
        {
            this.list = list;
        }

        protected override void xrefresh()
        {
            container.updateData(updateData);
            container.resize(list.count);
        }

        public void updateData(ScrollBar bar,int index)
        {
            ArenaExtensionTaskApplyBar taskApply=(ArenaExtensionTaskApplyBar)bar;
            taskApply.setData(list.get(index));
            taskApply.refresh();
        }
    }
}
