namespace platform.poker
{
    public class DDZOverSinglePanelReturnProcess : MouseClickProcess
    {
        public override void execute()
        {
            if (Room.room == null)
            {
                setVisible(false);
                return;
            }
            if (Room.room.isStatus(Room.STATE_DESTORY))
            {
                UnrealRoot.root.getDisplayObject<DDZOverSinglePanel>().setVisible(false);
                if (!DDZMatch.match.replay)
                {
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().show(Room.room);
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().setVisible(true);
                }
            }
            else
            {
                var panel = PKRoomPanel.GetPanel<PDDZRoomPanel>();
                panel.clearBaseOperate();
                panel.waitView.refresh();
                panel.waitView.setVisible(true);
                UnrealRoot.root.showPanel<PDDZRoomPanel>();
            }
        }
    }
}
