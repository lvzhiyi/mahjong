using basef.player;
using basef.rule;
using cambrian.common;
using platform.spotred;

namespace platform
{
    /// <summary>
    /// 比赛房间
    /// </summary>
    public class Room : BytesSerializable
    {
        ///房间类型：普通房，玩家自己创建的房间
        public const int NORMAL = 0;
        /// <summary>
        /// 房间类型：茶馆房 ，在茶馆中创建的房间
        /// </summary>
        public const int CLUB = 1 << 0;
        /// <summary>
        /// 房间类型：竞赛房，在竞赛场中创建的房间 
        /// </summary>
        public const int ARENA = 1 << 1;
        /// <summary>
        /// 房间类型：代开房，代理预先创建的房间
        /// </summary>
        public const int PROMOT = 1 << 2;
        /// <summary>
        /// 房间类型：对战房
        /// </summary>
        public const int DUEL = 1 << 3;
        /// <summary>
        /// 房间类型：3金币场房间，金币场中创建的房间
        /// </summary>
        public const int JBC = 1 << 5;
        /// <summary>
        /// 房间类型：练习场房间
        /// </summary>
        public const int JB_LXC = 1 << 6;
        /// <summary>
        /// 房间类型：初级场房间
        /// </summary>
        public const int JB_CJC = 1 << 7;
        /// <summary>
        /// 房间类型：中级场房间
        /// </summary>
        public const int JB_ZJC = 1 << 8;
        /// <summary>
        /// 房间类型：高级场房间
        /// </summary>
        public const int JB_GJC = 1 << 9;
        /// <summary>
        /// 房间类型：大师场房间
        /// </summary>
        public const int JB_DSC = 1 << 10;

        /// <summary>
        /// 后台比赛状态值(初始,准备，开始，解散，摧毁)
        /// </summary>
        public static int STATUS_INIT = 0, STATUS_READY = 1, STATE_MATCH = 2, STATE_DISBANDING = 4, STATE_DESTORY = 8;

        public static Room room { set; get; }

        /// <summary>
        /// 临时比赛数组：用于麻将总结算
        /// </summary>
        public static ArrayList<Match> tempMatchs = new ArrayList<Match>();

        public static void instance()
        {
            room = new Room();
            tempMatchs.clear();
        }

        public static void clear()
        {
            room = null;
            tempMatchs.clear();
        }

        /// <summary>
        /// 房间索引
        /// </summary>
        private int roomIndex;
        /// <summary>
        /// 比赛玩家
        /// </summary>
        public MatchPlayer[] players;
        /// <summary>
        /// 规则
        /// </summary>
        public RoomRule roomRule;
        /// <summary>
        /// 房间类型
        /// </summary>
        public int roomType;

        /// <summary>
        /// 比赛是否已经开始
        /// </summary>
        private int status;

        /// <summary>
        /// 玩家总分数
        /// </summary>
        private RoomPlayerResult playerTotalScores;
        /// <summary>
        /// 总结算
        /// </summary>
        private RoomResult roomResult;

        /// <summary>
        /// 每局分数
        /// </summary>
        public long[][] scores;

        /// <summary>
        /// 房间绑定源id，（房间绑定在其他对象上，比如休闲场，场区等等）
        /// </summary>
        private long bind;
        /// <summary>
        /// 房主id
        /// </summary>
        public long masterid { get;  set; }
        /// <summary>
        /// 房间创建时间
        /// </summary>
        private long createRoomTime;

        /// <summary>
        /// 防暂离时间
        /// </summary>
        public long fulltime { get; set; }

        /// <summary>
        /// 房间解散时,后端发一个id，过来，用于再来一局
        /// </summary>
        public int cacheId { get; set; }
        /// <summary>
        /// 获取设置界面的解散房间次数的值
        /// </summary>
        public int canDisbandCount;

        public Room()
        {
            roomResult = new RoomResult();
        }

        public Rule getRule()
        {
            return roomRule.rule;
        }

        /// <summary>
        /// 获取房间创建时间
        /// </summary>
        /// <returns></returns>
        public long getCreateRoomTime()
        {
            return this.createRoomTime;
        }

