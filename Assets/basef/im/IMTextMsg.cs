using cambrian.common;

namespace basef.im
{
    public class IMTextMsg
    {
        public long userid;

        string content;


        public string getContent()
        {
            return MaskWord.replace(content);
        }

        public void bytesRead(ByteBuffer data)
        {
            this.userid = data.readLong();
            this.content = data.readUTF();
        }
    }
}
