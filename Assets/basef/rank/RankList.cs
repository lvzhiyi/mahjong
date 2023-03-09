using cambrian.common;

namespace basef.rank
{
    public class RankList:BytesSerializable
    {
        /** 周排行统计数（默认前30名） */
        public static int RANK_SIZE = 30;

        /// <summary>
        /// 自己打的局数
        /// </summary>
        long jueshu;
        /** 排行时间 */
        int week;
        /** 排行个数 */
        int size;
        /** 排行列表 */
        RankPlayer[] list;

        public RankList()
        {

        }

        public RankList(int size)
        {
            this.list = new RankPlayer[size];
        }

        public RankPlayer[] getRankList()
        {
            return this.list;
        }

        /// <summary>
        /// 获取局数
        /// </summary>
        /// <returns></returns>
        public long getJueShu()
        {
            return jueshu;
        }

        public void setJueShu(long jueshu)
        {
            this.jueshu = jueshu;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt(); // 排行中玩家数
            this.list = new RankPlayer[len];
            for (int i = 0; i < len; i++)
            {
                list[i] = new RankPlayer();
                list[i].bytesRead(data);
            }
        }
    }
}
