using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class GetArenaMsgApplyContentListCommand : UserCommand
    {
        /// <summary> 查询类型 </summary>
        int type;

        /// <summary> 赛场审核 </summary>
        public GetArenaMsgApplyContentListCommand(int type)
        {
            this.id = CommandConst.PORT_ARENA_GET_EVENTLIST;
            this.type = type;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            //竞赛场ID   
            data.writeLong(Arena.arena.getId());
            //查询类型   
            data.writeInt(type);
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArrayList<ArenaEvent> list = new ArrayList<ArenaEvent>(len);

            ArenaEvent value = null;
            for (int i = 0; i < len; i++)
            {
                value = new ArenaEvent();
                value.bytesRead(data);
                list.add(value);
            }
            callbackobj = list;
        }
    }
}
