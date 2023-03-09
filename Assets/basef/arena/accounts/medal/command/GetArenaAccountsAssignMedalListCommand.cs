using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary> 获得指定成员奖章数量 </summary>
    public class GetArenaAccountsAssignMedalListCommand : UserCommand
    {
        long userid;

        public GetArenaAccountsAssignMedalListCommand(long userid)
        {
            id = CommandConst.PORT_ARENA_MEMBER_MEDAL;
            this.userid = userid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeLong(userid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = (int)data.readLong();
        }
    }
}
