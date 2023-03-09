using System;

namespace cambrian.common
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// 类名
        /// </summary>
        private string name;
        /// <summary>
        /// 开关
        /// </summary>
        public bool isDebug;

        public Logger(string name, bool isDebug)
        {
            this.name = name;
            this.isDebug = isDebug;
        }

        public string getMessage(object message)
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff") + " DEBUG " + this.name + " - " + message;
        }
        public string getMessage(object source, object message)
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff") + " DEBUG " + source.GetType().FullName + " - " + message;
        }
    }
}