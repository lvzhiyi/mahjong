namespace platform.mahjong
{
    /// <summary>
    /// 打开设置界面
    /// </summary>
    public class OpenMJSettingPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            MJRoomSettingPanel panel = UnrealRoot.root.getDisplayObject<MJRoomSettingPanel>();
            if (Room.room.roomRule.getGameNum() > -1)
            {
                panel.showQuit(true);
            }
            else
            {
                panel.showQuit(false);
            }
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
