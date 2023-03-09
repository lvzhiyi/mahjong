using cambrian.common;
namespace basef.arena
{
    public class AuxiliaryScoreEventRecord : BytesSerializable
    {
        public long uuid;
        public long arena;
        public int type;
        public long dest;
        public string name;
        public long time;
        public string info;
        public string head;
        public long fufen;
        public long warning;
        public string cause;
        
        public override void bytesRead(ByteBuffer data)
        {
            this.uuid = data.readLong();
            this.arena = data.readLong();
            this.type = data.readInt();
            this.dest = data.readLong();//目标id
            this.name = data.readUTF();//目标昵称
            this.head = data.readUTF();
            this.cause = data.readUTF();//上级昵称
            this.fufen = data.readLong();
            this.warning = data.readLong();
            this.time = data.readLong();
            this.info = data.readUTF();

            if (this.head == null || this.head.Length < 2) return;
            if (this.head[this.head.Length - 1] == '0')
            {
                if (this.head[this.head.Length - 2] == '/')
                {
                    CharBuffer buf = new CharBuffer();
                    buf.append(this.head, 0, this.head.Length - 2);
                    buf.append("/64");
                    this.head = buf.getString();
                }
            }
        }
    }
}