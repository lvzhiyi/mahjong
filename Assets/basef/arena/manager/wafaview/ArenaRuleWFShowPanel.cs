using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary>
    /// 竞赛场玩法显示界面
    /// </summary>
    public class ArenaRuleWFShowPanel : UnrealLuaPanel
    {
        bool freedomEnable;

        ArenaLockRule freedom;

        private ScrollContainer container;

        private ArrayList<ArenaLockRule> list;

        ArrayList<int> showRules;

        protected override void xinit()
        {
            container = transform.Find("Canvas").Find("container").GetComponent<ScrollContainer>();
            container.init();

            list = new ArrayList<ArenaLockRule>();
            freedom = new ArenaLockRule();
            freedom.name = "自由桌";
            showRules = new ArrayList<int>();
        }

        public void setData(bool freedomEnable, int[] showRules, ArrayList<ArenaLockRule> rules)
        {
            this.freedomEnable = freedomEnable;
            this.showRules.clear();
            this.showRules.add(showRules);
            list.clear();
            list.add(freedom);
            list.add(rules.toArray());
        }

        private bool isShow(int uuid)
        {
            if (uuid == 0) return freedomEnable;
            for (int i = 0; i < showRules.count; i++)
            {
                if (showRules.get(i) == uuid) return true;
            }
            return false;
        }

        public void updateShow(int uuid,bool isShow)
        {
            if (uuid == 0) freedomEnable=isShow;
            for (int i = showRules.count; i >= 0; i--)
            {
                if (showRules.get(i) == uuid&&!isShow)
                {
                    showRules.remove(uuid);
                }
            }
            
            if (isShow) showRules.add(uuid);
        }

        protected override void xrefresh()
        {
            container.updateData(callback);
            container.resize(list.count );
        }

        public bool getFreedomEnabel()
        {
            return freedomEnable;
        }

        /// <summary>
        /// 获取选中的规则
        /// </summary>
        /// <returns></returns>
        public ArrayList<int> getSelectRules()
        {
            return showRules;
        }

        public void callback(ScrollBar bar, int index)
        {
            ArenaRuleWFShowBar wfShowBar = (ArenaRuleWFShowBar)bar;
            wfShowBar.index_space = index;
            wfShowBar.setData(list.get(index));
            wfShowBar.isSelected(isShow(list.get(index).uuid));
            wfShowBar.refresh();
        }
    }
}
