using cambrian.common;

namespace platform.poker
{
    public class RecvPKTrusteeMsg : RecvMsgHandle
    {
        public RecvPKTrusteeMsg()
        {
            gamePlatform = GamePlatform.PK_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            if (Room.room == null) return;
            int index = data.readInt();
            bool trustee = data.readBoolean();
            Room.room.getPlayers()[index].setTrustee(trustee);
            PKRoomPanel.Panel.setTrustee(index, trustee);
        }
    }
}
