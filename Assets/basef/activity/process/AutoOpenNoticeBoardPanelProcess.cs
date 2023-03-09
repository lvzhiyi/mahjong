using basef.lobby;
using basef.player;
using cambrian.common;
using cambrian.game;
using cambrian.uui;
using platform;

namespace basef.activity
{
    /// <summary> 自动打开活动界面</summary>
    public class AutoOpenNoticeBoardPanelProcess : Process
    {
        public override void execute()
        {
            //CommandManager.addCommand(new NoticeBoardListCommand(), callback);
            Loader.getLoader().loadBytes(ServerInfos.server.getnoticeUrl(), callback);
        }

        public void callback(byte[] data)
        {
            if (data == null)
            {
                return;
            }
            ByteBuffer buffer = new ByteBuffer(data);

            int status = buffer.readShort();
            if (status != 0) return;
            int len = buffer.readInt();
            ArrayList<NoticeBoard> list = new ArrayList<NoticeBoard>();
            for (int i = 0; i < len; i++)
            {
                NoticeBoard notice = new NoticeBoard();
                notice.bytesRead(buffer);
                if (notice.status == 4 && PlayerToken.instance.isPromoter())
                    list.add(notice);
                else if (notice.status != 4)
                {
                    list.add(notice);
                }
            }

            removegonggao(list);
            if (list.count == 0) return;

            if (Room.room != null || ShowLobbyPanel.checkDisplayObject() == null)
                return;
            NoticeBoardPanel boardPanel = UnrealRoot.root.getDisplayObject<NoticeBoardPanel>();
            boardPanel.setData(list);
            boardPanel.refresh();
            boardPanel.setCVisible(true);
        }



        public void removegonggao(ArrayList<NoticeBoard> list)
        {
            long currtime = TimeKit.currentTimeMillis;
            for (int i = list.count - 1; i >= 0; i--)
            {
                NoticeBoard board = list.get(i);
                if (board.getReleaseTime() > TimeKit.currentTimeMillis ||
                    (board.getRemoveTime()!=0&&TimeKit.currentTimeMillis > board.getRemoveTime()))
                {
                    list.removeAt(i);
                }
            }
        }
    }
}
