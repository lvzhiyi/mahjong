using cambrian.common;

namespace basef.arena
{
    public class TicketTimeSoltTotal:BytesSerializable
    {
       // totals,0:达标房间数,1:参与人次，2：累收门票总数，3：实际返利总数，4.下级返利总数 ，
        public long reachRoom;

        /// <summary>
        /// 参与人数
        /// </summary>
        public long matchCount;
        /// <summary>
        /// 累收门票
        /// </summary>
        private long totalTicket;
        /// <summary>
        /// 获取累收门票
        /// </summary>
        /// <returns></returns>
        public double getTotalTicket()
        {
            return MathKit.getRound2Long(totalTicket);
        }

        /// <summary>
        /// 实际返利
        /// </summary>
        private long realRebate;
        /// <summary>
        /// 获取实际返利
        /// </summary>
        /// <returns></returns>
        public double getRealRebate()
        {
            return MathKit.getRound2Long(realRebate);
        }

        /// <summary>
        /// 下级返利总数
        /// </summary>
        private long nextRebate;
        /// <summary>
        /// 获取下级返利总数
        /// </summary>
        /// <returns></returns>
        public double getNextRebate()
        {
            return MathKit.getRound2Long(nextRebate);
        }

        public override void bytesRead(ByteBuffer data)
        {
            reachRoom = data.readLong();
            matchCount = data.readLong();
            totalTicket = data.readLong();
            realRebate = data.readLong();
            nextRebate = data.readLong();
        }
    }
}
