namespace platform.mahjong
{
    public class AYMJReplayShowControlProcess : MouseClickProcess
    {
        public override void execute()
        {
            UnrealCheckBox button = this.transform.GetComponent<UnrealCheckBox>();
            ReplayAYMJRoomPanel panel = getRoot<ReplayAYMJRoomPanel>();
            panel.rcview.setVisible(button.getState());
            button.reverse();
        }
    }
}
