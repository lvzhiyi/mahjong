using cambrian.common;
using cambrian.game;
using scene.game;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 获取程序变动记录
    /// </summary>
    public class GetArenaLogCommand : UserCommand
    {
        private long minuuid;

        /// <summary> 赛场变动 </summary>
        public GetArenaLogCommand(long minuuid)
        {
            id = CommandConst.PORT_ARENA_LOG_LIST;
            this.minuuid = minuuid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeLong(minuuid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArenaLog[] records = new ArenaLog[len];
            for (int i = 0; i < len; i++)
            {
                records[i] = new ArenaLog();
                records[i].bytesRead(data);
            }

            callbackobj = records;
        }
    }
}
