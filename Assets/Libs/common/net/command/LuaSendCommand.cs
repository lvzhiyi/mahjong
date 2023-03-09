namespace cambrian.common
{
    /// <summary>
    /// lua 发送消息（无返回类型）
    /// </summary>
    public class LuaSendCommand:SendCommand
    {
        ByteBuffer buffer;

        public LuaSendCommand(int cmd,ByteBuffer data)
        {
            id = (short)cmd;
            buffer = data;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.write(buffer);
        }
    }
}
