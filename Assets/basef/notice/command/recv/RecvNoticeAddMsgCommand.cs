using basef.lobby;
using cambrian.common;
using scene.game;

namespace basef.notice
{
    /// <summary>
    /// 接收后台发送的添加公告消息
    /// </summary>
    public class RecvNoticeAddMsgCommand : RecvPort
    {
        public RecvNoticeAddMsgCommand()
        {
            this.id = RecvConst.PORT_CLENT_SCROLL_NOTICE_ADD;
        }

        public override void bytesRead(ByteBuffer data)
        {
            ScrollNotice notice = new ScrollNotice();
            notice.bytesRead(data);
            ShowLobbyPanel.recvNoticeMsg(notice);
        }



    }
}
