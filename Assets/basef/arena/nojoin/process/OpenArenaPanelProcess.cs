namespace basef.arena
{
    public class OpenArenaPanelProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaPanel panel = UnrealRoot.root.getDisplayObject<ArenaPanel>();
            panel.refresh();
            UnrealRoot.root.showPanel<ArenaPanel>();
        }
    }
}
