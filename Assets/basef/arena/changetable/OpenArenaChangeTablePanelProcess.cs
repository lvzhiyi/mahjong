using cambrian.common;

namespace basef.arena
{
    /// <summary> 打开赛场换桌面板 </summary>
    public class OpenArenaChangeTablePanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new GetArenaCanChangeRoomListCommand(), back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            ArenaRoom[] rooms = (ArenaRoom[])obj;
            ArenaChangeTablePanel panel = UnrealRoot.root.getDisplayObject<ArenaChangeTablePanel>();
            panel.setData(rooms);
            panel.refresh();
            panel.setVisible(true);
            panel.setLastPanel(root);
        }
    }
}
