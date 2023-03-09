using cambrian.common;
using cambrian.game;
using scene.game;

namespace platform
{
    /// <summary>
    /// 10分钟不再接收邀请
    /// </summary>
    public class NOAcceptInviteTimeCommand:UserCommand
    {
        public NOAcceptInviteTimeCommand()
        {
            id = CommandConst.PORT_CLUB_ROOM_NO_ACCEPT_INVITE_TIME;
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readBoolean();
        }
    }
}
