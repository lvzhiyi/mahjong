using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 打开推广设置
    /// </summary>
    public class OpenArenAloneSetRuleRadioPanelProcess:MouseClickProcess
    {
        private ArenaNextExtension extension;
        public override void execute()
        {
            extension = getRoot<ArenaAgentPanel>().nextView.operateAgent.extension;
            CommandManager.addCommand(new GetArenaInfoCommand(Arena.arena.getId()),infoback);
        }

        public void infoback(object obj)
        {
            if (obj == null)
            {
                Alert.show("你不是该赛场的成员");
                return;
            }
            CommandManager.addCommand(new GetArenaRadioByAgentCommand(extension.userid), back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            object[] list = (object[])obj;
            ArenaAloneSetRuleRadioPanel panel = UnrealRoot.root.getDisplayObject<ArenaAloneSetRuleRadioPanel>();
            panel.setRebate((RebateList)list[0], (RebateList)list[1], extension.userid);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
