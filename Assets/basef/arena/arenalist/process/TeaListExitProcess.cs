using basef.lobby;

namespace basef.teahouse
{
    public class TeaListExitProcess: ExitPanelProcess
    {
        public override void execute()
        {
            base.execute();
            ShowLobbyPanel.showLobbyPanel(false);
        }
    }
}
