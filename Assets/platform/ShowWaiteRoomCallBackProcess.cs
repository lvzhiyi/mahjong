using platform.poker;
using platform.mahjong;
using platform.spotred;
using platform.spotred.waitroom;

namespace platform
{
    /// <summary>
    /// 创建房间，回调执行
    /// </summary>
    public class ShowWaiteRoomCallBackProcess : Process
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
                UnrealRoot.root.showPanel<SpotWaitRoomPanel>();

                if (playercount > 1)
                {
                    MatchPlayer[] players = Room.room.players;
                    IpGPS3Panel ipgps = UnrealRoot.root.getDisplayObject<IpGPS3Panel>();
                    ipgps.setMatchPlayers(Room.room.players);
                    ipgps.refresh();
                    ipgps.setCVisible(true);
                }
            }
            else if (platform == GamePlatform.MJ_GAME)
            {
                MahJongPanel panel = MahJongPanel.getPanel();
                panel.refresh();
                panel.showWaitView();
                MahJongPanel.showPanel();
                if (playercount > 1)
                    panel.showIPView();
            }
            else if (platform == GamePlatform.PK_GAME)
            {
                PKGAME.CreatRoom();
            }
        }
    }
}
