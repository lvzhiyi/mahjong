using platform.spotred.room;

namespace platform.spotred.waitroom
{
    public class OpenWaitSettingPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            SpotRoomSettingPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomSettingPanel>();

            if (Room.room.isType(Room.CLUB | Room.JBC))
            {
                panel.showQuit(false);
            }
            else
            {
                if (Room.room.roomRule.getGameNum() > -1)
                {
                    panel.showQuit(true);
                }
                else
                {
                    panel.showQuit(false);
                }
            }
            
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
