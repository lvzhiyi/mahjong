using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class GetArenaImprotListCommand : UserCommand
    {
        /// <summary> 获取导入茶馆 或竞赛场列表 </summary> 2525
        public GetArenaImprotListCommand()
        {
            id = CommandConst.PORT_ARENA_MEMBER_CREATE_LIST;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArrayList<ArenaImprotTeahouseData> list = new ArrayList<ArenaImprotTeahouseData>(len);
            ArenaImprotTeahouseData value = null;
            for (int i = 0; i < len; i++)
            {
                value = new ArenaImprotTeahouseData();
                value.bytesRead(data);
                if (value.id != Arena.arena.getId())
                {
                    list.add(value);
                }
            }
            callbackobj = list;
            value = null;
        }
    }
}
