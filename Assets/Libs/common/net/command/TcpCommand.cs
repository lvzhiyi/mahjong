using UnityEngine;

namespace cambrian.common
{
    public abstract class TcpCommand : Port
    {
        public TcpCommand()
        {
        }

        public TcpCommand(short id)
        {
            this.id = id;
        }

        public abstract TcpConnect getConnect();

        public virtual void bytesWrite(ByteBuffer data)
        {

        }

        public virtual void bytesRead(ByteBuffer data)
        {

        }
//        public void reConnect(Transform transform)
//        {
//            this.getConnect().reConnect();
//            CommandManager.addCommand(this);
//        }
    }
}