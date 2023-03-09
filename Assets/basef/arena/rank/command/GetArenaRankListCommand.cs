using basef.rank;
using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 获取竞赛场排行榜数据
    /// </summary>
    public class GetArenaRankListCommand:UserCommand
    {
        private long arenaid;

        private int type;

        public GetArenaRankListCommand(long arenaid,int type)
        {
            id = CommandConst.PORT_ARENA_RANK_LIST_GET;
            this.arenaid = arenaid;
            this.type = type;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeInt(type);
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
