using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 规则结算信息
    /// </summary>
    public class ArenaRuleAccountcs:BytesSerializable
    {
        /// <summary>
        /// 规则锁定id
        /// </summary>
        public int rulelockid;
        /// <summary>
        /// 规则名
        /// </summary>
        public string rulename;
        /// <summary>
        /// 今日门票返利数
        /// </summary>
        int t_ticket;
        /// <summary>
        /// 昨日日门票返利数
        /// </summary>
        int y_ticket;
        /// <summary>
        /// 本周门票返利数
        /// </summary>
        int w_ticket;
        /// <summary>
        /// 上周门票返利数
        /// </summary>
        int l_w_ticket;
        /// <summary>
        /// 今日参与人次
        /// </summary>
        public int t_match;
        /// <summary>
        /// 昨日参与人次
        /// </summary>
        public int y_match;
        /// <summary>
        /// 本周参与人次
        /// </summary>
        public int w_match;
        /// <summary>
        /// 上周参与人次
        /// </summary>
        public int l_w_match;

        public ArenaRuleAccountcs(int id,string name)
        {
            rulelockid = id;
            rulename = name;
        }

        public void addTodayMatch(int match)
        {
            t_match += match;
        }

        public void addYesterDayMatch(int match)
        {
            y_match += match;
        }

        public void addWeekMatch(int match)
        {
            w_match += match;
        }

        public void addLastWeekMatch(int match)
        {
            l_w_match += match;
        }

        public void addTodayTicket(int ticket)
        {
            t_ticket += ticket;
        }

        public void addYesterDayTicket(int ticket)
        {
            y_ticket += ticket;
        }

        public void addWeekTicket(int ticket)
        {
            w_ticket += ticket;
        }

        public void addLastWeekTicket(int ticket)
        {
            l_w_ticket += ticket;
        }

        public string getTicketData()
        {
            return t_ticket * 1d / Arena.PROPORTION  + "/" + y_ticket * 1d / Arena.PROPORTION  + "/" +
                   w_ticket * 1d / Arena.PROPORTION  + "/" + l_w_ticket * 1d / Arena.PROPORTION ;
        }

        public string getMatchData()
        {
            return t_match + "/" + y_match + "/" + w_match + "/" + l_w_match;
        }

    }
}
