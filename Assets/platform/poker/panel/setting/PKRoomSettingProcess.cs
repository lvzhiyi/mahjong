namespace platform.poker
{
    /// <summary> 打开设置面板 </summary>
    public class PKRoomSettingProcess : MouseClickProcess
    {
        public override void execute()
        {
            var setting = UnrealRoot.root.getDisplayObject<PKRoomSettingPanel>();
            setting.showQuit(Room.room.roomRule.getGameNum()>-1);
            setting.refresh();
            setting.setCVisible(true);
        }
    }
}
