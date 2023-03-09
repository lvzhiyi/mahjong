using basef.player;
using cambrian.common;
using cambrian.game;
using cambrian.uui;

namespace basef.activity
{
    /// <summary> 打开公告面板 </summary>
    public class OpenNoticeBoardPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            //CommandManager.addCommand(new NoticeBoardListCommand(), back);
            Loader.getLoader().loadBytes(ServerInfos.server.getnoticeUrl(), noticback);
        }

        public void noticback(byte[] data)
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
            NoticeBoardPanel panel = UnrealRoot.root.getDisplayObject<NoticeBoardPanel>();
            panel.setData(list);
            panel.refresh();
            panel.setCVisible(true);
        }

        //public void back(object obj)
        //{
        //    if (obj == null) return;
        //    ArrayList<NoticeBoard> list = (ArrayList<NoticeBoard>)obj;
        //    removegonggao(list);
        //    NoticeBoardPanel panel = UnrealRoot.root.getDisplayObject<NoticeBoardPanel>();
        //    panel.setData(list);
        //    panel.refresh();
        //    panel.setCVisible(true);
        //}


        public void removegonggao(ArrayList<NoticeBoard> list)
        {
            long currtime = TimeKit.currentTimeMillis;
            for (int i = list.count - 1; i >= 0; i--)
            {
                NoticeBoard board = list.get(i);
                if (board.getReleaseTime() > TimeKit.currentTimeMillis ||
                    (board.getRemoveTime() != 0 && TimeKit.currentTimeMillis > board.getRemoveTime()))
                {
                    list.removeAt(i);
                }
            }
        }
    }
}
