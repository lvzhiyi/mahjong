namespace basef.arena
{

    public class SelectArenaAgentLeftTypeProcess : MouseClickProcess
    {
        public override void execute()
        {
            int type = this.transform.parent.GetComponent<ArenaLeftTypeBar>().index_space;
            ArenaAgentPanel panel = getRoot<ArenaAgentPanel>();
            panel.updateSelect(type);
        }
    }
}
