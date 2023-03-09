using cambrian.common;

namespace platform.spotred
{
    public class Account
    {
        /** 加番类型： 天胡*/
        public static long HU_TIAN = 1 << 0;
        /** 加番类型： 海底*/
        public static long HU_HAIDI = 1 << 1;
        /** 加番类型： 碰三按七*/
        public static long HU_PONG3_7 = 1 << 2;
        /** 加番类型： 碰三全黑*/
        public static long HU_PONG3_BLACK = 1 << 3;
        /** 加番类型： 自摸*/
        public static long HU_XIAOJIA_ZIMO = 1 << 4;
        /** 加番类型： 引碰*/
        public static long HU_YINKONG = 1 << 5;
        /** 加番类型： 碰天牌*/
        public static long HU_PONG_TIAN = 1 << 6;
        /** 加番类型： 碰地牌*/
        public static long HU_PONG_DI = 1 << 7;
        /** 加番类型： 碰人牌*/
        public static long HU_PONG_REN = 1 << 8;
        /** 加番类型： 碰和牌 */
        public static long HU_PONG_HE = 1 << 9;
        /** 加番类型： 碰幺四*/
        public static long HU_PONG_YAO = 1 << 10;
        /** 加番类型： 扯奏*/
        public static long HU_CHEZOU = 1 << 11;
        /** 加番类型：全红 */
        public static long HU_ALL_RED = 1 << 12;
        /** 加番类型： 全黑 */
        public static long HU_ALL_BLACK = 1 << 13;
        /** 加番类型： 喂4根 */
        public static long HU_ALL_WEI4COUNT = 1 << 14;
        /** 加番类型： 小家碰黑牌 */
        public static long HU_PONG_BLACK_XIAOJIA = 1 << 15;
        /// <summary>
        /// 加番类型：报牌加番
        /// </summary>
        public static long HU_BAOPAI = 1 << 16;
        /// <summary>
        /// 加番类型: 点炮加番
        /// </summary>
        public static long HU_DIANPAO = 1 << 17;
        /// <summary>
        /// 加番类型: 巴底黄
        /// </summary>
        public static long HU_BADIHUANG = 1 << 18;
        /// <summary>
        ///  加番类型：十八烂
        /// </summary>
        public static long HU_18LAN = 1 << 19;
        /** 加番类型： 碰红七 */
        public static long HU_PONG_C34 = 1 << 20;
        /** 加番类型： 碰红九 */
        public static long HU_PONG_C45 = 1 << 21;

        /** 加番类型： 吃成坎 */
        public static long HU_CHOW_KAN = 1 << 22;
        /** 加番类型： 3张相同牌 */
        public static long HU_SAME_THREE = 1 << 23;
        /// <summary>
        /// 加番类型：炸
        /// </summary>
        public static long HU_ZHA_PAI = 1 << 27;
        /// <summary>
        /// 爆炸
        /// </summary>
        public static long HU_BAO_ZHA = 1 << 28;
        /// <summary>
        /// 认档
        /// </summary>
        public static long HU_REN_DANG = 1 << 29;
        /// <summary>
        /// 加番类型：加漂
        /// </summary>
        public static long HU_JIAPIAO =1<<30;
        /// <summary>
        /// 加番类型：红龙
        /// </summary>
        public static long HU_HONGLONG =1<<31;
        /// <summary>
        /// 加番类型：赶边
        /// </summary>
        public static long HU_GANBIAN =1<<32;
        /// <summary>
        /// 加番类型：黑龙
        /// </summary>
        public static long HU_HEILONG =1<<33;
        /// <summary>
        /// 加番类型：倒赶边
        /// </summary>
        public static long HU_DAO_GANBIAN =1<<34;

