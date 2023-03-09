using basef.player;
using cambrian.common;
using cambrian.game;
using platform;
using UnityEngine;

namespace basef.arena
{
    public class ArenaRuleWFShowOKProcess : MouseClickProcess
    {
        ArenaRuleWFShowPanel panel;

        bool freedomEnabel;

        ArrayList<int> showRules;

        public override void execute()
        {
            panel = UnrealRoot.root.getDisplayObject<ArenaRuleWFShowPanel>();
            long arenaId = Arena.arena.getId();
            freedomEnabel = panel.getFreedomEnabel();
            showRules = panel.getSelectRules();
            CommandManager.addCommand(new ArenaRuleWFShowCommand(arenaId, freedomEnabel, showRules), back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            if ((bool)obj)
            {
                Alert.show("设置成功");
                Arena.arena.fkcSettings.freedomEnable = freedomEnabel;
                Arena.arena.fkcSettings.showRules = showRules.toArray();
                panel.setVisible(false);
            }
        }
    }
}