using basef.player;
using cambrian.common;
using platform;

namespace basef.arena
{
    public class ArenaRoom: RoomEvent
    {
        /// <summary>
        /// 房间号
        /// </summary>
        public int roomid;
        /// <summary>
        /// 房间创建时间
        /// </summary>
        public long createtime;

        public ArenaLockRule rule;
        /// <summary>
        /// 当前玩家人数
        /// </summary>
        public int playercount;
        /// <summary>
        /// 桌子编号
        /// </summary>
        public int deskIndex;

        /// <summary>
        /// 玩家数据
        /// </summary>
        public SimplePlayer[] players;
        /// <summary>
        /// 加入限制（金豆不足，不能建房）
        /// </summary>
        public int limitjoin;
        /// <summary>
        /// 当前比赛局数
        /// </summary>
        public int tray;

        public bool isFull()
        {
            int count = 0;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                    count++;
            }
            return playercount == count;
        }

        public string getCurTray()
        {
            if (isStatus(Room.STATE_MATCH))
                return (tray + 1) + "/" + rule.rule.matchCount;
            return tray + "/" + rule.rule.matchCount;
        }

        /// <param name="data"></param>
        public override void bytesRead(ByteBuffer data)
        {
            this.deskIndex = data.readInt();
            this.limitjoin = data.readInt();
            this.tray = data.readInt();
            this.roomid = data.readInt();
            this.createtime = data.readLong();
            this.status = data.readInt();
            this.rule=new ArenaLockRule();
            rule.bytesRead(data);
            this.playercount = data.readInt();
            this.players = new SimplePlayer[rule.rule.playerCount];
            for (int i = 0; i < rule.rule.playerCount; i++)
            {
                if (data.readBoolean())
                {
                    this.players[i] = new SimplePlayer();
                    this.players[i].bytesRead(data);
                }
            }
        }
    }
}
