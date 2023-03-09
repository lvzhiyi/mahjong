using basef.arena;
using basef.lobby;
using cambrian.common;

namespace platform.poker
{
    public class PKOverLumpPanelReturnProcess : MouseClickProcess
    {
        public override void execute()
        {
            Room room = getRoot<PKAllOverPanel>().room;

            if (room == null)
            {
                ShowLobbyPanel.showLobbyPanel();
                return;
            }

            if (room.isType(Room.ARENA) && room.getBind() > 0)
            {
                CommandManager.addCommand(new GetArenaInfoCommand(Room.room.getBind()), arenaBack);
            }
            else
            {
                ShowLobbyPanel.showLobbyPanel();
                Room.clear();
            }
        }

        public void arenaBack(object obj)
        {
            if (obj == null)
            {
                ShowLobbyPanel.showLobbyPanel();
                return;
            }
            UnrealRoot.root.getDisplayObject<ArenaPanel>().refresh();
            UnrealRoot.root.showPanel<ArenaPanel>();                        
            Room.clear();
        }
    }
}
