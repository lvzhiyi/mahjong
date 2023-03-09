using cambrian.common;

namespace basef.arena
{
    public class ArenaMember : BytesSerializable
    {
        /// <summary>
        /// 成员类型-顶点
        /// </summary>
        public const int LEVEL_TOP = 0;

        /** 状态：用户在线状态 */
        public const int STATUS_ONLINE = 1 << 0;

        /** 状态：房卡场游戏中 */
        public const int STATUS_FKC_GAMING = 1 << 1;
        /** 状态：金币场游戏中 */
        public const int STATUS_JBC_GAMING = 1 << 2;

        /** 状态：休闲场房卡场限制使用 */
        public const int STATUS_FKC_CLUB_LIMIT = 1 << 3;
        /** 状态：休闲场金币场限制使用 */
        public const int STATUS_JBC_CLUB_LIMIT = 1 << 4;

        /** 状态: 竞赛场被拉入黑名单 */
        public const int STATUS_ARENA_LIMIT = 1 << 5;

        /** 是否是代理 */
        public const int STATUS_AGENT = 1 << 16;

        /** 已移除 */
        public const int STATUS_KICK = 1 << 17;

        /** 已冻结 */
        public const int STATUS_FREEZE = 1 << 18;

        /** 管理员：副擂主或者副推广员 */
        public const int STATUS_ADMIN = 1 << 19;

        public string getLeveName()
        {
            if (isAgent())
            {
                if (isMaster()) return "吧主";
                return level + "级合伙人";
            }
            else
            {
                if (hasStatus(STATUS_ADMIN)) return "助理";
                return "普通成员";
            }
        }
        /// <summary>
        /// 新增限制组中使用
        /// </summary>
        public bool isSelect;


        /// <summary>
        /// 用户ID
        /// </summary>
        public long userid;
        /// <summary> 用户昵称 </summary>
        public string name;
        /// <summary> 用户头像 </summary>
        public string head;
        /// <summary> 用户性别 </summary>
        public int sex;
        /// <summary> 上级ID </summary>
        public long master;
        /// <summary> 上级名称 </summary>
        public string masterName;
        /// <summary> 竞技场金币 </summary>
        long arenagold;
        /// <summary> 互斥 </summary>
        public long mutex;
        /// <summary> 备注 </summary>
        public string info;
        /// <summary>
        /// 是否在线，房卡场，金豆场这些
        /// </summary>
        public int status;

        // ----以下是推广员属性-----
        /// <summary> 等级 </summary>
        public int level;
        /// <summary> 推广任务 </summary>
        long task;
        /// <summary>
        /// 同桌限制控制 0关闭 1普通玩家 2普通玩家和代理 3整个树
        /// </summary>
        public int mutexStatus;

        /// <summary>
        /// 低于辅分限制
        /// </summary>
        public bool lowerFufeng;
        /// <summary>
        /// 辅分预警
        /// </summary>
        public bool isWarning;
        /// <summary>
        /// 总局数
        /// </summary>
        public int totaljushu;
        /// <summary>
        /// 权限值
        /// </summary>
        public int perms;
        /// <summary>
        /// 活动时间
        /// </summary>
        public long activetime;

        public double getArenaGold()
        {
            return MathKit.getRound2Long(arenagold);
        }

        public void setArenaGold(long gold)
        {
            this.arenagold = gold;
        }

        public bool hasStatus(int status)
        {
            return StatusKit.hasStatus(this.status, status);
        }

        public void setStatus(int status,bool b)
        {
            this.status = StatusKit.setStatus(this.status, status, b);
        }

        public int getStatus()
        {
            return status;
        }

        /// <summary>
        /// 是否有指定权限
        /// </summary>
        /// <param name="perms"></param>
        /// <returns></returns>
        public bool hasPerms(int perms)
        {
            if (!hasStatus(STATUS_ADMIN)) return false;
            return StatusKit.hasStatus(this.perms, perms);
        }

        /// <summary>
        /// 是否是推广员
        /// </summary>
        /// <returns></returns>
        public bool isAgent()
        {
            return StatusKit.hasStatus(status, STATUS_AGENT);
        }
        /** 是否是擂主 */
        public bool isMaster()
        {
            return isAgent() && level == LEVEL_TOP;
        }


        /// <summary>
        /// 获取推广任务
        /// </summary>
        /// <returns></returns>
        public double getTask()
        {
            return MathKit.getRound2Long(task);
        }

        public void setTask(long task)
        {
            this.task = task;
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.userid = data.readLong();
            this.name = data.readUTF();
            this.head = data.readUTF();
            this.sex = data.readInt();
            this.status = data.readInt();
            this.level = data.readInt();
            this.arenagold = data.readLong();
            this.mutex = data.readLong();
            this.master = data.readLong();
            this.task = data.readLong();
            this.mutexStatus = data.readInt();
            this.lowerFufeng = data.readBoolean();
            this.isWarning = data.readBoolean();
            this.info = data.readUTF();
            this.totaljushu = data.readInt();
            this.masterName = data.readUTF();
            this.perms = data.readInt();
            this.activetime = data.readLong();

            if (this.head == null || this.head.Length < 2) return;
            if (this.head[this.head.Length - 1] == '0')
            {
                if (this.head[this.head.Length - 2] == '/')
                {
                    CharBuffer buf = new CharBuffer();
                    buf.append(this.head, 0, this.head.Length - 2);
                    buf.append("/64");
                    this.head = buf.getString();
                }
            }
        }
    }
}
