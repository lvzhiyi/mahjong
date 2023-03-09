namespace basef.prop
{
    /// <summary>
    /// 道具类型
    ///  <pre>
    ///下列数值组合值，用于确定道具最终类型
    ///      1  ==  唯一性的道具，此类型的道具uid全局唯一，用于同一模版的道具，但属性可变
    ///      2  ==  绑定的道具，不可交易的道具
    ///     4  ==  功能性道具，可使用的道具
    ///    8  ==  不可出售给系统的道具
    ///    16 ==  不可摧毁的道具

    /// </summary>
    public class PropType
    {
        /** 道具类型：唯一性的道具，此类型的道具uid全局唯一，不重复 */
        public static  int UNIQUE = 1 << 0;
        /** 道具类型：绑定的道具，不可交易的道具 */
        public static  int BIND = 1 << 1;
        /** 道具类型：功能性道具，可使用的道具 */
        public static  int FUNCTIONAL = 1 << 2;
        /** 道具类型：不可出售给系统的道具 */
        public static  int UNRECYCLABLE = 1 << 3;
        /** 道具类型：不可摧毁的道具 */
        public static  int INDESTRUCTIBLE = 1 << 4;
    }
}
