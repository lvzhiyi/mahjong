using cambrian.common;
using platform.spotred.waitroom;

namespace platform.spotred.room
{
    public class RecvCPRoomPropUseMsg : RecvMsgHandle
    {
        public RecvCPRoomPropUseMsg()
        {
            gamePlatform = GamePlatform.CP_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            long user = data.readLong(); //使用者
            int destIndex = data.readInt(); //使用目标在房间中的索引
            int prop = data.readInt(); //使用的道具的SID
            if (Room.room == null) return;
            SpotRoomPanel panel = UnrealRoot.root.checkDisplayObject<SpotRoomPanel>();
            if (panel != null)
                panel.showPropAnimation(user, destIndex, prop);
            else
            {
                SpotWaitRoomPanel waitRoom = UnrealRoot.root.checkDisplayObject<SpotWaitRoomPanel>();
                waitRoom.showPropAnimation(user, destIndex, prop);
            }
        }
    }
}
