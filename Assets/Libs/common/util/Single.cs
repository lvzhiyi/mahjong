using System;

namespace cambrian.common
{
    public sealed class Single
    {
        /** 默认的通配键 */
        public static readonly string WILDCARD = "*";
        /** 空byte数组 */
        public static readonly byte[] EMPTY_BYTE_ARRAY = { };
        /** 空short数组 */
        public static readonly short[] EMPTY_SHORT_ARRAY = { };
        /** 空int数组 */
        public static readonly int[] EMPTY_INT_ARRAY = { };
        /** 空long数组 */
        public static readonly long[] EMPTY_LONG_ARRAY = { };
        /** 空float数组 */
        public static readonly float[] EMPTY_FLOAT_ARRAY = { };
        /** 空double数组 */
        public static readonly double[] EMPTY_DOUBLE_ARRAY = { };
        /** 空char数组 */
        public static readonly char[] EMPTY_CHAR_ARRAY = { };
        /** 空String数组 */
        public static readonly string[] EMPTY_STRING_ARRAY = { };
        /** 空Object数组 */
        public static readonly object[] EMPTY_OBJECT_ARRAY = { };
        /** 空标识对象 */
        public static readonly object VOID = new Object();
        /** 100以内质数数组 */
        public static readonly int[] PRIME_NUMBERS = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
        /** 幂数，指数字符 */
        public static readonly char[] MIS = { 'º', '¹', '²', '³', '⁴', '⁵', '⁶', '⁷', '⁸', '⁹' };
        /** 十六进制数字（小写）字符列表 */
        public static readonly char[] HEX_LOWER = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
        /** 十六进制数字（大写）字符列表 */
        public static readonly char[] HEX_UPPER = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        /** 十天干 */
        public static readonly char[] TG = { '甲', '乙', '丙', '丁', '戊', '己', '庚', '辛', '壬', '癸' };
        /** 十二地支 */
        public static readonly char[] DZ = { '子', '丑', '寅', '卯', '辰', '巳', '午', '未', '申', '酉', '戌', '亥' };
        /** 十二生肖 */
        public static readonly char[] ZODIACS = { '鼠', '牛', '虎', '兔', '龙', '蛇', '马', '羊', '猴', '鸡', '狗', '猪' };
        /** 农历月 */
        public static readonly char[] MONTH_ZH = { '正', '二', '三', '四', '五', '六', '七', '八', '九', '十', '冬', '腊' };
        /** 中文数字 */
        public static readonly char[] NUMS_ZH_LOWER = { '〇', '一', '二', '三', '四', '五', '六', '七', '八', '九', '十' };
        /** 中文大写数字 */
        public static readonly char[] NUMS_ZH_UPPER = { '零', '壹', '贰', '叁', '肆', '伍', '陆', '柒', '捌', '玖', '拾' };
        /** 中文数字单位 */
        public static readonly char[] UNIT_ZH_LOWER = { '个', '拾', '百', '千', '万', '亿', '兆' };
        /** 中文大写数字单位 */
        public static readonly char[] UNIT_ZH_UPPER = { '個', '拾', '佰', '仟', '萬', '億', '兆' };
        /** 二十四节气 */
        public static readonly string[] SOLAR_TERMS = new string[]{"小寒","大寒","立春","雨水","惊蛰","春分","清明","谷雨","立夏","小满","芒种","夏至","小暑","大暑",
        "立秋","处暑","白露","秋分","寒露","霜降","立冬","小雪","大雪","冬至"};
    }
}
