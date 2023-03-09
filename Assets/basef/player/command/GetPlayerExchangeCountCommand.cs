using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.player
{
    /// <summary>
    /// 获取玩家使用代币兑换金币的次数
    /// </summary>
    public class GetPlayerExchangeCountCommand : UserCommand
    {
        public GetPlayerExchangeCountCommand()
        {
            this.id = CommandConst.PORT_PLAYER_TOKEN_EXCHANGE_COUNT;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int count = data.readInt();
            callbackobj = count;
        }
    }
}
