namespace platform.mahjong
{
    public class MJReplayShowControlProcess : MouseClickProcess
    {
        public override void execute()
        {
            UnrealCheckBox button = this.transform.GetComponent<UnrealCheckBox>();
            ReplayMahjongRoomPanel panel = getRoot<ReplayMahjongRoomPanel>();
            panel.rcview.setVisible(button.getState());
            button.reverse();
        }
    }
}
