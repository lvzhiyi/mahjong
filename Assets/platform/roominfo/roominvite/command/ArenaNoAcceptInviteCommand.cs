using cambrian.common;
using cambrian.game;
using scene.game;

namespace platform
{
    /// <summary>
    /// 竞赛场10分钟不再接收邀请
    /// </summary>
    public class ArenaNoAcceptInviteCommand : UserCommand
    {
        public ArenaNoAcceptInviteCommand()
        {
            id = CommandConst.PORT_ARENA_REFUSE_INVITE;
        }


        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readBoolean();
        }
    }
}
