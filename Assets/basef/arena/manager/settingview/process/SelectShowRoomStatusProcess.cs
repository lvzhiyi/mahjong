namespace basef.arena
{
    public class SelectShowRoomStatusProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaSettingOtherBar bar = GetComponent<ArenaSettingOtherBar>();
            bar.checkedImg(!bar.status);
            ArenaSettingOtherView view = transform.parent.GetComponent<ArenaSettingOtherView>();
            view.showroomnameValue = bar.status?1:0;
        }
    }
}
