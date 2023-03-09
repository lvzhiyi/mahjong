using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    public class ArenaAddAwardOkPorcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaAddAwardPanel panel = getRoot<ArenaAddAwardPanel>();
            if (panel.getCondition() == null || panel.getCondition().Length == 0)
            {
                return;
            }
            long gold = StringKit.parseLong(panel.getCondition());
            if (panel.entery == null)
            {
               
                CommandManager.addCommand(
                    new UpdateExchangeSettingCommand(Arena.arena.getId(), gold, 0, UpdateExchangeSettingCommand.ADD),
                    back);
            }
            else
            {
                CommandManager.addCommand(
                    new UpdateExchangeSettingCommand(Arena.arena.getId(), gold, panel.entery.uuid, UpdateExchangeSettingCommand.UPDATE),
                    back);
            }
        }

        public void back(object obj)
        {
            if (obj == null) return;
            UnrealRoot.root.getDisplayObject<ArenaManagerPanel>().managerView.runView.awardView.refresh();
            root.setVisible(false);
        }
    }
}
