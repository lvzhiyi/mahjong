using System;
using System.Net.Sockets;
using cambrian.game;
using UnityEngine;

namespace cambrian.common
{
    /// <summary>
    /// TCP连接
    /// </summary>
    public class TcpConnect : MonoBehaviour
    {
        /* static fields */
        /** 日志记录 */
        protected static Logger log = LogFactory.getLogger<TcpConnect>();


        /// <summary>
        /// ping间隔时间
        /// </summary>
        public static int pingintervaltime = 5000;

        /** 发送超时时间，毫秒 */
        private const int TIMEOUT = 1000;

        /** 默认的接收消息的最大长度，400k */
        public const int MAX_DATA_LENGTH = 1024 * 1024;

        //头信息
        public const int HEAD_SEND_SIZE = 16;
        public const int HEAD_ACCESS_SIZE = 22;
        public const int HEAD_SEND = 1;
        public const int HEAD_ACCESS = 2;

        /* fields */
        /** 流水号 */
        int uid;

        public string title;

        /** 连接的地址 */
        public string host;

        /** 连接的端口 */
        public int port;

        /** opened */
        public bool opened;

        /** 是否启用ping */
        public bool pingEnable;

        /** ping值 */
        public int pingValue;


        /// <summary> 最近ping时间 </summary>
        long lastpingtime;

        /// <summary>
        /// 连接的socket
        /// </summary>
        private TcpClient tcpClient;

        /// <summary> 连接的消息传送处理器 </summary>
        private ProxyDataHandler handler;

        /// <summary> 数据长度 </summary>
        private int len = 0;

        /// <summary> 等待读取头数据 </summary>
        private bool waitHead = true;
        /// <summary> 缓存已读数据 </summary>
        private ByteBuffer data;

        Action<TcpConnect, Exception> onConnectException;


        /** 获得消息处理器 */

        public ProxyDataHandler getTransmitHandler()
        {
            return this.handler;
        }

        /** 设置消息处理器 */

        public void setTransmitHandler(ProxyDataHandler handler)
        {
            this.handler = handler;
        }

