using basef.player;
using cambrian.common;
using Libs;
using platform;
using platform.spotred;

namespace platform
{
    public class BaseReplay : BytesSerializable
    {

        /// <summary>
        /// 房间号
        /// </summary>
        public int roomid;
        /// <summary>
        /// 房间
        /// </summary>
        protected Room room;
        /// <summary>
        /// 规则
        /// </summary>
        public RoomRule rule;
        /// <summary>
        /// 玩家
        /// </summary>
        protected SimplePlayer[] players;
        /// <summary>
        /// order步骤index
        /// </summary>
        public int index;
        /// <summary>
        /// 时间
        /// </summary>
        protected int time;

        public void setData(int roomid, int time, RoomRule rule)
        {
            this.roomid = roomid;
            this.time = time;
            this.rule = rule;
        }

        private void createRoom()
        {
            Room.instance();
            this.room = Room.room;
            this.room.setIndex(this.roomid);
            this.room.players = new MatchPlayer[this.rule.rule.playerCount];
            for (int i = 0; i < this.room.players.Length; i++)
            {
                this.room.players[i] = new MatchPlayer();
                this.room.players[i].player = this.players[i];
            }
            this.room.roomRule = this.rule;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = this.rule.rule.playerCount;
            this.players = new SimplePlayer[len];
            for (int i = 0; i < len; i++)
            {
                this.players[i] = new SimplePlayer();
                this.players[i].bytesRead(data);
            }
            this.createRoom();
            ByteBuffer undecompress = GZipKit.unDecompress(data.toArray());
            data.clear();
            data.write(undecompress);
        }

        /// <summary>
        /// 开始回放
        /// </summary>
        public virtual void start()
        {

        }

        public virtual void execute()
        {

        }

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <returns></returns>
        public Room getRoom()
        {
            return room;
        }

        public virtual void doOrder()
        {

        }

        //倒退1步
        public virtual void fallback()
        {

        }
    }
}
