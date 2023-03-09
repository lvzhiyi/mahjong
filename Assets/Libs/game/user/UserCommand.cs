using cambrian.common;

namespace cambrian.game
{
    /// <summary>
    /// 用户通信基类
    /// </summary>
    public class UserCommand : AccessCommand
    {

        public static TcpConnect connect;

        public override TcpConnect getConnect()
        {
            return connect;
        }
    }
}