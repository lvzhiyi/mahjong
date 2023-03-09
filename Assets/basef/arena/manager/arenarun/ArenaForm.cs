using cambrian.common;

namespace basef.arena
{
    public class ArenaForm:BytesSerializable
    {
        /// <summary>
        /// 自己的金豆
        /// </summary>
         long arenagold;

        public double getArenaGold()
        {
            return (MathKit.getRound2Long(arenagold));
        }

        /// <summary>
        /// 自己推广任务
        /// </summary>
        long task;

        public double getTask()
        {
            return MathKit.getRound2Long(task);
        }

        /// <summary>
        /// 所有玩家金豆总数(除自己外)
        /// </summary>
         long totalGold;
        public double getTotalGold()
        {
            return MathKit.getRound2Long(totalGold);
        }
        /// <summary>
        /// 下级的推广任务总数
        /// </summary>
        long totalTask;

        public double getNextTotalTask()
        {
            return MathKit.getRound2Long(totalTask);
        }

        /// <summary>
        /// 所有负数金豆总数
        /// </summary>
        long oweGold;

        public double getOweGold()
        {
            return MathKit.getRound2Long(oweGold);
        }

        public double getTotalArenaGold()
        {
            return getArenaGold() + getTask() + getTotalGold()+ getNextTotalTask();
        }

        public override void bytesRead(ByteBuffer data)
        {
            arenagold = data.readLong();
            task = data.readLong();
            totalGold = data.readLong();
            totalTask = data.readLong();
            oweGold = data.readLong();
        }
    }
}
