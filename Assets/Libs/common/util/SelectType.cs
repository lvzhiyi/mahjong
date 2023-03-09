namespace cambrian.common
{
    /**
     * 类说明：
     * 
     *  通过委托方法，对T类型对象做出赛选操作，返回赛选结果类型
     *  以下是可能的结果：
     *   {SelectType.FALSE}         不符合赛选
     *   {SelectType.TRUE}          符合赛选
     *   {SelectType.FALSE_BREAK}   不符合赛选并中断进一步赛选，例如从一批待赛选对象中赛选一个不符合条件的对象
     *   {SelectType.TRUE_BREAK}    符合赛选并中断进一步赛选，例如从一批待赛选对象中赛选一个符合条件的对象
     * 
     * @author HYZ [huangyz1988@qq.com]
     * @version 2018年11月23日 上午12:08:57
     */
    public delegate int Selector<in T>(T t);

    /**
     * 类说明：
     * 
     * <pre>
     *  通过{@linkplain #select select(T t)}方法，对T类型对象做出赛选操作，返回赛选结果类型
     *  以下是可能的结果：
     *   {@linkplain #FALSE FALSE}         不符合赛选
     *   {@linkplain #TRUE TRUE}          符合赛选
     *   {@linkplain #FALSE_BREAK FALSE_BREAK}   不符合赛选并中断进一步赛选，例如从一批待赛选对象中赛选一个不符合条件的对象
     *   {@linkplain #TRUE_BREAK TRUE_BREAK}    符合赛选并中断进一步赛选，例如从一批待赛选对象中赛选一个符合条件的对象
     * </pre>
     * 
     * @author HYZ [huangyz1988@qq.com]
     * @version 2018年11月23日 上午12:08:57
     */
    public class SelectType
    {

        /** 赛选结果状态值：不符合选择 */
        public const int FALSE = 0;
        /** 赛选结果状态值：符合选择 */
        public const int TRUE = 1;
        /** 赛选结果状态值：不符合赛选并中断进一步赛选 */
        public const int FALSE_BREAK = 2;
        /** 赛选结果状态值：符合赛选并中断进一步赛选 */
        public const int TRUE_BREAK = 3;
    }
}
