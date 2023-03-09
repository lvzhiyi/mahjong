using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary>
    /// 赛场玩法视图
    /// </summary>
    public class ArenaWaFaView:UnrealLuaSpaceObject
    {
        private ScrollContainer container;
        /// <summary>
        /// 玩法显示按钮
        /// </summary>
        private UnrealTextScaleButton showwf;
        protected override void xinit()
        {
            container = transform.Find("typeshow").GetComponent<ScrollContainer>();
            container.init();
            showwf = transform.Find("button").Find("show").GetComponent<UnrealTextScaleButton>();
            showwf.init();
        }

        private ArrayList<ArenaLockRule> list;
        protected override void xrefresh()
        {
            showwf.setVisible(true);
            list= Arena.arena.fkcSettings.getRules();
            container.updateData(callback);
            container.resize(list.count);
            if (list.count == 0)
            {
                showwf.setVisible(false);
            }
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
