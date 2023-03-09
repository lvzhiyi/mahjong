namespace basef.arena
{
    public class OpenArenRulePanelDetailInfoProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaRulePanel panel = UnrealRoot.root.getDisplayObject<ArenaRulePanel>();
            if (Arena.arena.fkcSettings.getRules().count == 0)
            {
                Alert.show("未锁定规则");
                return;
            }
            panel.setRule(Arena.arena.fkcSettings.getRules().toArray(), true);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
