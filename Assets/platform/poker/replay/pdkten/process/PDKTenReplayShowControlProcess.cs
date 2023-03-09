namespace platform.poker
{
    public class PDKTenReplayShowControlProcess : MouseClickSlideProcess
    {
        public override void mouseClick()
        {
            UnrealCheckBox button = transform.GetComponent<UnrealCheckBox>();
            var panel = getRoot<PPDKTenReplayRoomPanel>();
            panel.rcview.setVisible(button.getState());
            button.reverse();
        }
    }
}