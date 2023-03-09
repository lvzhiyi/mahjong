using cambrian.common;

namespace basef.arena
{
    /// <summary> 竞赛场 导入茶馆 内容bar 数据 </summary>
    public class ArenaImprotTeahouseData : BytesSerializable
    {
        /// <summary> 场地类型: 竞赛场 </summary>
        public const int TYPE_ARENA = 0;
        /// <summary> 场地类型：茶馆 </summary>
        public const int TYPE_CLUB = 1;

        /// <summary> 头像 </summary>
        public string headurl;

        /// <summary> 名字 </summary>
        public string name;

        /// <summary> ID </summary>
        public long id;

        /// <summary> 类型 </summary> 茶馆0 或 竞赛场1
        public int type;

        public override void bytesRead(ByteBuffer data)
        {
            this.headurl = data.readUTF();
            this.name = data.readUTF();
            this.id = data.readLong();
            this.type = data.readInt();
        }
    }
}
