using cambrian.common;

namespace basef.arena
{
    public class ArenaMutexMember:BytesSerializable
    {
        public long userid;
        public string nickname;
        public string head;
        /// <summary>
        /// 是否是已经退出
        /// </summary>
        public bool isExist;

        public ArenaMutexMember()
        {
        }

        public ArenaMutexMember(long userid,string nickname,string head)
        {
            this.userid = userid;
            this.nickname = nickname;
            this.head = head;
        }


        public override void bytesRead(ByteBuffer data)
        {
            this.userid = data.readLong();
            this.nickname = data.readUTF();
            this.head = data.readUTF();
            isExist = data.readBoolean();

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

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(userid);
            data.writeUTF(nickname);
            data.writeUTF(head);
        }
    }
}
