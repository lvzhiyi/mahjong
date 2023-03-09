namespace platform.poker
{
    public class DDZReplayShowControlProcess : MouseClickSlideProcess
    {
        public override void mouseClick()
        {
            UnrealCheckBox button = transform.GetComponent<UnrealCheckBox>();
            var panel = getRoot<PDDZReplayRoomPanel>();
            panel.rcview.setVisible(button.getState());
            button.reverse();
        }
    }
}
