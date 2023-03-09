using cambrian.common;
using cambrian.game;
using scene.game;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 获取程序变动记录
    /// </summary>
    public class GetArenaMemeberChangeRecordCommand : UserCommand
    {
        private long maxuuid;

        /// <summary> 赛场变动 </summary>
        public GetArenaMemeberChangeRecordCommand(long uuid)
        {
            id = CommandConst.PORT_ARENA_MEMBER_CHANGE_DETAIL;
            this.maxuuid = uuid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeLong(maxuuid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArenaMemberChangeRecord[] records = new ArenaMemberChangeRecord[len];
            for (int i = 0; i < len; i++)
            {
                records[i] = new ArenaMemberChangeRecord();
                records[i].bytesRead(data);
            }

            callbackobj = records;
        }
    }
}
