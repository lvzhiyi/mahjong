using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary>
    /// 竞赛场换桌面板
    /// </summary>
    public class ArenaChangeTablePanel : UnrealLuaPanel
    {
        ScrollContainer container;

        ArenaRoom[] rooms;

        protected override void xinit()
        {
            container = content.Find("content").Find("container").GetComponent<ScrollContainer>();
            container.init();
        }

        public void setData(ArenaRoom[] rooms)
        {
            this.rooms = rooms;
        }

        protected override void xrefresh()
        {
            container.updateData(back);
            container.resize(rooms.Length);
            container.resizeDelta();
        }

        private void back(ScrollBar bar, int index)
        {
            ArenaChangTableBar temp = (ArenaChangTableBar)bar;
            temp.index_space = index;
            temp.setData(rooms[index]);
            temp.refresh();
        }
    }
}


