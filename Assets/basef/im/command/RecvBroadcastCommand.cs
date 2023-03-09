using cambrian.common;

namespace basef.im
{
    /// <summary>
    /// 聊天-接收-发送广播消息 
    /// </summary>
    public class RecvBroadcastCommand : RecvPort
    {

        public const int COMMAND_IM_READYFOR_BROADCAST = 6001;

        public RecvBroadcastCommand()
        {
            this.id = COMMAND_IM_READYFOR_BROADCAST;
        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
//            IMBroadcast msg = new IMBroadcast();
//            msg.bytesRead(data);
//
//            IMBroadcastManager.executeMsg(msg);
        }
    }
}
