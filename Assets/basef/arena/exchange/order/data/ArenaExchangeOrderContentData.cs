using cambrian.common;

namespace basef.arena
{
    public class ArenaExchangeOrderContentData : BytesSerializable
    {
        /// <summary> 商品图片下标 </summary>
        public int imgindex = 0;

        /// <summary> 商品名字 </summary>
        public string name = "";

        /// <summary> 商品价格 </summary>
        public int price = 0;

        /// <summary> 商品数量 </summary>
        public int num = 0;

        /// <summary> 兑换日期 </summary>
        public long time =0;

        /// <summary> 兑换状态 </summary>
        public int status = 0;

    }
}
