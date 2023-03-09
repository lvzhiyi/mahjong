namespace platform.spotred.room
{
    public class OpenSetPanelProcess:MouseClickProcess
    {
        public override void execute()
        {
            SpotRoomSettingPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomSettingPanel>();
            panel.showQuit(!Room.room.isType(Room.JBC));
            panel.refresh();
            panel.setVisible(true);
        }
    }
}
