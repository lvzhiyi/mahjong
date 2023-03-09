using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class GetArenaRoomListCommand:UserCommand
    {
        private long arenaid;

        public GetArenaRoomListCommand(long arenaid)
        {
            id = CommandConst.PORT_ARENA_ROOM_FKC_LIST;
            this.arenaid = arenaid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArrayList<ArenaRoom> list = new ArrayList<ArenaRoom>();

            for (int i = 0; i < len; i++)
            {
                ArenaRoom room = new ArenaRoom();
                room.bytesRead(data);
                list.add(room);
            }

            if (list.count > 0)
            {
                ArrayList<ArenaRoom> sortlist = new ArrayList<ArenaRoom>();

                ArenaRoom room=null;
                for (int i = 0; i < list.count; i++)
                {
                    room = list.get(i);
                    if (room.playercount != room.players.Length)
                        sortlist.add(room);
                }

                for (int i = 0; i < list.count; i++)
                {
                    room = list.get(i);
                    if (room.playercount == room.players.Length)
                        sortlist.add(room);
                }
                callbackobj = sortlist;
            }
            else
            {
                callbackobj = list;
            }
        }
    }
}
