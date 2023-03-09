using basef.arena;
using basef.lobby;
using cambrian.common;
using platform.spotred.over;

namespace platform.spotred.room
{
    /// <summary>
    /// 总结算界面显示，退到大厅界面。
    /// </summary>
    public class RetuanLobbyPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.setVisible(false);

            Room room = getRoot<AllOverPanel>().room;

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
