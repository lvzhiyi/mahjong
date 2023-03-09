namespace platform.poker
{
    public class PDKTenOverSinglePanelReturnProcess : MouseClickProcess
    {
        public override void execute()
        {
            if (Room.room.isStatus(Room.STATE_DESTORY))
            {
                UnrealRoot.root.getDisplayObject<PDKTenOverSinglePanel>().setVisible(false);
                if (!PDKTenMatch.match.replay)
                {
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().show(Room.room);
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().setVisible(true);
                }
            }
            else
            {
                var panel = PKRoomPanel.GetPanel<PDKTenRoomPanel>();
                panel.clearBaseOperate();
                panel.waitView.refresh();
                panel.waitView.setVisible(true);
                UnrealRoot.root.showPanel<PDKTenRoomPanel>();
            }
        }
    }
}
