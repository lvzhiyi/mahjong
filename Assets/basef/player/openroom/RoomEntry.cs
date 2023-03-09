using cambrian.common;

namespace basef.player
{
    /// <summary>
    /// 开房数据
    /// </summary>
    public class RoomEntry
    {
        /** 代开放状态：初始，已开始，已结算，已解散, 已完结，已超时，已解散 */
        public const int STATUS_INIT = 0, STATUS_STARTED = 1, STATUS_OVER = 2, STATUS_DISBAND = 3, STATUS_FINISH=4, STATUS_TIMEOUT=5, STATUS_WAIT=6;

        public int roomid;
        public long createtime;

        public int matchCount;
        public int status;
        public BriefPlayer player;


        public string getStatusName()
        {
            switch (status)
            {
                case STATUS_INIT:
                case STATUS_WAIT:
                    return "未开始";
                case STATUS_STARTED:
                    return "已开始";
                case STATUS_DISBAND:
                    return "已解散";
                case STATUS_OVER:
                case STATUS_FINISH:
                    return "已结算";
                case STATUS_TIMEOUT:
                    return "已超时";
            }

            return "";
        }

        public bool isStatus(int status)
        {
            return this.status == status;
        }

        public void bytesRead(ByteBuffer data)
        {
            this.roomid = data.readInt();
            this.createtime = data.readLong();
            this.matchCount = data.readInt();
            this.status = data.readInt();
            bool b = data.readBoolean();
            if (b)
            {
                player=new BriefPlayer();
                player.bytesRead(data);
            }
        }

        public override string ToString()
        {
            return " RoomEntry,roomid:"+roomid+",matchCount:"+matchCount;
        }
    }
}
