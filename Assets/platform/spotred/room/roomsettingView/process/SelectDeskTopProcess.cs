namespace platform.spotred.room
{
    /// <summary>
    /// 选择桌布
    /// </summary>
    public class SelectDeskTopProcess:MouseClickProcess
    {
        public override void execute()
        {
            DeskTopBar bar = GetComponent<DeskTopBar>();
            this.getRoot<SpotRoomSettingPanel>().selectedDeskTop(bar.index_space);
            DeskTopManager.instance.setDeskTopStyle(bar.type);
            this.getRoot<SpotRoomSettingPanel>().executeAction();
        }
    }
}
