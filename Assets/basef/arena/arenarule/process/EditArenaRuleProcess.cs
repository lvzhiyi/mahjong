namespace basef.arena
{
    public class EditArenaRuleProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaLockRule rule = transform.parent.GetComponent<ArenaWaFaBar>().lockRule;
            ArenaRulePanel panel = UnrealRoot.root.getDisplayObject<ArenaRulePanel>();
            panel.setRule(new ArenaLockRule[1]{ rule},false);
            panel.refresh();
            panel.setCVisible(true);
            panel.setLastPanel(this.root);
            this.root.setCVisible(false);
        }
    }
}
