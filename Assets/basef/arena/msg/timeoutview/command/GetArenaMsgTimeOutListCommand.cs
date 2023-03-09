using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary> 获取房间超时消息 </summary>
    public class GetArenaMsgTimeOutListCommand : UserCommand
    {
        int type;

        public GetArenaMsgTimeOutListCommand(int type)
        {
            this.id = CommandConst.PORT_ARENA_GET_EVENTLIST;
            this.type = type;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeInt(type);
        }


        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArrayList<ArenaRoomEvent> list = new ArrayList<ArenaRoomEvent>(len);
            ArenaRoomEvent value=null;
            for (int i = 0; i < len; i++)
            {
                value = new ArenaRoomEvent();
                value.bytesRead(data);
                list.add(value);
            }
            callbackobj = list;
        }
    }
}
