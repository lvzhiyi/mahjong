using cambrian.common;
using platform.spotred.waitroom;

namespace platform.spotred
{
    /// <summary>
    /// 川牌-房间-接收玩家加入房间（后台广播）
    /// </summary>
    public class RecvCPAddRoomMsg : RecvMsgHandle
    {

        public RecvCPAddRoomMsg()
        {
            this.gamePlatform = GamePlatform.CP_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            int index = data.readInt();
            long score = data.readLong();

            if (Room.room != null)
                Room.room.getSpotRedCount().getIndexCount(index).score = score;

            MatchPlayer player = new MatchPlayer();
            player.bytesRead(data);

            Room.room.fulltime = data.readLong();

            if (Room.room != null)
            {
                Room.room.players[index] = player;
                SpotWaitRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotWaitRoomPanel>();
                panel.refresh();
                MatchPlayer[] players = Room.room.players;
                IpGPS3Panel ipgps = UnrealRoot.root.getDisplayObject<IpGPS3Panel>();
                ipgps.setMatchPlayers(Room.room.players);
                ipgps.refresh();
                ipgps.setCVisible(true);
            }
        }
    }
}
