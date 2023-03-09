using System;

namespace cambrian.common
{
    /// <summary>
    /// HTTP通信条目
    /// </summary>
    public class HttpEntry
    {
        /// <summary>
        /// 通信序列号
        /// </summary>
        public int id;
        /// <summary>
        /// 发送时间
        /// </summary>
        public long time;
        /// <summary>
        /// 返回数据
        /// </summary>
        public ByteBuffer value=null;

        public SimpleHttpCommand command;

        public HttpEntry(int id, long time, SimpleHttpCommand command)
        {
            this.id = id;
            this.time = time;
            this.command = command;
        }
    }
}