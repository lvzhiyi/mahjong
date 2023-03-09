namespace basef.arena
{
    public class SaveChangUIProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaChangeUIPanel panel = getRoot<ArenaChangeUIPanel>();
            panel.saveSetting();
        }
    }
}
