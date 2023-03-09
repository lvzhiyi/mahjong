using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{

    /// <summary> 获取商品内容 </summary>
    public class GetArenaExchangePhysicalCommand : UserCommand
    {
        public GetArenaExchangePhysicalCommand()
        {
            this.id = CommandConst.PORT_ARENA_AWARD_EXCHANGE_ITEM;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArrayList<ArenaExchangePhysicalGoodsData> list = new ArrayList<ArenaExchangePhysicalGoodsData>();
            ArenaExchangePhysicalGoodsData goods;
            for (int i = 0; i < len; i++)
            {
                int sid = data.readInt();
                goods = new ArenaExchangePhysicalGoodsData(sid);
                list.add(goods);
            }

            callbackobj = list;
        }
    }
}