using cambrian.common;
using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 点炮胡
    /// </summary>
    public class MJMatchHuOperate : MJBaseOperate
    {
        /// <summary>
        ///  胡牌类型：炮胡
        /// </summary>
        public const int HU_NORMAL = 1;
        /// <summary> 胡牌类型：自摸 </summary>
        public const int HU_SELF = 1<<1;
        /// <summary> 胡牌类型：抢杠 </summary>
        public const int HU_ROB = 1<<2;
        /// <summary> 胡牌类型：杠上炮 </summary>
        public const int HU_KONG = 1<<3;
        /// <summary> 胡牌类型：点杠花 </summary>
        public const int HU_KONG_OTHER = 1<<4;
        /// <summary> 胡牌类型：自杠花 </summary>
        public const int HU_KONG_SELF = 1 << 5;
        /// <summary> 胡牌类型：天胡 </summary>
        public const int HU_TIAN = 1 << 6;
        /// <summary> 胡牌类型：地胡 </summary>
        public const int HU_DI = 1 << 7;
        /// <summary> 胡牌类型：海底炮 </summary>
        public const int HU_HAIDI = 1 << 8;
        /// <summary>
        /// 胡牌情景类型：绝张胡
        /// </summary>
        public const int HU_JUST_ONE = 1 << 10;

        public static bool isZiMO(int hutype)
        {
            return StatusKit.hasStatus(hutype, HU_SELF)|| StatusKit.hasStatus(hutype, HU_TIAN) || StatusKit.hasStatus(hutype, HU_KONG_SELF) || StatusKit.hasStatus(hutype, HU_HAIDI| HU_SELF);
        }


        /// <summary> 胡牌者 </summary>
        public int index;
        /// <summary>
        /// 胡的哪张牌
        /// </summary>
        public int card;
        /// <summary> 胡牌顺序 </summary>
        public int order;
        /// <summary> 胡牌类型：抢杠，自摸，天胡，地胡等等，用于前端显示不同效果 </summary>
        public int huType;
        /// <summary>
        /// 来源
        /// </summary>
        public int from;

        public MJMatchHuOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex, isreplay)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            index = data.readInt();
            from = data.readInt();
            card = data.readInt();
            order = data.readInt();
            huType = data.readInt();
        }
    }
}
