namespace cambrian.common
{
    /// <summary>
    /// 日志记录器工厂
    /// </summary>
    public class LogFactory
    {
        /* static fields */
        /** 当前的记录器工厂 */
        private static LogFactory factory = new LogFactory();

        /* static methods */
        /// <summary> 获得指定类的记录器，指定是否是debug模式 </summary>
        public static Logger getLogger<T>(bool isDebug=true)
        {
            return factory.getInstance(typeof(T).FullName, isDebug);
        }
        /* fields */

        /* constructors */

        /* properties */

        /* init start */

        /* methods */
        public Logger getInstance(string name, bool isDebug)
        {
#if !UNITY_EDITOR
            isDebug = false;
#endif
            return new Logger(name, false);
        }
        /* common methods */

    }
}