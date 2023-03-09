using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.notice
{
    public class ScrollNoticeCommand:UserCommand
    {
        public ScrollNoticeCommand()
        {
            this.id = CommandConst.POST_SCROLLNOTICE_LIST;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArrayList<ScrollNotice> list = new ArrayList<ScrollNotice>();
            for (int i = 0; i < len; i++)
            {
                ScrollNotice scrollnotice = new ScrollNotice();
                scrollnotice.bytesRead(data);
                list.add(scrollnotice);
            }

            callbackobj = list;

        }
    }
}
