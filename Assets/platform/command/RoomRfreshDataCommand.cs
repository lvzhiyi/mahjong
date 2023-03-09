using cambrian.common;
using scene.game;

namespace platform
{
    /// <summary>
    /// 刷新房间数据
    /// </summary>
    public class RoomRfreshDataCommand:SendCommand
    {
        public RoomRfreshDataCommand()
        {
            id = CommandConst.PORT_ROOM_REFRESH;
        }
    }
}
