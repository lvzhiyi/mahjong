using cambrian.common;

namespace cambrian.game
{
    public class ServerStatusCommmand : UserCommand
    {
        public ServerStatusCommmand()
        {
            this.id = ProxyDataHandler.PORT_TIME;
        }
        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            if (data.length == 0) return;
            long time = data.readLong();
            TimeKit.currentTimeMillis = time;
            this.callbackobj = time;
        }
    }
}