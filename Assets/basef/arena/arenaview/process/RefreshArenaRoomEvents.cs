using cambrian.common;

namespace basef.arena
{
    public class RefreshArenaRoomEvents : Process
    {
        public override void execute()
        {
            CommandManager.addCommand(new GetArenaRoomListCommand(Arena.arena.getId()), getArenaRoom);
        }

        public void getArenaRoom(object obj)
        {
            if (obj == null) return;
            ArenaPanel panel = UnrealRoot.root.getDisplayObject<ArenaPanel>();
            ArrayList<ArenaRoom> list = (ArrayList<ArenaRoom>)obj;
            panel.deskView.setClub(list);
            panel.deskView.refreshRoom();
        }
    }
}
