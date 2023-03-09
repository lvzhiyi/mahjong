using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary>
    /// 竞赛场统计界面
    /// </summary>
    public class ArenaWfPanel : UnrealLuaPanel
    {

        private ScrollContainer container;

        private ArrayList<ArenaLockRule> list;

        protected override void xinit()
        {
            container = transform.Find("Canvas").Find("content").Find("view").Find("typeshow").GetComponent<ScrollContainer>();
            container.init();
        }

        protected override void xrefresh()
        {
            list = Arena.arena.fkcSettings.getRules();
            container.updateData(callback);
            container.resize(list.count);
        }

        public void callback(ScrollBar bar, int index)
        {
            ArenaWaFaBar waFaBar = (ArenaWaFaBar)bar;
            waFaBar.index_space = index;
            waFaBar.setData(list.get(index));
            waFaBar.refresh();
        }
    }
}
