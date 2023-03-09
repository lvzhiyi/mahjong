using cambrian.common;
using cambrian.game;

namespace platform.poker
{
    public class PDKTenOverSinglePanelAgainProcess:MouseClickProcess
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
            UnrealRoot.root.showPanel<PDKTenRoomPanel>();
        }
    }
}
