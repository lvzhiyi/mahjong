using cambrian.common;
using scene.game;

namespace platform
{
    /// <summary>
    /// 接收刷新房间数据
    /// </summary>
    public class RecvRoomDataCommand: RecvPort
    {
        public RecvRoomDataCommand()
        {
            id = RecvConst.PORT_CLIENT_ROOM_REFRESH_MSG;
        }

        public override void bytesRead(ByteBuffer data)
        {
           LoadRoomData.reConnectRoom(data);
        }
    }
}
