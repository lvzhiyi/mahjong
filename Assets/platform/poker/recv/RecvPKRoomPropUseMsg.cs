using cambrian.common;
using UnityEngine;

namespace platform.poker
{
    public class RecvPKRoomPropUseMsg : RecvMsgHandle
    {
        public RecvPKRoomPropUseMsg()
        {
            gamePlatform = GamePlatform.PK_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            long user = data.readLong();      //使用者
            int destIndex = data.readInt();   //使用目标在房间中的索引
            int prop = data.readInt();        //使用的道具的SID
            if (Room.room == null) return;
            var panel = PKRoomPanel.Panel;
            if (panel) panel.showPropAnimation(user, destIndex, prop);
        }
    }
}
