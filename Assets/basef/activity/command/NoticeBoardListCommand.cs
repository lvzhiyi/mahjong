using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.activity
{
    /// <summary> 获取公告列表 </summary>
    public class NoticeBoardListCommand : UserCommand
    {
        public NoticeBoardListCommand()
        {
            this.id = CommandConst.PORT_NOTICEBOARD_LIST;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArrayList<NoticeBoard> list = new ArrayList<NoticeBoard>();
            for (int i = 0; i < len; i++)
            {
                NoticeBoard notice = new NoticeBoard();
                notice.bytesRead(data);
                list.add(notice);
            }
            callbackobj = list;
        }
    }
}
