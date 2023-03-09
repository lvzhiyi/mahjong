using cambrian.common;

namespace basef.arena
{
    public class PersonalJournalEventRecord : BytesSerializable
    {

        /** 创建房间 */
        public const int TYPE_CREATE_ROOM = 1;
        /** 加入房间 */
        public const int TYPE_JOIN_ROOM = 2;
        /** 退出房间 */
        public const int TYPE_EXIT_ROOM = 3;

        public static string getType(int type)
        {
            if (type == TYPE_CREATE_ROOM)
                return "创建房间";
            else if (type == TYPE_JOIN_ROOM)
                return "加入房间";
            else
                return "退出房间";
        }

        public long uuid;
        public long arena;
        public long userid;
        public int type;
        public long cause;
        public long time;
        public string info;

        public override void bytesRead(ByteBuffer data)
        {
            this.uuid = data.readLong();
            this.arena = data.readLong();
            this.userid = data.readLong();
            this.type = data.readInt();
            this.cause = data.readLong();
            this.time = data.readLong();
            this.info = data.readUTF();

        }
    }
}