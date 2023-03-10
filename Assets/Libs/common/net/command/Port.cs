using System;

namespace cambrian.common
{
    /// <summary>
    /// 类说明：通讯接口
    /// </summary>
    public class Port
    {
        public const int OK = 0;
        /// <summary>
        /// 接口号
        /// </summary>
        public short id;
        /// <summary>
        /// 发送时记录发送时间,返回服务器消息时记录通信消耗的时间
        /// </summary>
        protected long excutetime;
        /// <summary>
        /// 回调函数
        /// </summary>
        public Action<object> callback;

        public object callbackobj;

        /// <summary>
        /// 执行命令
        /// </summary>
        public virtual void excute()
        {
            throw new SystemException(" ,must override this func!");
        }
    }
}