using UnityEngine;

namespace cambrian.common
{
    /// <summary>
    /// 服务分发器
    /// </summary>
    public class ProxyDataHandler
    {
        /* static fields */
        /** 日志记录 */
        private static Logger log = LogFactory.getLogger<ProxyDataHandler>(true);
        /* 标准端口常量定义 */
        /** 反射端口 */
        public static short PORT_ECHO = 1;
        /** ping接收端口 */
        public static short PORT_PING = 2;
        /** 消息访问返回端口 */
        public static short PORT_ACCESS_RETURN = 0x3;
        /** 时间端口 */
        public static short PORT_TIME = 6;
        /** 属性端口 */
        public static short PORT_PROPERTY = 0x11;

        /* */
        /** 端口改变标志常量 */
        public static int HANDLER_CHANGED = 0, PORT_CHANGED = 1;

        /* fields */
        /** 缺省的消息传送处理接口 */
        private RecvPort transmitHandler;
        /** 内部端口对应的消息传送处理接口数组 */
        private RecvPort[] handlerArray = new RecvPort[0xffff];

        /* properties */
        /** 获得缺省的消息传送处理接口 */

        public RecvPort getTransmitHandler()
        {
            return this.transmitHandler;
        }

        /** 设置指定端口对应的消息传送处理接口 */

        public void setTransmitHandler(RecvPort handler)
        {
            this.transmitHandler = handler;
            if (log.isDebug)
                Debug.Log(log.getMessage("setTransmitHandler, " + handler));
        }

        /** 获得指定端口对应的消息传送处理接口 */

        public RecvPort getRecvCommand(int port)
        {
            return this.handlerArray[port];
        }

        /** 设置指定端口对应的消息传送处理接口 */

        public void setRecvCommand(RecvPort handler)
        {
            if (log.isDebug)
            {
                if (this.handlerArray[handler.id] != null)
                    Debug.LogWarning(log.getMessage("setRecvCommand this.handlerArray[handler.id] != null") + handler);
            }
            this.handlerArray[handler.id] = handler;
            if (log.isDebug)
                Debug.Log(log.getMessage("setPort, port=" + handler.id + " " + handler));
        }

        /* methods */

        /// <summary>
        /// 消息处理方法， 参数connect为连接， 参数data是传送的消息，
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="data"></param>
        public void transmit(TcpConnect connect, ByteBuffer data)
        {
            int port = data.getUnsignedShort(data.getOffset());
            RecvPort handler = this.getRecvCommand(port);

            if (handler != null)
            {
                handler.transmit(connect, data);
            }
            else if (this.transmitHandler != null)
            {
                this.transmitHandler.transmit(connect, data);
            }
        }
    }
}