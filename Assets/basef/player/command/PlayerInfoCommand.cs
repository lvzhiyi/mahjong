using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.player
{
    /// <summary>
    /// 获取玩家基本信息
    /// </summary>
    public class PlayerInfoCommand:UserCommand
    {
        public PlayerInfoCommand()
        {
            this.id = CommandConst.COMMAND_MAHJONG_PLAYER_INFO;
        }

        public override void bytesRead(ByteBuffer data)
        {
            if (data.length == 0) return;
            this.callbackobj = data;
        }
    }
}
