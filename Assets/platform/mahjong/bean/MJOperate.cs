using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 麻将操作类型
    /// </summary>
    public class MJOperate
    {
        // ===============可操作状态值（数值越大优先级越高）================
        /// <summary> 状态：可以过 </summary>
        public const  int CAN_CANCEL = 1 << 0;
        /// <summary> 状态：可以加飘 </summary>
        public const  int CAN_PIAO = 1 << 1;
        /// <summary> 状态：可以换牌 </summary>
        public const  int CAN_REPLACE = 1 << 2;
        /// <summary> 状态：可以定缺 </summary>
        public const  int CAN_DISTYPE = 1 << 3;
        /// <summary> 状态：可以报牌 </summary>
        public const  int CAN_BAOPAI = 1 << 4;
        /// <summary> 状态：可以听牌 </summary>
        public const  int CAN_LISTEN = 1 << 5;
        /// <summary> 状态：可以出牌 </summary>
        public const  int CAN_DISCARD = 1 << 6;
        /// <summary> 状态：可以补花 </summary>
        public const  int CAN_FLOWER = 1 << 7;
        /// <summary> 状态：可以吃 </summary>
        public const  int CAN_CHOW = 1 << 8;
        /// <summary> 状态：可以碰 </summary>
        public const  int CAN_PONG = 1 << 9;
        /// <summary> 状态： 可以杠 </summary>
        public const  int CAN_KONG = 1 << 10;
        /// <summary> 状态：可以胡 </summary>
        public const  int CAN_HU = 1 << 11;
        /// <summary>状态：可以估卖</summary>
        public const int CAN_SAEL = 1 << 12;
        /// <summary>状态：可以报杠</summary>
        public const int CAN_BAO_KONG = 1 << 13;


        /// <summary>
        /// 可以躺牌(前端使用的)
        /// </summary>
        public const int CAN_TANG = 1 << 30;
        /// <summary>
        /// 必须躺，没有过
        /// </summary>
        public const int MUST_TANG = 1 << 29;

        public static bool isCanSale(int operate)
        {
            return StatusKit.hasStatus(operate, CAN_SAEL);
        }

        /// <summary>
        /// 可以听牌
        /// </summary>
        /// <param name="operate"></param>
        /// <returns></returns>
        public static bool isCanTing(int operate)
        {
            return StatusKit.hasStatus(operate, CAN_LISTEN);
        }

        /// <summary>
        /// 是否可以换三张
        /// </summary>
        /// <param name="operate"></param>
        /// <returns></returns>
        public static bool isCanReplace(int operate)
        {
            return StatusKit.hasStatus(operate, CAN_REPLACE);
        }
        /// <summary>
        /// 可以飘
        /// </summary>
        /// <param name="operate"></param>
        /// <returns></returns>
        public static bool isCanPiao(int operate)
        {
            return StatusKit.hasStatus(operate, CAN_PIAO);
        }

        /// <summary>
        /// 是否是定缺
        /// </summary>
        /// <returns></returns>
        public static bool isDisType(int operate)
        {
            return StatusKit.hasStatus(operate, CAN_DISTYPE);
        }

        /// <summary>
        /// 可以出牌
        /// </summary>
        /// <returns></returns>
        public static bool hasCanDisCard(int operate)
        {
            return StatusKit.hasStatus(operate, CAN_DISCARD);
        }


        /// <summary>
        /// 只能出牌
        /// </summary>
        /// <returns></returns>
        public static bool isDisCard(int operate)
        {
            return StatusKit.isStatus(operate, CAN_DISCARD);
        }


        public static bool isCanHu(int operate)
        {
            return StatusKit.hasStatus(operate, CAN_HU);
        }

        // =================组合状态（这些组合状态可能可取消）===================
        /// <summary> 组合状态：可胡，碰 </summary>
        public const  int CAN_HU_PONG = CAN_HU | CAN_PONG;
        /// <summary> 组合状态：可胡，碰 </summary>
        public const  int CAN_HU_CHOW = CAN_HU | CAN_CHOW;
        /// <summary> 组合状态：可碰，吃 </summary>
        public const  int CAN_FIX = CAN_PONG | CAN_CHOW;
        /// <summary> 组合状态：可胡，碰，吃 </summary>
        public const  int CAN_HU_FIX = CAN_FIX | CAN_HU;
    }
}
