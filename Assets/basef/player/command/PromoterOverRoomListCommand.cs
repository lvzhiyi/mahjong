using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.player
{
    /// <summary>
    /// 获取已结算的房间
    /// </summary>
    public class PromoterOverRoomListCommand : UserCommand
    {
        private int index;
        public PromoterOverRoomListCommand(int index)
        {
            this.id = CommandConst.PORT_ROOM_PROMOT_OVER_LIST;
            this.index = index;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(index);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data;
        }

      
    }
}
