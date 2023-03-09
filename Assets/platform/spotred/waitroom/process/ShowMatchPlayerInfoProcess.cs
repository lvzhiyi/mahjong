using platform.spotred.room;

namespace platform.spotred.waitroom
{
    public class ShowMatchPlayerInfoProcess: MouseClickProcess//等待房间和比赛房间
    {
        public override void execute()
        {
            MatchPlayerBar bar = this.transform.GetComponent<MatchPlayerBar>();

            PlayerPropInfoPanel panel = UnrealRoot.root.getDisplayObject<PlayerPropInfoPanel>();
            panel.setMatchPlayer(bar.matchplayer);
            panel.refresh();
            panel.setCVisible(true);
           
        }
    }
}
