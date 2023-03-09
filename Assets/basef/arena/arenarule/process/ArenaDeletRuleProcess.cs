using cambrian.common;

namespace basef.arena
{
    public class ArenaDeletRuleProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaLockRule rule= transform.parent.GetComponent<ArenaWaFaBar>().lockRule;
            CommandManager.addCommand(new ArenaCreateAndDeleteCommand(Arena.arena.getId(),false, rule),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            ArenaWfPanel panel = getRoot<ArenaWfPanel>();
            panel.refresh();
        }
    }
}
