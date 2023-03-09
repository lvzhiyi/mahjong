using cambrian.common;
using cambrian.game;
using scene.game;

namespace platform.spotred.room
{
    public class UsePropCommand : UserCommand
    {
        /// <summary>
        /// 使用目标
        /// </summary>
        int destIndex;
        /// <summary>
        /// 道具id
        /// </summary>
        int prop;

        public UsePropCommand(int destIndex, int prop)
        {
            this.id = CommandConst.PORT_ROOM_PROP_USE;
            this.destIndex = destIndex;
            this.prop = prop;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(destIndex);
            data.writeInt(prop);
        }
    }
}
