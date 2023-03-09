using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 获取竞赛场玩家列表
    /// </summary>
    public class GetArenaMembersListCommand : UserCommand
    {
        /// <summary> 类型：直属节点</summary>
        public const int SELECT_NODES = 101;
        /// <summary> 类型：全树节点</summary>
        public const int SELECT_TREE_NODES = 102;
        /// <summary>类型：直属成员</summary>
        public const int SELECT_MEMBERS = 103;
        /// <summary> 类型： 全树代理</summary>
        public const int SELECT_TREE_AGENTS = 106;
        /// <summary>类型：直属代理</summary>
        public const int SELECT_AGENTS = 105;
        //------------server需要的值-----------

        /** 成员：全树附带一个true */
        public const int SELECT_SERVER_MEMBERS = 101;
        /** 代理：全树附带一个true */
        public const int SELECT_SERVER_AGENTS = 102;
        /** 成员和代理：全树附带一个true */
        public const int SELECT_SERVER_NODES = 103;


        /// <summary>
        /// 查询成员的类型
        /// </summary>
        private int type;
        /// <summary>
        /// true:代表全部，false:直属
        /// </summary>
        private bool b;

        public GetArenaMembersListCommand(int type,bool b)
        {
            id = CommandConst.PORT_ARENA_MEMBER_LIST;
            this.type = type;
            this.b = b;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeInt(type);
            data.writeBoolean(b);
            data.writeInt(0);
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArenaMember[] members = new ArenaMember[len];
            ArenaMember member = null;

            for (int i = 0; i < len; i++)
            {
                member = new ArenaMember();
                member.bytesRead(data);
                members[i] = member;
            }

            callbackobj = members;
        }
    }
}
