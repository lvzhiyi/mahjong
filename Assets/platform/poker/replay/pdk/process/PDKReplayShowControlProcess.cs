namespace platform.poker
{
    public class PDKReplayShowControlProcess : MouseClickSlideProcess
    {
        public override void mouseClick()
        {
            UnrealCheckBox button = transform.GetComponent<UnrealCheckBox>();
            var panel = getRoot<PPDKReplayRoomPanel>();
            panel.rcview.setVisible(button.getState());
            button.reverse();
        }
    }
}