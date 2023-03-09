using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.rank
{
    public class GoldRankListCommand:UserCommand
    {
        private int type;
        public GoldRankListCommand(int type)
        {
            this.id = CommandConst.PORT_GOLDRANK_GET;
            this.type = type;
        }

        public override void bytesWrite(ByteBuffer data)
        {
           data.writeInt(type);
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
