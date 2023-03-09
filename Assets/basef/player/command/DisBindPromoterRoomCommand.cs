using cambrian.common;
using cambrian.game;
using scene.game;
using basef.player;

namespace basef.player
{
    public class DisBindPromoterRoomCommand:UserCommand
    {
        private RoomEntry roomEntry;
        public DisBindPromoterRoomCommand(RoomEntry roomEntry)
        {
            this.id = CommandConst.PORT_ROOM_PROMOT_DISBIND_ROOM;
            this.roomEntry = roomEntry;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(roomEntry.roomid);
            data.writeLong(roomEntry.createtime);
        }

        public override void bytesRead(ByteBuffer data)
        {
            bool b= data.readBoolean();
            callbackobj = b;
        }
    }
}
