using cambrian.game;
using UnityEngine;

namespace cambrian.common
{
    /// <summary>
    /// 类说明：发送数据无返回
    /// @version 1.0
    /// @author maxw{woldits@qq.com}
    /// </summary>
    public class SendCommand : TcpCommand
    {
        protected static Logger log = LogFactory.getLogger<SendCommand>(true);
        public override TcpConnect getConnect()
        {
            return UserCommand.connect;
        }

        public override void excute()
        {
            ByteBuffer data = new ByteBuffer();
            this.bytesWrite(data);
            this.getConnect().send(this.id, data);
            if (log.isDebug)
                Debug.Log(log.getMessage(this, "id=" + this.id + " ,send , len=" + data.length + " ,time=" + this.excutetime));
        }
    }
}