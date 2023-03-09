namespace basef.arena
{
    /// <summary>
    /// 选择赛场打烊
    /// </summary>
    public class SelectCloseArenaStatusProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaSettingOtherBar bar = GetComponent<ArenaSettingOtherBar>();
            bar.checkedImg(!bar.status);
            ArenaSettingOtherView view = transform.parent.GetComponent<ArenaSettingOtherView>();
            view.suspendValue = bar.status;
        }
    }
}
