using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.rank
{
    public class RankListNowCommand: UserCommand
    {
        public RankListNowCommand()
        {
            this.id = CommandConst.PORT_RANK_LIST_NOW;
        }

        public override void bytesRead(ByteBuffer data)
        {
            RankList list=new RankList();
            list.bytesRead(data);
            list.setJueShu(data.readLong());
            callbackobj = list;
        }
    }
}
