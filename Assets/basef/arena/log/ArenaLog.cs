using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 赛场日志
    /// </summary>
    public class ArenaLog
    {

        // =================成员变动====================
        /** 日志类型：成员加入 */
        public const int LOG_JOIN = 1;
        /** 日志类型：成员踢出 */
        public const int LOG_KICK = 2;
        /** 日志类型：成员退出 */
        public const int LOG_EXIT = 3;
        // =================积分变动====================
        /** 日志类型：合伙人积分变动 */
        public const int LOG_AGENT_SCORE_CHANGE = 4;
        /** 日志类型：成员积分变动 */
        public const int LOG_MEMBER_SCORE_CHANGE = 5;
        // =================成员操作====================
        /** 日志类型：加入黑名单 */
        public const int LOG_MEMBER_PERMIT = 6;
        // =================推广员操作====================
        /** 日志类型：冻结合伙人 */
        public const int LOG_AGENT_FREEZE = 7;
        /** 日志类型：踢出合伙人 */
        public const int LOG_AGENT_KICK = 8;
        /** 日志类型：升级助理 */
        public const int LOG_SET_ADMIN = 9;
        /** 日志类型：设置为合伙人 */
        public const int LOG_SET_AGENT = 10;
        // =================规则修改====================
        /** 日志类型：冻结合伙人 */
        public const int LOG_RULE_UPDATE = 11;

        /** 流水号 */
        public long uuid;
        /** 所属竞赛场 */
        public long arena;
        /** 记录类型 */
        public int type;
        /** 目标 */
        public long dest;
        /** 目标昵称 */
        public string destname;
        /** 原由 */
        public long cause;
        /** 原由昵称 */
        public string causename;
        /** 上级 */
        public long master;
        /** 数值 */
        public long value;
        /** 创建时间 */
        public long time;
        /** 附加信息 */
        public string info;


        public void bytesRead(ByteBuffer data)
        {
            this.uuid = data.readLong();
            this.arena = data.readLong();
            this.type = data.readInt();
            this.dest = data.readLong();
            this.destname = data.readUTF();
            this.cause = data.readLong();
            this.causename = data.readUTF();
            this.master = data.readLong();
            this.value = data.readLong();
            this.time = data.readLong();
            this.info = data.readUTF();
        }

        public string getText()
        {
            string str = "";
            switch (type)
            {
                case LOG_JOIN:
                    str = destname + "（" + dest + "）被" + causename + "（" + cause + "）邀请进入休闲场";
                    break;
                case LOG_KICK:
                    str = destname + "（" + dest + "）被" + causename + "（" + cause + "）踢出休闲场";
                    break;
                case LOG_EXIT:
                    str = destname + "（" + dest + "）主动退出休闲场";
                    break;
                case LOG_AGENT_SCORE_CHANGE:
                    str = causename + "（" + cause + ")给合伙人" + destname + "（" + dest + (value > 0 ? ")增加" : ")减少") + MathKit.getRound2LongStr(value) + "积分";
                    break;
                case LOG_MEMBER_SCORE_CHANGE:
                    str = causename + "（" + cause + "）给玩家" + destname + "（" + dest + (value > 0 ? "）增加" : "）减少") + MathKit.getRound2LongStr(value) + "积分";
                    break;
                case LOG_MEMBER_PERMIT:
                    str = destname + "（" + dest + "）被" + causename + "（" + cause + "）"+info;
                    break;
                case LOG_AGENT_FREEZE:
                    str = causename + "（" + cause + "）" + info + destname + "（" + dest + ")";
                    break;
                case LOG_AGENT_KICK:
                    str = causename + "（" + cause + "）踢出合伙人" + destname + "（" + dest + ")，回收" + MathKit.getRound2LongStr(value) + "积分";
                    break;
                case LOG_SET_ADMIN:
                    str = destname + "（" + dest + "）被" + causename + "（" + cause + "）" + info;
                    break;
                case LOG_SET_AGENT:
                    str = destname + "（" + dest + "）被" + causename + "（" + cause + "）设置为合伙人";
                    break;
                case LOG_RULE_UPDATE:
                    str = causename + "（" + cause + "）" + info + " 规则";
                    break;
            }
            return str;
        }
    }
}
