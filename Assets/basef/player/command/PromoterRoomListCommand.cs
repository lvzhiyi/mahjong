using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.player
{
    /// <summary>
    /// 获取未结算的房间
    /// </summary>
    public class PromoterRoomListCommand : UserCommand
    {
        public PromoterRoomListCommand()
        {
            this.id = CommandConst.PORT_ROOM_PROMOT_LIST;
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data;
        }

      
    }
}
