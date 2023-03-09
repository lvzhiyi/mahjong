using cambrian.common;

namespace cambrian.game
{
    /// <summary>
    /// 聊天
    /// </summary>
    public class ChatCommand: AccessCommand
    {
        public static TcpConnect connect;

        public override TcpConnect getConnect()
        {
            return connect;
        }
    }
}
