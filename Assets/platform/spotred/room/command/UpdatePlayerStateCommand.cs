using cambrian.common;
using cambrian.game;
using scene.game;

namespace platform.spotred.room
{
    /// <summary>
    /// 更新玩家状态(切出去代表离线)
    /// </summary>
    public class UpdatePlayerStateCommand: UserCommand
    {
        private bool online;
        public UpdatePlayerStateCommand(bool online)
        {
            this.id = CommandConst.COMMAND_PLAYER_CONNECT_STATUS;
            this.online = online;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeBoolean(online);
        }
    }
}
