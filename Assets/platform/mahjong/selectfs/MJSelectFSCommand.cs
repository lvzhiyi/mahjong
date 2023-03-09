using cambrian.common;
using cambrian.game;
using scene.game;

namespace platform.mahjong
{
    /// <summary>
    /// 麻将-选择房数命令
    /// </summary>
    public class MJSelectFSCommand:UserCommand
    {
        /// <summary>
        /// 人数
        /// </summary>
        private int playerCount;
        /// <summary>
        /// 房数
        /// </summary>
        private int fangshu;

        int roomid;

        public MJSelectFSCommand(int playerCount,int fangshu,int roomid)
        {
            id = CommandConst.PORT_ROOM_SELECT_FS;
            this.playerCount = playerCount;
            this.fangshu = fangshu;
            this.roomid = roomid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(roomid);
            data.writeInt(playerCount);
            data.writeInt(fangshu);
        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
        }
    }
}
