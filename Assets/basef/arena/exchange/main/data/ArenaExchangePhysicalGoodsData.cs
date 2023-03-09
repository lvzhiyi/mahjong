using basef.award;
using cambrian.common;

namespace basef.arena
{
    public class ArenaExchangePhysicalGoodsData : BytesSerializable
    {
        public int sid;

        /// <summary> 标题 </summary>
        public string title;

        /// <summary> 价格 人民币 </summary>
        public int price1;

        /// <summary> 价格 奖章 </summary>
        public int price2;

        /// <summary> 图片路径 </summary>
        public int imgindex;

        public ArenaExchangePhysicalGoodsData(int sid)
        {
            this.sid = sid;

            Award award = (Award) Sample.factory.newSample(sid);

            imgindex = int.Parse(award.img);

            title = award.getName();

            price1 = award.getToken();

            price2 = award.getMoney();
        }
    }
}