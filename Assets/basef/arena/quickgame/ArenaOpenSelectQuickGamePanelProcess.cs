
using cambrian.common;
using UnityEngine;

namespace basef.arena
{

    public class ArenaOpenSelectQuickGamePanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArrayList<ArenaLockRule> rules = Arena.arena.fkcSettings.getRules();
            ArenaSelectRulePanel panel = UnrealRoot.root.getDisplayObject<ArenaSelectRulePanel>();
            if (rules != null)
            {
                panel.setData(rules);
                panel.refresh();
                panel.setCVisible(true);
            }
            else
            {
                Alert.show("暂无规则");
            }
        }
    }
}
