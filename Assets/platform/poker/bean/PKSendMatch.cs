namespace platform.poker
{
    public class PKSendMatch
    {
        /// <summary> 操作类型：叫地主 </summary>
        public const int JIAOZHUANG = 101;

        /// <summary> 操作类型：抢地主 </summary>
        public const int QIANGZHUANG = 102;

        /// <summary> 操作类型：加倍 </summary>
        public const int JIABEI = 103;

        /// <summary> 操作类型：明牌 </summary>
        public const int MING = 104;

        /// <summary> 操作类型：出牌 </summary>
        public const int DISCARD = 105;

        /// <summary> 操作类型：取消 </summary>
        public const int CANCEL = 106;

        /// <summary> 操作类型：取消明牌</summary>
        public const int CANCEL_MING = 107;

        /// <summary> 操作类型：叫分</summary>
        public const int JIAOFEN = 108;
        /// <summary>
        /// 扯牌
        /// </summary>
        public const int CHE_PAI = 112;
    }
}
