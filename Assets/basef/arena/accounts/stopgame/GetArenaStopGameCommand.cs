using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class GetArenaStopGameCommand : UserCommand
    {
      
        long playerid;

        public GetArenaStopGameCommand(long playerid)
        {
            this.id = CommandConst.PORT_ARENA_STOP_GAME;
           
            this.playerid = playerid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeLong(playerid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            bool b = data.readBoolean();
            callbackobj = b;
        }
    }
}
