using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 商品兑换记录 </summary>
    public class ArenaExchangePhysicalRecordConteneBar : ScrollBar
    {
        ArenaExchangePhysicalRecordData data;

        /// <summary> 商品名字 </summary>
        private UnrealTextField goodsname;

        /// <summary> 商品价格 </summary>
        private UnrealTextField goodsprice;

        /// <summary> 兑换状态 </summary>
        private UnrealTextField status;

        /// <summary> 兑换时间 </summary>
        private UnrealTextField etime;

        protected override void xinit()
        {
            goodsname = this.transform.Find("title_1").GetComponent<UnrealTextField>();
            goodsname.init();

            goodsprice = this.transform.Find("title_2").GetComponent<UnrealTextField>();
            goodsprice.init();

            status = this.transform.Find("title_3").GetComponent<UnrealTextField>();
            status.init();

            etime = this.transform.Find("title_4").GetComponent<UnrealTextField>();
            etime.init();
        }

        protected override void xrefresh()
        {
            if (this.data == null) return;
            goodsname.text = data.title;
            goodsprice.text = data.price;
            etime.text = TimeKit.format(data.time,"yyyy-MM-dd HH:mm:ss");
            setStatus();
        }

        private void setStatus()
        {
            switch (data.state)
            {
                case ArenaExchangePhysicalRecordData.SUCCESS:
                    status.text = "已发货";
                    break;
                case ArenaExchangePhysicalRecordData.FAILURE:
                    status.text = "已收货";
                    break;
                case ArenaExchangePhysicalRecordData.UNDO:
                    status.text = "未发货";
                    break;
            }
        }

        public void setData(ArenaExchangePhysicalRecordData data)
        {
            this.data = data;
        }
    }
}
