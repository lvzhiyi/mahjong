using cambrian.common;
using platform.spotred;
using scene.game;

namespace platform
{
    /// <summary>
    /// 接收-房间快速开始后，规则，人数的变化
    /// </summary>
    public class RecvRoomADJustCommand:RecvPort
    {
        public RecvRoomADJustCommand()
        {
            id = RecvConst.COMMAND_CLIENT_ROOM_FAST_START;
        }

        public override void bytesRead(ByteBuffer data)
        {
            RoomRule rule=new RoomRule();
            rule.bytesRead(data);
            int len = data.readInt();
            MatchPlayer[] players=new MatchPlayer[len];
            for (int i = 0; i < len; i++)
            {
                players[i]=new MatchPlayer();
                players[i].bytesRead(data);
            }

            SpotRedCount[] mcounts=new SpotRedCount[len];

            for (int i = 0; i < len; i++)
            {
                mcounts[i]=new SpotRedCount();
                mcounts[i].bytesRead(data);
            }

            Room room= Room.room;
            if (room == null) return;
            room.players = players;
            room.roomRule = rule;
            room.addSpotRedCount(mcounts);
        }
    }
}
