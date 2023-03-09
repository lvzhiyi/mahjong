using cambrian.common;

namespace basef.arena
{
    public class ArenaMemberChangeRecord: BytesSerializable
    {
        /// <summary>
        /// 记录类型-加入
        /// </summary>
        public const int TYPE_JOIN = 101;
        /// <summary>
        /// 记录类型-踢出
        /// </summary>
        public const int TYPE_KICK = 102;

        public static string getTypeName(int type)
        {
            if (type == TYPE_JOIN)
                return "加入休闲场";
            else
                return "踢出休闲场";
        }

        /// <summary>
        /// 流水号
        /// </summary>
        public long uuid;
        /// <summary>
        /// 所属休闲场
        /// </summary>
        public long arena;
        /// <summary>
        /// 记录类型
        /// </summary>
        public int type;
        /// <summary>
        /// 目标
        /// </summary>
        public long dest;
        /// <summary>
        /// 创建时间
        /// </summary>
        public long time;
        /// <summary>
        /// 原由
        /// </summary>
        public long cause;
        /// <summary>
        /// 附加信息
        /// </summary>
        public string info;

        public string nickname;

        public string head;

        public override void bytesRead(ByteBuffer data)
        {
            this.uuid = data.readLong();
            this.arena = data.readLong();
            this.type = data.readInt();
            this.dest = data.readLong();
            this.time = data.readLong();
            this.cause = data.readLong();
            this.info = data.readUTF();
            this.nickname = data.readUTF();
            this.head = data.readUTF();

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
            this.head += "?" + MathKit.random(1, 10000);
        }
    }
}
