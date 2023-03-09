namespace basef.arena
{
    public class SelectArenaRankShowStatusProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaSettingOtherBar bar = GetComponent<ArenaSettingOtherBar>();
            bar.checkedImg(!bar.status);
            ArenaSettingOtherView view = transform.parent.GetComponent<ArenaSettingOtherView>();
            view.rankValue = bar.status;
        }
    }
}
