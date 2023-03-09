using cambrian.common;
using cambrian.game;

namespace platform.poker
{
    public class DDZOverSinglePanelAgainProcess : MouseClickProcess
    {
        public override void execute()
        {
            DDZMatch.match = null;
            CommandManager.addCommand(new PKPlayerReadyCommand(true, Room.room.getRoomIndex()),back);
            SoundManager.playButton();
            root.setVisible(false);
        }

        void back(object obj)
        {
            var panel = PKRoomPanel.Panel;
            panel.refreshGameView();
            panel.refreshWaitView();
            UnrealRoot.root.showPanel<PDDZRoomPanel>();
        }
    }
}
