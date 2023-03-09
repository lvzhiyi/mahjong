namespace platform.poker
{
    public class PDKAnYueOverSinglePanelReturnProcess : MouseClickProcess
    {
        public override void execute()
        {
            if (Room.room.isStatus(Room.STATE_DESTORY))
            {
                UnrealRoot.root.getDisplayObject<PDKAnYueOverSinglePanel>().setVisible(false);
                if (!PDKAnYueMatch.match.replay)
                {
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().show(Room.room);
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().setVisible(true);
                }
            }
            else
            {
                var panel = PKRoomPanel.GetPanel<PDKAnYueRoomPanel>();
                panel.clearBaseOperate();
                panel.waitView.refresh();
                panel.waitView.setVisible(true);
                UnrealRoot.root.showPanel<PDKAnYueRoomPanel>();
            }
        }
    }
}
