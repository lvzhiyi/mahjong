using basef.lobby;

namespace basef.arena
{
    public class ReturnLobbyProcess:MouseClickProcess
    {
        public override void execute()
        {
            ShowLobbyPanel.showLobbyPanel(false);
            root.setVisible(false);
        }
    }
}
