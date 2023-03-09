using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 规则结算
    /// </summary>
    public class RuleAccount
    {
        /// <summary>
        /// 锁定规则ID
        /// </summary>
        public int rule;
        /// <summary>
        /// 规则名称
        /// </summary>
        public string rulename;
        /// <summary>
        /// 门票返利数
        /// </summary>
        public int ticket;
        /// <summary>
        /// 参与人次
        /// </summary>
        public int match;

        public void bytesRead(ByteBuffer data)
        {
            this.rule = data.readInt();
            this.rulename = data.readUTF();
            this.ticket = data.readInt();
            this.match = data.readInt();
        }
    }
}