        /* methods */

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="title"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public void init(string title, string host, int port, bool pingEnable,
            Action<TcpConnect, Exception> onConnectException)
        {
            this.enabled = false;
            this.pingEnable = pingEnable;
            this.title = title;
            this.host = host;
            this.port = port;
            this.onConnectException = onConnectException;

            this.tcpClient = new TcpClient();
            ProxyDataHandler handler = new ProxyDataHandler();
            handler.setRecvCommand(DataAccessHandler.getInstance());
            this.setTransmitHandler(handler);
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="onConnect">连接结果回调</param>
        public void connect(Action<TcpConnect, bool> onConnect)
        {
            try
            {
                if(data==null) data=new ByteBuffer();
                data.clear();
                this.len = 4;
                waitHead = true;
                if (log.isDebug) Debug.Log(log.getMessage("_connect"));
                if (this.tcpClient.Connected) this.tcpClient.Close();

                this.tcpClient = new TcpClient();
                this.tcpClient.Connect(this.host, this.port);
                this.tcpClient.SendTimeout = TIMEOUT;
                this.tcpClient.SendBufferSize = 8196*2;
                this.tcpClient.ReceiveBufferSize = MAX_DATA_LENGTH;
                this.tcpClient.SendBufferSize = MAX_DATA_LENGTH;
                this.tcpClient.NoDelay = false;
                if (log.isDebug) Debug.Log(log.getMessage("_connect ok"));
                this.enabled = true;
                this.opened = true;
                onConnect(this, true);
            }
            catch (Exception e)
            {
                if (log.isDebug) Debug.LogWarning(log.getMessage("_connect error:\n" + e));
                onConnect(this, false);
            }
            // onConnect(this, opened);
        }


        public void close()
        {
            this.opened = false;
            if (this.tcpClient != null)
            {
                this.tcpClient.Close();
                this.tcpClient = null;
            }
        }

        /** 获取流水号 */

        public int getUid()
        {
            return ++this.uid;
        }

        public void send(short port, ByteBuffer buffer)
        {
            int len = 10 + buffer.length;
            ByteBuffer head = new ByteBuffer(len);
            head.writeInt(len); //4
            head.writeShort(port); //2
            head.writeInt(CRC32.getValue(buffer.getArray(), buffer.getOffset(), buffer.length)); //4
            head.write(buffer);
            send(head.getArray(), head.getOffset(), head.length);
        }

        public void send(short port, short ackPort, int seq, ByteBuffer buffer)
        {
            int len = 16 + buffer.length;
            MsgBuffer head = new MsgBuffer(16, buffer.length);

            head.write(buffer);
            head.setShort(ackPort, 10); //2

            head.setInt(seq, 12); //4
            head.setOffsetUncheckLock(10);

            head.setInt(len, 0); //4
            head.setShort(port, 4); //2
            head.setInt(CRC32.getValue(head.getArray(), head.getOffset(), head.length), 6); //4
            head.setOffsetUncheckLock(0);

            send(head.getArray(), head.getOffset(), head.length);
        }

        public void send(byte[] bytes, int offset, int len)
        {
            NetworkStream ns = this.tcpClient.GetStream();
            ns.Write(bytes, offset, len);
            ns.Flush();
        }

        public void ping(long time)
        {
            if (time - this.lastpingtime < pingintervaltime) return;
            this.lastpingtime = time;
            try
            {
                byte[] bytes = new byte[10];
                ByteKit.writeInt(bytes, 10, 0);
                ByteKit.writeShort(bytes, ProxyDataHandler.PORT_ECHO, 4);
                ByteKit.writeInt(bytes, this.pingValue, 6);
                this.send(bytes, 0, 10);
            }
            catch (Exception)
            {
                onConnectException(this, new DataAccessException("网络中断"));
            }
        }

        /// <summary>
        /// 最近通信时间
        /// </summary>
        long lasttime;

        void Update()
        {
            if (!opened) return;
#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
            try
            {
                this.receive();
            }
            catch (Exception e)
            {
                this.onConnectException(this, e);
                return;
            }
#else
            this.receive();
#endif
            if (pingEnable)
            {
                long time = TimeKit.currentTimeMillis;
                if (time < 0) return;
                //ping消息发送10秒后还没收到返回消息
                if (lastpingtime>0&&time - this.lastpingtime > 4000)
                {
                    if (this.lasttime > 0 && this.lastpingtime > this.lasttime)
                    {
                        this.onConnectException(this, new DataAccessException("网络超时"));
                        return;
                    }
                }
                ping(time);
            }
        }

        /// <summary>
        /// 重置连接状态
        /// </summary>
        public void resetStatus()
        {
            if (this.tcpClient != null && this.tcpClient.Connected) this.tcpClient.Close();
            this.opened = false;
            this.enabled = false;
            this.lastpingtime = 0;
            this.lasttime = 0;
        }

        void receive()
        {
            if (tcpClient == null || !tcpClient.Connected) return;
            
            NetworkStream ns = tcpClient.GetStream();
            while (readData(ns)) ;
        }

        bool readData(NetworkStream ns)
        {
            if (tcpClient.Available == 0) return false;// 无数据可读
            int remainning = tcpClient.Available;
            if (remainning < this.len)// 数据小于待读长度
            {
                int top = data.getTop();
                ns.Read(data.getArray(), top, remainning);
                data.setTop(top + remainning);
                this.len -= remainning;
                return false; // 一条消息数据未完，继续等待
            }
            else// 数据大于待读长度
            {
                int top = data.getTop();
                ns.Read(data.getArray(), top, this.len);
                data.setTop(top + this.len);
                if (waitHead)
                {
                    this.len = data.readInt()-4;
                    data.clear().setCapacity(len);
                    waitHead = false;
                }
                else
                {
                   // try
                    //{
                        this.transmit(data);
                   // }
                    //catch (NullReferenceException e)
                    //{
                    //    print("NullReferenceException==" + e.Message + "，");
                    //}
                    //catch (Exception e)
                    //{
                    //    print("e===="+e.Message);
                    //}
                    //finally
                    //{
                        data.clear();
                        waitHead = true;
                        this.len = 4;
                    //}
                }
                return true;
            }
        }

        /** 连接的消息接收方法 */

        void transmit(ByteBuffer data)
        {
            long time = TimeKit.currentTimeMillis;
            
            int port = data.getUnsignedShort(0);

            if (port == ProxyDataHandler.PORT_PING)
            {
                this.lasttime = time;
                this.pingValue = (int) (time - lastpingtime);
            }
            else
            {
#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
                try
                {
                    handler.transmit(this, data);
                }
                catch (Exception e)
                {
                }
#else
                this.handler.transmit(this, data);
#endif
            }
        }
    }
}