using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.rank
{
    public class GoldGroupRankListCommand:UserCommand
    {
        public GoldGroupRankListCommand()
        {
            this.id = CommandConst.PORT_GOLDRANK_GROUP_GET;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len=data.readInt();
            ArrayList<GoldRankGroup> list=new ArrayList<GoldRankGroup>();
            for (int i = 0; i < len; i++)
            {
                GoldRankGroup group=new GoldRankGroup();
                group.bytesRead(data);
                list.add(group);
            }
            callbackobj = list;
        }
    }
}
