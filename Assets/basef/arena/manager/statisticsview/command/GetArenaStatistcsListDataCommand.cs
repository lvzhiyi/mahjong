using cambrian.common;
using cambrian.game;
using scene.game;
using UnityEngine;

namespace basef.arena
{
    /// <summary> 获取开桌统计 </summary>
    public class GetArenaStatistcsListDataCommand : UserCommand
    {
        long arenaid;

        public GetArenaStatistcsListDataCommand(long arenaid)
        {
            this.id = CommandConst.PORT_ARENA_ROOM_STATS;
            this.arenaid = arenaid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            ArrayList<ArenaStatistcsBarData> list = new ArrayList<ArenaStatistcsBarData>(3);
            ArenaStatistcsBarData value;
            for (int i = 0; i < 3; i++)
            {
                value = new ArenaStatistcsBarData();
                value.bytesRead(data);
                list.add(value);
            }
            callbackobj = list;
        }
    }
}
