using cambrian.common;

namespace platform.spotred.room
{
    /// <summary>
    /// 比赛房间
    /// </summary>
    public class Voting:BytesSerializable
    {
        /** 投票规则调整 */
        public const int VOTE_ADJUST = 1;
        /** 投票解散 */
        public const int VOTE_DISBAND = 2;

        // voterStatus[index]为下面的值，0表示没有同意也没拒绝
        public const int VOTE_AGREED = 1;
        public const int VOTE_REFUSED = -1;

        /** 结束时间 */
        long endTime;
        /** 发起者索引 */
        public int sponsor;
        /** 解散类型 */
        public int type;
        /** 各个位置人的操作状态 */
        public int[] voterStatus;

        public MJAdjustment justment;

        public Voting() 
        {
                
        }

        public bool isAgreed(int index)
        {
            return voterStatus[index] == VOTE_AGREED;
        }

        public bool isRefused(int index)
        {
            return voterStatus[index] == VOTE_REFUSED;
        }

        public bool isWaiting(int index)
        {
            return voterStatus[index] == 0;
        }

        public long getOvertime()
        {
            return endTime;
        }

        public override void bytesRead(ByteBuffer data)
        {
            type = data.readInt();
            endTime = data.readLong();
            sponsor = data.readInt();
            int len = data.readInt();
            voterStatus=new int[len];
            for (int i = 0; i < voterStatus.Length; i++)
            {
                voterStatus[i] = data.readInt();
            }

            if (data.readBoolean())
            {
                justment=new MJAdjustment();
                justment.bytesRead(data);
            }
        }
    }
}
