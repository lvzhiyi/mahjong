using cambrian.common;
using platform.spotred;

namespace platform.mahjong
{
    public class RecvMJTrusteeMsg: RecvMsgHandle
    {
        public RecvMJTrusteeMsg()
        {
            this.gamePlatform = GamePlatform.MJ_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            if (Room.room == null) return;
            int index = data.readInt();//哪个人
            bool trustee = data.readBoolean();//托管or取消托管

            MatchPlayer[] players = Room.room.getPlayers();
            players[index].setTrustee(trustee);

            MahJongPanel panel = MahJongPanel.getPanel();
            if (panel != null)
            {
                panel.setTrustee(index, trustee);
            }
        }
    }
}
