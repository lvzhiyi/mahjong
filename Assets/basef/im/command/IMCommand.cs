using cambrian.common;
using scene.game;

namespace basef.im
{
    public class IMCommand : AccessCommand
    {
        public override TcpConnect getConnect()
        {
            return ConnectRoot.gameconnect;
        }
    }
}
