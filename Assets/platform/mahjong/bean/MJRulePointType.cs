namespace platform.mahjong
{
    /// <summary>
    /// 麻将规则，具体的番数类型
    /// </summary>
    public class MJRulePointType
    {
        /// <summary>对对胡</summary>
        public const int P_TRIPLET = 0;

        /// <summary>小七对</summary>
        public const int P_NEAT = 1;

        /// <summary>龙七对</summary>
        public const int P_NEAT_R = 2;

        /// <summary>双龙七对</summary>
        public const int P_NEAT_R1 = 3;

        /// <summary>三龙七对</summary>
        public const int P_NEAT_R2 = 4;

        /// <summary>抢杠</summary>
        public const int A_ROB = 5;

        /// <summary>自摸</summary>
        public const int A_SELF = 6;

        /// <summary>杠上花（1番）</summary>
        public const int A_KONG_HU = 7;

        /// <summary>杠上炮（1番）</summary>
        public const int A_KONG_PAO = 8;

        /// <summary>中张/断幺九（1番）：不包含面值为1和9的牌 ，也叫断幺九 </summary>
        public const int A_NO_19 = 9;

        /// <summary>门清（1番）：除暗杠外，没有碰或杠</summary>
        public const int A_NO_FIX = 10;

        /// <summary>清一色（2番）：所有牌都是同一种花色</summary>
        public const int A_QING = 11;

        /// <summary>将对（2番）：基础牌型为对对胡，且所有碰杠牌，刻子牌，将牌均为面值为2,5,8的牌</summary>
        public const int A_ONLY_258 = 12;

        /// <summary>金钩钩/大单钓（2番）：基础牌为对对胡，所有牌都碰或杠，仅剩一张手牌，大单钓</summary>
        public const int A_SINGLE = 13;

        /// <summary>带幺九/全幺九（2番）：每副牌都带1或9，如果基础牌为对对胡，那么就是全幺九</summary>
        public const int A_WITH_19 = 14;

        /// <summary>海底（1番）：最后一张胡牌或点炮 </summary>
        public const int A_HAIDI = 15;

        /// <summary>胡绝张（1番）：要胡的牌仅剩一张</summary>
        public const int A_JUST_ONE = 16;

        /// <summary> 有1根： </summary>
        public const int A_ROOT_1 =17;

        /// <summary> 有2根： </summary>
        public const int A_ROOT_2 = 18;

        /// <summary> 有3根（最多出现根数）： </summary>
        public const int A_ROOT_3 = 19;

        /// <summary> 天胡（3番）： </summary>
        public const int A_TIAN_HU = 20;

        /// <summary> 地胡（2番）： </summary>
        public const int A_DI_HU = 21;

        /// <summary> 有4根（最多出现根数）： </summary>
        public const int A_ROOT_4 = 22;
        /// <summary>
        /// 对对胡（2番）
        /// </summary>
        public const int P_TRIPLET_2 = 23;
        /// <summary>
        /// 卡心5
        /// </summary>
        public const int A_LSN_MID_5 = 24;

        public static int getTypePoint(int type)
        {
            return Room.room.getRule().getRuleTypePoint(type);
        }
    }
}
