namespace platform.poker
{
    /// <summary> 斗地主玩家状态 </summary>
    public class PlayerStatus
    {
        /// <summary> 状态 : 已明牌状态 </summary>
        public const int MINGPAI = 1 << 0;

        /// <summary> 状态 : 加倍 </summary>
        public const int JIABEI = 1 << 1;

        /// <summary> 状态 : 叫地主 </summary>
        public const int JIAODIZHU = 1 << 2;

        /// <summary> 状态 : 抢地主 </summary>
        public const int QIANGDIZHU = 1 << 3;

        /// <summary> 状态 : 不叫 </summary>
        public const int NOJIAODIZHU = 1 << 4;

        /// <summary> 状态 : 抢地主 </summary>
        public const int NOQIANGDIZHU = 1 << 5;

        /// <summary> 状态 : 不要 </summary>
        public const int PASS = 1 << 6;

        /// <summary> 状态 : 不要 </summary>
        public const int NOJIABEI = 1 << 7;

        /// <summary> 状态 : 春天 </summary>
        public const int CHUN_TIAN = 1 << 8;

        /// <summary> 状态 : 返春 </summary>
        public const int FAN_CHUN_TIAN = 1 << 12;

        /// <summary> 状态 : 一分 </summary>
        public const int CALLSCORE_ONE = 1 << 9;

        /// <summary> 状态 : 两分 </summary>
        public const int CALLSCORE_TWO = 1 << 10;

        /// <summary> 状态 : 三分 </summary>
        public const int CALLSCORE_THREE = 1 << 11;
    }
}
