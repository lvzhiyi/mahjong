using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class GetArenaListCommand:UserCommand
    {
        public GetArenaListCommand()
        {
            id = CommandConst.PORT_ARENA_LIST;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArrayList<Arena> list=new ArrayList<Arena>(len);
            Arena arena=null;
            for (int i = 0; i < len; i++)
            {
                arena=new Arena();
                arena.bytesRead(data);
                list.add(arena);
            }

            callbackobj = list;
        }
    }
}
