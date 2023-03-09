namespace platform.spotred.waitroom
{
    public class OpenGPSPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            if (Room.room == null) return;
            MatchPlayer[] players = Room.room.players;
            IpGPS3Panel panel = UnrealRoot.root.getDisplayObject<IpGPS3Panel>();
            panel.setMatchPlayers(players);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