        public int getRoomRealPlayerCount()
        {
            int count = 0;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] == null) continue;
                count++;
            }

            return count;
        }

        /// <summary>
        /// 是否是指定类型的房间
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool isType(int type)
        {
            //isType(CLUB | JBC);
            if (type == 0) return this.roomType == 0;
            return (this.roomType & type) == type;
        }

        public long getBind()
        {
            return bind;
        }

        /// <summary>
        /// 获取比赛玩家数量
        /// </summary>
        /// <returns></returns>
        public int getPlayerCount()
        {
            return this.roomRule.rule.playerCount;
        }

        public void addSpotRedCount(SpotRedCount[] counts)
        {
            if (playerTotalScores == null)
                playerTotalScores = new RoomPlayerResult();
            playerTotalScores.addSpotRedCounts(counts);
        }

        public RoomPlayerResult getSpotRedCount()
        {
            return playerTotalScores;
        }

        /// <summary>
        /// 设置房间结果
        /// </summary>
        public void setRoomResult(TotalScore[] result, long ticket)
        {
            roomResult.addSpotRedCounts(result, ticket);
        }

        /// <summary>
        /// 获取房间结果
        /// </summary>
        /// <returns></returns>
        public RoomResult getRoomResult()
        {
            return roomResult;
        }

        /// <summary>
        /// 设置房间结果
        /// </summary>
        /// <param name="result"></param>
        public void setRoomResult1(RoomResult result)
        {
            roomResult = result;
        }

        public void cancelReady()
        {
            for (int i = 0; i < this.players.Length; i++)
            {
                this.players[i].setReady(false);
            }
        }

        public void setRule(RoomRule rule)
        {
            this.roomRule = rule;
        }

        /// <summary>
        /// 这个方法只在回放中 前台自己生成房间时用
        /// </summary>
        /// <param name="index"></param>
        public void setIndex(int index)
        {
            this.roomIndex = index;
        }

        public int getRoomIndex()
        {
            return this.roomIndex;
        }

        public void setStatus(int status, bool b)
        {
            if (b)
                this.status |= status;
            else
                this.status &= ~status;
        }

        public bool isStatus(int status)
        {
            return (this.status & status) == status;
        }

        /// <summary>
        /// 自己在房间中的位置
        /// </summary>
        /// <returns></returns>
        public int indexOf()
        {
            for (int i = 0; i < this.players.Length; i++)
            {
                if (this.players[i] == null) continue;
                if (this.players[i].isSelf(Player.player.userid))
                {
                    return i;
                }
            }
            return 0;
        }

        public int getMasterIndex()
        {
            for (int i = 0; i < this.players.Length; i++)
            {
                if (this.players[i] == null) continue;
                if (this.players[i].isSelf(masterid))
                {
                    return i;
                }
            }

            return -1;
        }

        public MatchPlayer getSelfMatchPlayer()
        {
            for (int i = 0; i < this.players.Length; i++)
            {
                if (this.players[i] == null) continue;
                if (this.players[i].isSelf(Player.player.userid))
                {
                    return this.players[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 获取玩家的位置
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int getPlayerIndex(long userid)
        {
            for (int i = 0; i < this.players.Length; i++)
            {
                if (this.players[i] == null) continue;
                if (this.players[i].userid == userid)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 获取总计信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public SpotRedCount getSpotRedCount(long userid)
        {
            int index = getPlayerIndex(userid);
            if (playerTotalScores == null) return new SpotRedCount();
            return playerTotalScores.getIndexCount(index);
        }

        public MatchPlayer[] getPlayers()
        {
            return this.players;
        }
        /// <summary>
        /// 移除玩家
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public MatchPlayer removePlayer(long userid)
        {
            MatchPlayer player = null;

            for (int i = 0; i < this.players.Length; i++)
            {
                if (this.players[i] == null) continue;
                if (this.players[i].isSelf(userid))
                {
                    player = this.players[i];
                    this.players[i] = null;
                    break;
                }
            }

            return player;
        }

        /// <summary>
        /// 是否是房主
        /// </summary>
        /// <returns></returns>
        public bool isMaster()
        {
            return masterid == Player.player.userid;
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.roomIndex = data.readInt();
            this.roomType = data.readInt();
            this.masterid = data.readLong();
            this.bind = data.readLong();
            if (this.roomRule == null)
                this.roomRule = new RoomRule();
            this.roomRule.bytesRead(data);
            this.players = new MatchPlayer[this.roomRule.rule.playerCount];
            for (int i = 0; i < this.players.Length; i++)
            {
                long userid = data.readLong();
                if (userid == 0) continue;
                this.players[i] = new MatchPlayer();
                this.players[i].bytesRead(userid, data);
            }

            //当前比赛局数
            int pnum = data.readInt();
            this.roomRule.setGameNum(pnum);
            this.status = data.readInt();
            this.createRoomTime = data.readLong();

            int n = data.readInt();
            SpotRedCount[] mcounts = new SpotRedCount[n];
            for (int i = 0; i < mcounts.Length; i++)
            {
                mcounts[i] = new SpotRedCount();
                mcounts[i].bytesRead(data);
            }

            this.addSpotRedCount(mcounts);
            fulltime = data.readLong();
            canDisbandCount = data.readInt();
        }

        public void byteCurWrite(ByteBuffer data)
        {
            data.writeInt(this.roomIndex);
            data.writeInt(this.roomType);
            data.writeLong(this.masterid);
            data.writeLong(this.bind);
            this.roomRule.bytesWrite(data);
            for (int i = 0; i < this.players.Length; i++)
            {
                players[i].bytesWrite(data);
            }
            data.writeInt(this.roomRule.getGameNum());
            data.writeInt(this.status);
            data.writeLong(this.createRoomTime);

            playerTotalScores.bytesWrite(data);
            roomResult.bytesWrite(data);
            data.writeLong(roomResult.getTicket());
            data.writeInt(cacheId);
        }


        public void byteCurRead(ByteBuffer data)
        {
            this.roomIndex = data.readInt();
            this.roomType = data.readInt();
            this.masterid = data.readLong();
            this.bind = data.readLong();
            if (this.roomRule == null)
                this.roomRule = new RoomRule();
            int sid = data.readInt();
            this.roomRule.rule = RuleManager.manager.newSample(sid);
            this.roomRule.rule.bytesReadLocal(data);
            this.players = new MatchPlayer[this.roomRule.rule.playerCount];
            for (int i = 0; i < this.players.Length; i++)
            {
                this.players[i] = new MatchPlayer();
                this.players[i].bytesRead(data);
            }

            //当前比赛局数
            int pnum = data.readInt();
            this.roomRule.setGameNum(pnum);
            this.status = data.readInt();
            this.createRoomTime = data.readLong();

            this.playerTotalScores = new RoomPlayerResult();
            this.playerTotalScores.bytesRead(data);
            this.roomResult.bytesRead(data);
            this.roomResult.setTicket(data.readLong());
            cacheId = data.readInt();
        }


        public Room clone()
        {
            Room r = new Room();
            ByteBuffer data = new ByteBuffer();
            this.byteCurWrite(data);
            r.byteCurRead(data);
            r.scores = this.scores;
            return r;
        }
    }
}
