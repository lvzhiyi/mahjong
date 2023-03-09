using cambrian.common;

namespace platform.mahjong
{
    public class RecvMJRoomPropUseMsg : RecvMsgHandle
    {
        public RecvMJRoomPropUseMsg()
        {
            gamePlatform = GamePlatform.MJ_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            long user = data.readLong(); //使用者
            int destIndex = data.readInt(); //使用目标在房间中的索引
            int prop = data.readInt(); //使用的道具的SID
            if (Room.room == null) return;
            MahJongPanel panel = MahJongPanel.getPanel();
            if (panel != null)
                panel.showPropAnimation(user, destIndex, prop);
        }
    }
}
