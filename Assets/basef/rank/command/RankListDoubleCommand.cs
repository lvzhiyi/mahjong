using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.rank
{
    public class RankListDoubleCommand:UserCommand
    {
        public RankListDoubleCommand()
        {
            this.id = CommandConst.PORT_RANK_LIST_DOUBLE_WEEK;
        }

        public override void bytesRead(ByteBuffer data)
        {
            RankList list = new RankList();
            list.bytesRead(data);
            list.setJueShu(data.readLong());
            callbackobj = list;
        }
    }
}
