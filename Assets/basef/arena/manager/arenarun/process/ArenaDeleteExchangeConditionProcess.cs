using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 删除兑换条件
    /// </summary>
    public class ArenaDeleteExchangeConditionProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaAwardBar bar = transform.parent.GetComponent<ArenaAwardBar>();
            CommandManager.addCommand(
                new UpdateExchangeSettingCommand(Arena.arena.getId(), 0, bar.entery.uuid,
                    UpdateExchangeSettingCommand.DELETE), back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            getRoot<ArenaManagerPanel>().managerView.runView.awardView.refresh();
        }
    }
}
