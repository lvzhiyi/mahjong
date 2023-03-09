using cambrian.common;
using cambrian.game;
using scene.game;

/// <summary>
/// 获取赛场可换桌列表
/// </summary>
namespace basef.arena
{
    public class GetArenaCanChangeRoomListCommand : UserCommand
    {
        public GetArenaCanChangeRoomListCommand()
        {
            id = CommandConst.PORT_ARENA_ROOM_INVITE_PLAYERS_ROOM_LIST;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArenaRoom[] rooms = new ArenaRoom[len];

            ArenaRoom room = null;
            for (int i = 0; i < len; i++)
            {
                room = new ArenaRoom();
                room.bytesRead(data);
                rooms[i] = room;
            }

            callbackobj = rooms;
        }
    }
}
