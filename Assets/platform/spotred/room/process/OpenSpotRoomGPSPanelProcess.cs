using platform.spotred.waitroom;

namespace platform.spotred.room
{
    public class OpenSpotRoomGPSPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            MatchPlayer[] players = Room.room.players;
            IpGPS3Panel panel = UnrealRoot.root.getDisplayObject<IpGPS3Panel>();
            panel.setMatchPlayers(players);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
