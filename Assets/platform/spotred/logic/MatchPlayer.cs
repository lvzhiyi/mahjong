using cambrian.common;
using basef.player;

namespace platform.spotred
{
    /// <summary>
    /// 参赛玩家
    /// </summary>
    public class MatchPlayer
    {
        /// <summary>
        /// 准备,观战,托管,裁判
        /// </summary>
        public const int STATUS_READY=1, STATUS_WATCH = 2, STATUS_TRUSTEE=4, STATUS_REFEREE=8;

        public SimplePlayer player;

        public int status;
        /// <summary>
        /// ip
        /// </summary>
        public string ip;
        /// <summary>
        /// 经度
        /// </summary>
        public int gps_longitude;
        /// <summary>
        /// 维度
        /// </summary>
        public int gps_latitude;
        /// <summary>
        /// 离线时间
        /// </summary>
        public long offlinetime;
        /// <summary>
        /// 分数
        /// </summary>
        public long score;

        public long userid
        {
            get
            {
                return this.player.userid;
            }
        }

        public bool isSelf(long userid)
        {
            return this.player.userid == userid;
        }

        public bool isReady()
        {
            return StatusKit.hasStatus(status, STATUS_READY);
        }

        /// <summary>
        /// 是否托管
        /// </summary>
        /// <returns></returns>
        public bool isTrustee()
        {
            return StatusKit.hasStatus(status, STATUS_TRUSTEE);
        }

        /// <summary>
        /// 设置托管状态
        /// </summary>
        /// <param name="b"></param>
        public void setTrustee(bool b)
        {
            if (b)
                status |= STATUS_TRUSTEE;
            else
                status &= (~STATUS_TRUSTEE);
        }

        public void setReady(bool b)
        {
            if (b)
                status |= STATUS_READY;
            else
                status &= (~STATUS_READY);
        }

        public void bytesRead(ByteBuffer data)
        {
            this.player = new SimplePlayer();
            this.player.bytesRead(data);
            this.ip = this.player.host;
            this.status = data.readInt();
            this.gps_longitude = data.readInt();
            this.gps_latitude = data.readInt();
            this.offlinetime = data.readLong();
        }
        public void bytesRead(long userid, ByteBuffer data)
        {
            this.player = new SimplePlayer();
            this.player.byteRead(userid, data);
            this.ip = this.player.host;
            this.status = data.readInt();
            this.gps_longitude = data.readInt();
            this.gps_latitude = data.readInt();
            this.offlinetime = data.readLong();
         }

        /// <summary>
        /// 有积分信息的序列化
        /// </summary>
        /// <param name="data"></param>
        public void bytesReadAndScore(ByteBuffer data)
        {
            this.bytesRead(data);
            this.score = data.readLong();
        }

        public void bytesWrite(ByteBuffer data)
        {
            player.bytesWrite(data);
            data.writeInt(status);
            data.writeInt(gps_longitude);
            data.writeInt(gps_latitude);
            data.writeLong(offlinetime);
        }
    }
}
