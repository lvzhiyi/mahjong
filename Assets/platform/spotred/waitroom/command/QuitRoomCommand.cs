using cambrian.common;
using cambrian.game;
using scene.game;

namespace platform.spotred.waitroom
{
    /// <summary>
    ///川牌-房间-玩家离开房间
    /// </summary>
    public class QuitRoomCommand:UserCommand
    {
        int roomid;
        public QuitRoomCommand(int roomid)
        {
            this.id=CommandConst.COMMAND_ROOM_REMOVE;
            this.roomid = roomid;
        }


        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(roomid);
        }

    }
}
