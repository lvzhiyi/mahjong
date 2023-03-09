using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class ArenaImprotMemberCommand : UserCommand
    {
        int type;  //类型

        long atid; //茶馆ID 竞赛场

        /// <summary> 导入自己创建的茶馆或竞赛场成员 </summary> 2527
        public ArenaImprotMemberCommand(int type,long atid)
        {
            this.type = type;
            this.atid = atid;
            this.id = CommandConst.PORT_ARENA_MEMBER_LIST_LEAD;
        }

        public int[] count = new int[2];

        public override void bytesRead(ByteBuffer data)
        {
            count[0] = data.readInt();
            count[1] = data.readInt();
            callbackobj = count;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeInt(type);
            data.writeLong(atid);
        }
    }
}
