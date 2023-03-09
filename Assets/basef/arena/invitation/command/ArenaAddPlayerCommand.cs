using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 添加玩家
    /// </summary>
    public class ArenaAddPlayerCommand : UserCommand
    {
        private long userid;

        private long arenaid;

        public ArenaAddPlayerCommand(long arenaid, long userid)
        {
            id = CommandConst.PORT_ARENA_MEMBER_ADD;
            this.arenaid = arenaid;
            this.userid = userid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeLong(userid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readBoolean();
        }
    }
}
