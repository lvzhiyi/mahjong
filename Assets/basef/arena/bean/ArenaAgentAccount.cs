using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 结算
    /// </summary>
    public class ArenaAgentAccount : BytesSerializable
    {
        /// <summary>
        /// 所属竞赛场
        /// </summary>
        long arenaid;

        /// <summary> 所属玩家 </summary>
        long userid;

        /// <summary> 所属日,周，月 </summary>
        long day, week, month;

        // =====总计======
        /// <summary> 门票收入 </summary>
        int ticket;

        /// <summary> 参与人次 </summary>
        int match;

        // =====今日======
        /// <summary> 门票收入 </summary>
        int d_ticket;

        /// <summary> 参与人次 </summary>
        int d_match;

        // =====昨日======
        /// <summary> 门票收入 </summary>
        int y_ticket;

        /// <summary> 参与人次 </summary>
        int y_match;

        // =====本周======
        /// <summary> 门票收入 </summary>
        int w_ticket;

        /// <summary> 参与人次 </summary>
        int w_match;

        // =====上周======
        /// <summary> 门票收入 </summary>
        int lw_ticket;

        /// <summary> 参与人次 </summary>
        int lw_match;

        // =====本月======
        /// <summary> 门票收入 </summary>
        int m_ticket;

        /// <summary> 参与人次 </summary>
        int m_match;

        // =====上月======
        /// <summary> 门票收入 </summary>
        int lm_ticket;

        /// <summary> 参与人次 </summary>
        int lm_match;




        public override void bytesRead(ByteBuffer data)
        {
            // 时间
            this.day = data.readLong();
            this.week = data.readLong();
            this.month = data.readLong();

            // 累计
            this.ticket = data.readInt();
            this.match = data.readInt();

            // 累计
            this.d_ticket = data.readInt();
            this.d_match = data.readInt();

            // 累计
            this.y_ticket = data.readInt();
            this.y_match = data.readInt();

            // 累计
            this.w_ticket = data.readInt();
            this.w_match = data.readInt();

            // 累计
            this.lw_ticket = data.readInt();
            this.lw_match = data.readInt();

            // 累计
            this.m_ticket = data.readInt();
            this.m_match = data.readInt();

            // 累计
            this.lm_ticket = data.readInt();
            this.lm_match = data.readInt();
        }
    }
}

