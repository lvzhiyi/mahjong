using cambrian.common;

namespace basef.arena
{
    public class TaskBill:Bill
    {
        /** 变动类型： 任务发放 */
        public const int TYPE_DISPATCH_SUP = 101;
        /** 变动类型： 任务回收 */
        public const int TYPE_APPLY_DECR_SUB = 102;
        /** 增加类型： 任务退回(上级拒绝减少任务申请) */
        public const int TYPE_APPLY_DECR_REFUSED = 103;
        /** 变动类型： 玩家兑换奖章 */
        public const int TYPE_PLAYER_MEDAL_EXCHANGE = 104;


        /** 减少类型： 给下级发放任务 */
        public const int TYPE_DISPATCH_SUB = 201;
        /** 变动类型： 任务减少申请 */
        public const int TYPE_APPLY_DECR_SUP = 202;
        /** 变动类型： 玩家报名 */
        public const int TYPE_PLAYER_SIGN_UP = 203;
        //变动类型：终止比赛
        public const int TYPE_PLAYER_STOP_GAME = 301;
        /** 数值模式 */
        public int mode;
        /** 竞技场id */
        public long arenaid;
        /// <summary>
        /// 上级id
        /// </summary>
        public long master;
        /** 剩余数量 */
        long task;

        public string getTaskType()
        {
            switch (type)
            {
                case TYPE_PLAYER_STOP_GAME:
                    return "修改积分";
                case TYPE_DISPATCH_SUP:
                    return "任务发放";//有
                case TYPE_APPLY_DECR_SUB:
                    return "任务回收";//有
                case TYPE_APPLY_DECR_SUP:
                    return "任务减少申请";//有
                case TYPE_PLAYER_MEDAL_EXCHANGE:
                    return "兑换奖章";//有
                case TYPE_APPLY_DECR_REFUSED:
                    return "任务退回";
                case TYPE_PLAYER_SIGN_UP:
                    return "赛场报名";//有
                case TYPE_DISPATCH_SUB:
                    return "给下级发放任务";
                default: return "";
            }
        }

        /// <summary>
        /// 获取剩余的推广任务
        /// </summary>
        /// <returns></returns>
        public double getTask()
        {
            return MathKit.getRound2Long(task);
        }

        /// <summary>
        /// 获取单据交易的值
        /// </summary>
        /// <returns></returns>
        public double getValue()
        {
            return MathKit.getRound2Long(value);
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.uuid = data.readLong();
            this.source = data.readLong();
            this.type = data.readInt();
            this.mode = data.readInt();
            this.value = data.readLong();
            this.task = data.readLong();
            this.arenaid = data.readLong();
            this.master = data.readLong();
            this.time = data.readLong();
            this.info = data.readUTF();
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(uuid);
            data.writeLong(source);
            data.writeInt(type);
            data.writeInt(mode);
            data.writeLong(value);
            data.writeLong(task);
            data.writeLong(arenaid);
            data.writeLong(master);
            data.writeLong(time);
            data.writeUTF(info);
        }
    }
}
