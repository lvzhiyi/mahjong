namespace basef.arena
{
    public class ArenaShowMatchRoomProcess : MouseClickProcess
    {
        public override void execute()
        {
            getRoot<ArenaPanel>().deskView.setMatchRoom(!getRoot<ArenaPanel>().deskView.isMatchRoom);
            getRoot<ArenaPanel>().deskView.refresh();
        }
    }
}
