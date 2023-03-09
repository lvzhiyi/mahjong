
using cambrian.game;

namespace cambrian.common
{
    public class LuaCommand:UserCommand
    {
        ByteBuffer buffer;
        public LuaCommand(int command_id,ByteBuffer data)
        {
            id = (short)command_id;
            buffer = data;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.write(buffer);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data;
        }
    }
}
