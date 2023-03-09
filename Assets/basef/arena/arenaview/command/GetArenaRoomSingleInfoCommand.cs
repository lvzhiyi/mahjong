using cambrian.common;
using cambrian.game;
using platform.spotred;
using scene.game;

namespace basef.arena
{
    public class GetArenaRoomSingleInfoCommand:UserCommand
    {
        private int roomid;
        private long createtime;
        public GetArenaRoomSingleInfoCommand(int roomid,long createtime)
        {
            id = CommandConst.PORT_ARENA_ROOM_FKC_DETAIL;
            this.roomid = roomid;
            this.createtime = createtime;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeInt(roomid);
            data.writeLong(createtime);
        }

        public override void bytesRead(ByteBuffer data)
        {
            MatchPlayer[] players = new MatchPlayer[data.readInt()];
            for (int i = 0; i < players.Length; i++)
            {
                if (data.readBoolean())
                {
                    players[i] = new MatchPlayer();
                    players[i].bytesReadAndScore(data);
                }
            }
            this.callbackobj = players;
        }
    }
}
