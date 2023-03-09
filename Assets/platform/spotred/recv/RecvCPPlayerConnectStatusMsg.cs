using basef.player;
using cambrian.common;
using platform.spotred.room;
using platform.spotred.waitroom;

namespace platform.spotred
{
    public class RecvCPPlayerConnectStatusMsg : RecvMsgHandle
    {
        public RecvCPPlayerConnectStatusMsg()
        {
            gamePlatform = GamePlatform.CP_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            int index = data.readInt();
            bool online = data.readBoolean();//true,上线
            long time = data.readLong();
            if (Room.room != null)
            {
                if (Room.room.players[index] != null)
                    Room.room.players[index].player.setStatus(SimplePlayer.STATUS_ONLINE, online);
                Room.room.players[index].offlinetime = time;

                SpotWaitRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotWaitRoomPanel>();

                if (UnrealRoot.root.checkDisplayObject<SpotWaitRoomPanel>() != null)
                {
                    panel.setMathPlayerStatus(index, online);
                }
                else
                {
                    SpotRoomPanel roomPanel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();

                    roomPanel.headView.ShowAllMatchPlayer(Room.room, CPMatch.match.dangjia,
                        CPMatch.match.xiaojia);
                }
            }
        }
    }
}
