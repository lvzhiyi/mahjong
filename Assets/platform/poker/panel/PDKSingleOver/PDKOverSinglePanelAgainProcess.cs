using cambrian.common;
using cambrian.game;

namespace platform.poker
{
    /// <summary> 跑得快 下一局 再来一局 </summary>
    public class PDKOverSinglePanelAgainProcess : MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new PKPlayerReadyCommand(true, Room.room.getRoomIndex()), back);
            SoundManager.playButton();
            root.setVisible(false);
        }

        private void back(object obj)
        {
            var panel = PKRoomPanel.Panel;
            panel.refreshGameView();
            panel.refreshWaitView();
            UnrealRoot.root.showPanel<PPDKRoomPanel>();
        }
    }
}
