using cambrian.common;

namespace basef.arena
{
    public class ArenaMsgJoinContentData : BytesSerializable
    {
        /// <summary> 用户头像 </summary>
        public string headurl;

        /// <summary> 用户名字 </summary>
        public string username;

        /// <summary> 用户ID </summary>
        public long usersid;

        /// <summary> 类型 </summary>
        public int type;

        /// <summary> 操作人名字 </summary>
        public string operatename;
    }
}
