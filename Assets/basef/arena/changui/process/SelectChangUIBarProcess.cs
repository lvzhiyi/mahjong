using cambrian.unreal.scroll;

namespace basef.arena
{
    public class SelectChangUIBarProcess : MouseClickProcess
    {
        public override void execute()
        {
            ScorllTableBar bar = this.transform.GetComponent<ScorllTableBar>();
            ArenaChangeUIPanel panel = getRoot<ArenaChangeUIPanel>();
            panel.selectedBar(bar.index_space);
        }
    }
}
