using basef.lobby;
using cambrian.common;
using scene.game;

namespace basef.notice
{
    public class RecvNoticeDeleteMsgCommand : RecvPort
    {
        /// <summary>
        /// 接收后台发送的公告变更消息
        /// </summary>
        public RecvNoticeDeleteMsgCommand()
        {           
            id = RecvConst.PORT_CLENT_SCROLL_NOTICE_DELETE;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int id = data.readInt();
            ShowLobbyPanel.delteNoticeMsg(id);
        }

    }
}
