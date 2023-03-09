using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.rank
{
    public class TeaRankListPanelCommand:UserCommand
    {
        public TeaRankListPanelCommand()
        {
            this.id = CommandConst.PORT_CLUB_ACTIVITY_RANK_LIST;
        }

        public override void bytesRead(ByteBuffer data)
        {
            TeaRankList list=new TeaRankList();
            list.bytesRead(data);
            callbackobj = list;
        }
    }
}
