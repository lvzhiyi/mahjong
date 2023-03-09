using basef.lobby;
using cambrian.common;
using scene.game;

namespace basef.notice
{
    public class RecvNoticeUpdateMsgCommand : RecvPort
    {
        /// <summary>
        /// 接收后台发送的公告变更消息
        /// </summary>
        public RecvNoticeUpdateMsgCommand()
        {           
            id = RecvConst.PORT_CLENT_SCROLL_NOTICE_UPDATE;
        }

        public override void bytesRead(ByteBuffer data)
        {
            ScrollNotice notice = new ScrollNotice();
            notice.bytesRead(data);
            ShowLobbyPanel.updateNoticeMsg(notice);
        }

    }
}
