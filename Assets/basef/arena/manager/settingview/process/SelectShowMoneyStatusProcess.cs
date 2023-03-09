namespace basef.arena
{
    public class SelectShowMoneyStatusProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaSettingOtherBar bar = GetComponent<ArenaSettingOtherBar>();
            bar.checkedImg(!bar.status);
            ArenaSettingOtherView view = transform.parent.GetComponent<ArenaSettingOtherView>();
            view.showmoneyValue = bar.status?1:0;
        }
    }
}
