
using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.arena
{
    public class ArenaSelectRulePanel : UnrealLuaPanel
    {
        private ScrollTableContainer container;

        ArrayList<ArenaLockRule> lockRules;

        protected override void xinit()
        {
            container = transform.Find("Canvas").Find("container").GetComponent<ScrollTableContainer>();
            container.init();
        }

        public void setData(ArrayList<ArenaLockRule> lockRules)
        {
            this.lockRules = lockRules;
        }

        protected override void xrefresh()
        {
            this.container.updateData(updateData);
            this.container.resize(lockRules.count);
        }


        public void updateData(ScorllTableBar bar, int index)
        {
            ArenaQuickGameRuleBar ruleBar = (ArenaQuickGameRuleBar)bar;
            ruleBar.setData(lockRules.get(index));
            ruleBar.refresh();
        }
    }
}
