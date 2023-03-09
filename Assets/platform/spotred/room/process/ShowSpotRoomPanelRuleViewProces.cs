namespace platform.spotred.room
{
    public class ShowSpotRoomPanelRuleViewProces:MouseClickProcess
    {
        public override void execute()
        {
            this.getRoot<SpotRoomPanel>().showRuleView();
        }
    }
}
