namespace platform.poker
{
    public class PDKAnYueReplayShowControlProcess : MouseClickSlideProcess
    {
        public override void mouseClick()
        {
            UnrealCheckBox button = transform.GetComponent<UnrealCheckBox>();
            var panel = getRoot<PPDKAnYueReplayRoomPanel>();
            panel.rcview.setVisible(button.getState());
            button.reverse();
        }
    }
}