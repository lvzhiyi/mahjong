using UnityEngine;

namespace cambrian.common
{
    public class PintPort : RecvPort
    {
        /// <summary>
        /// 日志
        /// </summary>
        public PintPort()
        {
            this.id = ProxyDataHandler.PORT_PING;
        }
        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            long time = data.readLong();
            if (log.isDebug)
                Debug.Log(log.getMessage(this + ",lasttime=" + time + ",time=" + (TimeKit.currentTimeMillis - time)));
        }
    }
}