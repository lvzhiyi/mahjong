using cambrian.common;

namespace basef.arena
{
    /// <summary> 导入成员 bar 数据 </summary>
    public class ArenaImprotMembersManagerContentData : BytesSerializable
    {
        /// <summary> 头像地址 </summary>
        public string headurl;

        /// <summary> 用户ID </summary>
        public long userid = 0;

        /// <summary> 用户名字 </summary>
        public string name = "";

        /// <summary> 是否是本赛场成员 </summary> false为不是
        public bool arenaMember = false;

        public override void bytesRead(ByteBuffer data)
        {
            this.headurl = data.readUTF();
            this.userid = data.readLong();
            this.name = data.readUTF();
            this.arenaMember = data.readBoolean();
        }
    }
}
