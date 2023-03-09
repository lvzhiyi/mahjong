using cambrian.common;
using cambrian.game;
using scene.game;

namespace platform
{
    /// <summary>
    /// 普通房间再来一句
    /// </summary>
    public class AgainNormalRoomCommand:UserCommand
    {
        int cacheId;

        public AgainNormalRoomCommand(int cacheId)
        {
            id = CommandConst.PORT_ROOM_NORMAL_AGAIN;
            this.cacheId = cacheId;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(cacheId);
        }

        public override void bytesRead(ByteBuffer data)
        {
            Room.instance();
            Room.room.bytesRead(data);
            new ShowWaiteRoomCallBackProcess().execute();
        }
    }
}
