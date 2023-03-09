using cambrian.common;
using platform.spotred.waitroom;

namespace platform.spotred
{
    /// <summary>
    /// 川牌-房间-客户端接收-玩家准备(广播)
    /// </summary>
    public class RecvCPPlayerReadyMsg : RecvMsgHandle
    {
        public RecvCPPlayerReadyMsg()
        {
            this.gamePlatform = GamePlatform.CP_GAME;
        }

        public override void handle(ByteBuffer data)
        {

            int index = data.readInt();
            bool ready = data.readBoolean();

            if (Room.room != null)
            {
                MatchPlayer player = Room.room.getPlayers()[index];
                if (player == null) return;
                player.setReady(ready);

                SpotWaitRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotWaitRoomPanel>();
                panel.refresh();
            }
        }
    }
}
