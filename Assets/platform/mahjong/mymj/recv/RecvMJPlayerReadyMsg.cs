using cambrian.common;
using cambrian.game;
using platform.spotred;

namespace platform.mahjong
{
    /// <summary>
    /// 川牌-房间-客户端接收-玩家准备(广播)
    /// </summary>
    public class RecvMJPlayerReadyMsg : RecvMsgHandle
    {
        public RecvMJPlayerReadyMsg()
        {
            this.gamePlatform = GamePlatform.MJ_GAME;
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
                MahJongPanel panel = MahJongPanel.getPanel();
                panel.setPlayerReady(index);
                SoundManager.playMJEffect("ready");
            }
        }
    }
}
