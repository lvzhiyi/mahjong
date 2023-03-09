using System;

namespace cambrian.common
{
    public class LuaRecvCommand:RecvPort
    {
        private Action<object> back; 
        public LuaRecvCommand(int cmd,Action<object> back)
        {
            this.id = (short)cmd;
            this.back = back;
        }


        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            back(data);
        }
    }
}
