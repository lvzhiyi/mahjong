using basef.rule;

namespace basef.arena
{
    /// <summary>
    /// 选择竞赛场规则
    /// </summary>
    public class SelectArenaRuleTypeProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaRuleBar bar = transform.GetComponent<ArenaRuleBar>();
            bar.selected();
            this.getRoot<ArenaRulePanel>().showView(bar.lockRule, bar.index_space);
        }
    }
}
