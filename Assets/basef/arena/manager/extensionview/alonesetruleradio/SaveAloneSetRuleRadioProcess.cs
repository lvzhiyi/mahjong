using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 保存独立设置
    /// </summary>
    public class SaveAloneSetRuleRadioProcess:MouseClickProcess
    {
        private ArenaAloneSetRuleRadioPanel panel;

        public override void execute()
        {
            panel = getRoot<ArenaAloneSetRuleRadioPanel>();
            if (panel.list.special)
                CommandManager.addCommand(new SaveArenaRadioByAgentCommand(panel.dest, panel.list), back);
            else
            {
                CommandManager.addCommand(new ArenaRuleRateCancelCommand(panel.dest), back);
            }
        }

        public void back(object obj)
        {
            if (obj == null) return;
            RebateList list=(RebateList)obj;
            panel.setRebates(list);
            panel.refresh();
            Alert.autoShow("保存成功");
        }
    }
}
