using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    public class ArenaListView : UnrealLuaSpaceObject
    {
        ArrayList<Arena> arenas;

        private ScrollContainer container;

        protected override void xinit()
        {
            this.container = this.transform.Find("container").GetComponent<ScrollContainer>();
            this.container.init();
        }

        public void setData(ArrayList<Arena> arenas)
        {
            this.arenas = arenas;
        }

        protected override void xrefresh()
        {
            this.container.updateData(callback);
            this.container.resize(arenas.count);
        }

        public void callback(ScrollBar bar, int index)
        {
            ArenaListBar listBar = (ArenaListBar)bar;
            listBar.setData(arenas.get(index));
            listBar.refresh();
        }
    }
}
