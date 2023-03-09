using cambrian.common;
using scene.game;

namespace basef.im
{
    /// <summary>
    /// 川牌-房间-客户端接收-发送的快捷消息
    /// </summary>
    public class RecvIMQuickMsgCommand :RecvPort
    {

        public RecvIMQuickMsgCommand()
        {
            this.id = RecvConst.COMMAND_IM_READYFOR_QUICKMESSAGE;
        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            IMQuickMsg msg = new IMQuickMsg();
            msg.bytesRead(data);
            IMQuickMsgManager.executeMsg(msg);
        }
    }
}
