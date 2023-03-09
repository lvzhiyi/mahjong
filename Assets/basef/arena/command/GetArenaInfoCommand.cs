using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 获取竞赛场的信息
    /// </summary>
    public class GetArenaInfoCommand:UserCommand
    {
        private long arenaid;

        public GetArenaInfoCommand(long arenaid)
        {
            id = CommandConst.PORT_ARENA_INFO;
            this.arenaid =arenaid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            if (data.readBoolean())//是否是该竞赛场成员
            {
                Arena arena = new Arena();
                arena.bytesRead(data);
                arena.getMember().bytesRead(data);
                Arena.arena = arena;
                Arena.arena.isNewMsg = data.readBoolean();
                callbackobj = Arena.arena;
            }
            else
            {
                callbackobj = null;
            }
        }
    }
}
