using basef.arena;
using basef.lobby;
using cambrian.common;

namespace platform.mahjong
{
    public class MJRetuanLobbyPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            Room room = null;
            if (UnrealRoot.root.checkDisplayObject<MJAllOverPanel>()!=null)
            {
                room = getRoot<MJAllOverPanel>().room;
            }

            show(room);
        }

        public void show(Room room)
        {
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
            ArenaPanel panel = UnrealRoot.root.getDisplayObject<ArenaPanel>();
            panel.refresh();
            UnrealRoot.root.showPanel<ArenaPanel>();
        }
    }
}
