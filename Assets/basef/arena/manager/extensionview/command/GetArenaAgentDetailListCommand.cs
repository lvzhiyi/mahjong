using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 获取推广员列表(在推广管理，下级推广员需要的)
    /// </summary>
    public class GetArenaAgentDetailListCommand:UserCommand
    {
        public GetArenaAgentDetailListCommand()
        {
            id = CommandConst.PORT_ARENA_AGENT_DETAIL_LIST;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArenaNextExtension[] ex=new ArenaNextExtension[len];
            for (int i = 0; i < len; i++)
            {
                ex[i]=new ArenaNextExtension();
                ex[i].bytesRead(data);
            }

            long total = data.readLong();// 总积分
            long score = data.readLong();// 自己的积分

            object[] objs = { ex, total, score };
            callbackobj = objs;
            //callbackobj = ex;
        }

        
    }
}
