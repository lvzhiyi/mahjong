namespace basef.arena
{
    public class SelectChangUITitleProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaChangUITitleTypeBar bar = this.transform.parent.GetComponent<ArenaChangUITitleTypeBar>();
            ArenaChangeUIPanel panel = UnrealRoot.root.getDisplayObject<ArenaChangeUIPanel>();
            panel.setSelect(bar.index_space);
        }
    }
}
