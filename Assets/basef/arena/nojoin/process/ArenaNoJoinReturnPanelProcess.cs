using basef.lobby;

namespace basef.arena
{
    /// <summary> 未参加竞赛场面板 返回大厅 </summary>
    public class ArenaNoJoinReturnPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            ShowLobbyPanel.showLobbyPanel();
        }
    }
}
