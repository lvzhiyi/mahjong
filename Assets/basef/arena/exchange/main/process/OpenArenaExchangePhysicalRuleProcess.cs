namespace basef.arena
{
    /// <summary> 打开实物奖品兑换规则 </summary>
    public class OpenArenaExchangePhysicalRuleProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaExchangeRulePanel panel = UnrealRoot.root.getDisplayObject<ArenaExchangeRulePanel>();
            panel.setCVisible(true);
        }
    }
}
