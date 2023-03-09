using cambrian.common;

namespace platform
{
    public abstract class ObserverMsg
    {
        protected ArrayList<RecvMsgHandle> list=new ArrayList<RecvMsgHandle>();

        public void addMsg(RecvMsgHandle msg)
        {
            this.list.add(msg);
        }

        public void remove(RecvMsgHandle msg)
        {
            this.list.remove(msg);
        }

        public abstract void notify(int gamePlatform, ByteBuffer data);
    }
}
