using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 房间超时事件
    /// </summary>
    public class ArenaRoomEvent:Event
    {
        /** 事件类型：房间超时 */
       // public const int TYPE_ROOM_TIMEOUT = 101;
        /// <summary>
        /// 单据来源，归属（例如：用户ID,休闲场ID等）
        /// </summary>
        public int room;
        /// <summary>
        /// 房间创建时间
        /// </summary>
        public long createTime;
        /// <summary>
        /// 信息
        /// </summary>
        public string info;


        public override void bytesRead(ByteBuffer data)
        {
            this.uuid = data.readLong();
            this.type = data.readInt();
            this.status = data.readInt();
            this.arenaid = data.readLong();
            this.room = data.readInt();
            this.createTime = data.readLong();
            this.time = data.readLong();
            this.info = data.readUTF();
        }
    }
}
