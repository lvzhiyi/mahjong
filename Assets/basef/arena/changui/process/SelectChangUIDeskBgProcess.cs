using cambrian.unreal.scroll;

namespace basef.arena
{
    public class SelectChangUIDeskBgProcess : MouseClickProcess
    {
        public override void execute()
        {
            ScorllTableBar bar = this.transform.GetComponent<ScorllTableBar>();
            ChangUIRuleView view = this.transform.parent.parent.parent.parent.GetComponent<ChangUIRuleView>();
            view.selectDeskBg(bar.index_space);
        }
    }
}
