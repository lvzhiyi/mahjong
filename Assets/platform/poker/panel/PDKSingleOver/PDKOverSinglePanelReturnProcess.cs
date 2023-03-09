namespace platform.poker
{
    public class PDKOverSinglePanelReturnProcess : MouseClickProcess
    {
        public override void execute()
        {
            if (Room.room.isStatus(Room.STATE_DESTORY))
            {
                UnrealRoot.root.getDisplayObject<PDKOverSinglePanel>().setVisible(false);
                if (!PDKMatch.match.replay)
                {
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().show(Room.room);
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().setVisible(true);
                }
            }
            else
            {
                var panel = PKRoomPanel.GetPanel<PPDKRoomPanel>();
                panel.clearBaseOperate();
                panel.waitView.refresh();
                panel.waitView.setVisible(true);
                UnrealRoot.root.showPanel<PPDKRoomPanel>();
            }
        }
    }
}
