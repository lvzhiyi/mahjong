using cambrian.common;

namespace platform
{
    /// <summary>
    /// 同意加入房间
    /// </summary>
    public class AgreeJoinRoomProcess:MouseClickProcess
    {
        public override void execute()
        {
            RoomPlayerInvitePanel panel= getRoot<RoomPlayerInvitePanel>();
            int roomid = 0;
            if (panel.type == 1)
            {
                roomid = panel.inviteInfo.roomid;
            }
            else if (panel.type == 3)
            {
                roomid = panel.inviteInfo.roomid;
            }
            else
            {
                roomid = panel.againGame.roomid;
            }
            UnrealRoot.root.getDisplayObject<BackRoomPanel>().setVisible(false) ;
            CommandManager.addCommand(new AgreeInviteJoinRoomCommand(roomid),back);
            
        }

        public void back(object obj)
        {
            if (obj == null)
            {
                RoomPlayerInvitePanel panel = getRoot<RoomPlayerInvitePanel>();
                panel.setVisible(false);
                return;
            }
        }
    }
}
