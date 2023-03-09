using platform.mahjong;
using platform.poker;
using platform.spotred.waitroom;

namespace platform
{
    public class ShowConnectWaiteRoomCallBackProcess:Process
    {
        public override void execute()
        {
            Room room = Room.room;
            if (room == null) return;
            int playercount = room.getRoomRealPlayerCount();
            int platform = room.getRule().getPlatFormValue();
            if (platform == GamePlatform.CP_GAME)
            {
                SpotWaitRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotWaitRoomPanel>();
                panel.refresh();
                panel.setVisible(true);

            }
            else if (platform == GamePlatform.MJ_GAME)
            {
                MahJongPanel panel = MahJongPanel.getPanel();
                panel.refresh();
                panel.showWaitView();
                panel.setVisible(true);
            }
            else if (platform == GamePlatform.PK_GAME)
            {
                PKGAME.ReconnectCreatRoom();
            }
        }
    }
}
