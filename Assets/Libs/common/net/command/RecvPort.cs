namespace cambrian.common
{
    /// <summary>
    /// 类说明：接收后台数据
    /// @version 1.0
    /// @author maxw{woldits@qq.com}
    /// </summary>
    public class RecvPort : Port
    {
        /// <summary>
        /// 日志
        /// </summary>
        protected static Logger log = LogFactory.getLogger<RecvPort>(true);

        /** 接收到的数据 */
        protected ByteBuffer data;

        public RecvPort()
        {
        }

        public RecvPort(short id)
        {
            this.id = id;
        }

        public override void excute()
        {
            this.bytesRead(this.data);
        }

        public virtual void bytesRead(ByteBuffer data)
        {

        }

        /**
        * 消息处理方法， 参数connect为连接， 参数data是传送的消息，
        */

        public void transmit(TcpConnect connect, ByteBuffer data)
        {
            this.data = data;
            int port = data.readUnsignedShort(); // port
            int crc = data.readInt(); // crc
            this.excute();
        }
    }
}