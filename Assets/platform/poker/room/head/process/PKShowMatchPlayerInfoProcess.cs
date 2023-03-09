using platform.spotred.room;

namespace platform.poker
{
    public class PKShowMatchPlayerInfoProcess : MouseClickProcess
    {
        public override void execute()
        {
            PKRoomMatchPlayerBar bar = transform.GetComponent<PKRoomMatchPlayerBar>();

            PlayerPropInfoPanel panel = UnrealRoot.root.getDisplayObject<PlayerPropInfoPanel>();
            panel.setMatchPlayer(bar.getPlayer());
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
