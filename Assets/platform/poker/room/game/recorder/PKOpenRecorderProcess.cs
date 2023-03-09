namespace platform.poker
{
    public class PKOpenRecorderProcess : MouseClickSlideProcess
    {
        private PKRoomPanel panel;

        public override void mouseClick()
        {
            if (!panel) panel = PKRoomPanel.Panel;
            panel.gameView.recorder.showHideOff();
            panel.gameView.recorder.refresh();
        }
    }
}