        public static string getInfo(long value)
        {
            CharBuffer buf = new CharBuffer();
            if (StatusKit.hasStatus(value, HU_TIAN)) buf.append("天胡");
            if (StatusKit.hasStatus(value, HU_HAIDI)) buf.append(buf.length() > 0 ? "," : "").append("海底");
            if (StatusKit.hasStatus(value, HU_PONG3_7)) buf.append(buf.length() > 0 ? "," : "").append("碰三案七");
            if (StatusKit.hasStatus(value, HU_PONG3_BLACK)) buf.append(buf.length() > 0 ? "," : "").append("碰三案黑");
            if (StatusKit.hasStatus(value, HU_XIAOJIA_ZIMO)) buf.append(buf.length() > 0 ? "," : "").append("自摸");
            if (StatusKit.hasStatus(value, HU_YINKONG)) buf.append(buf.length() > 0 ? "," : "").append("引碰");
            if (StatusKit.hasStatus(value, HU_PONG_TIAN)) buf.append(buf.length() > 0 ? "," : "").append("碰天牌");
            if (StatusKit.hasStatus(value, HU_PONG_DI)) buf.append(buf.length() > 0 ? "," : "").append("碰地牌");
            if (StatusKit.hasStatus(value, HU_PONG_REN)) buf.append(buf.length() > 0 ? "," : "").append("碰人牌");
            if (StatusKit.hasStatus(value, HU_PONG_HE)) buf.append(buf.length() > 0 ? "," : "").append("碰和牌");
            if (StatusKit.hasStatus(value, HU_PONG_YAO)) buf.append(buf.length() > 0 ? "," : "").append("碰幺四");
            if (StatusKit.hasStatus(value, HU_CHEZOU)) buf.append(buf.length() > 0 ? "," : "").append("扯奏");
            if (StatusKit.hasStatus(value, HU_ALL_RED)) buf.append(buf.length() > 0 ? "," : "").append("全红");
            if (StatusKit.hasStatus(value, HU_ALL_BLACK)) buf.append(buf.length() > 0 ? "," : "").append("全黑");
            if (StatusKit.hasStatus(value, HU_ALL_WEI4COUNT)) buf.append(buf.length() > 0 ? "," : "").append("喂4根");
            if (StatusKit.hasStatus(value, HU_PONG_BLACK_XIAOJIA))
                buf.append(buf.length() > 0 ? "," : "").append("碰黑牌");
            if (StatusKit.hasStatus(value, HU_BAOPAI)) buf.append(buf.length() > 0 ? "," : "").append("报牌");
            if (StatusKit.hasStatus(value, HU_DIANPAO)) buf.append(buf.length() > 0 ? "," : "").append("点炮胡");
            if (StatusKit.hasStatus(value, HU_BADIHUANG)) buf.append(buf.length() > 0 ? "," : "").append("巴底黄");
            if (StatusKit.hasStatus(value, HU_18LAN)) buf.append(buf.length() > 0 ? "," : "").append("十八烂");
            if (StatusKit.hasStatus(value, HU_PONG_C34)) buf.append(buf.length() > 0 ? "," : "").append("碰红七");
            if (StatusKit.hasStatus(value, HU_PONG_C45)) buf.append(buf.length() > 0 ? "," : "").append("碰红九");
            if (StatusKit.hasStatus(value, HU_CHOW_KAN)) buf.append(buf.length() > 0 ? "," : "").append("吃成坎");
            if (StatusKit.hasStatus(value, HU_SAME_THREE)) buf.append(buf.length() > 0 ? "," : "").append("3张相同牌");
            if(StatusKit.hasStatus(value,HU_ZHA_PAI)) buf.append(buf.length() > 0 ? "," : "").append("炸");
            if (StatusKit.hasStatus(value, HU_BAO_ZHA)) buf.append(buf.length() > 0 ? "," : "").append("爆炸");
            if (StatusKit.hasStatus(value, HU_REN_DANG)) buf.append(buf.length() > 0 ? "," : "").append("认档");
            if (StatusKit.hasStatus(value, HU_JIAPIAO)) buf.append(buf.length() > 0 ? "," : "").append("加飘");
            if (StatusKit.hasStatus(value, HU_HONGLONG)) buf.append(buf.length() > 0 ? "," : "").append("红龙");
            if (StatusKit.hasStatus(value, HU_GANBIAN)) buf.append(buf.length() > 0 ? "," : "").append("赶边");
            if (StatusKit.hasStatus(value, HU_HEILONG)) buf.append(buf.length() > 0 ? "," : "").append("黑龙");
            if (StatusKit.hasStatus(value, HU_DAO_GANBIAN)) buf.append(buf.length() > 0 ? "," : "").append("倒赶边");
            return buf.getString();
        }
    }
}
