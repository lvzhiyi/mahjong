using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaExchangeOrderConteneBar : ScrollBar
    {
        ArenaExchangeOrderContentData data;

        /// <summary> 商品图片 </summary>
        private Image img;

        /// <summary> 商品名字 </summary>
        private UnrealTextField shopname;

        /// <summary> 商品价格 </summary>
        private UnrealTextField price;

        /// <summary> 商品数量 </summary>
        private UnrealTextField num;

        /// <summary> 兑换状态 </summary>
        private UnrealTextField status;

        /// <summary> 兑换时间 </summary>
        private UnrealTextField etime;

        protected override void xinit()
        {
            img = this.transform.Find("img").GetComponent<Image>();

            shopname = this.transform.Find("shopname").GetComponent<UnrealTextField>();
            shopname.init();

            price = this.transform.Find("price").GetComponent<UnrealTextField>();
            price.init();

            num = this.transform.Find("num").GetComponent<UnrealTextField>();
            num.init();

            etime = this.transform.Find("time").GetComponent<UnrealTextField>();
            etime.init();

            status = this.transform.Find("status").GetComponent<UnrealTextField>();
            status.init();

        }

        protected override void xrefresh()
        {
            if (this.data == null) return;
            shopname.text = data.name.ToString();
            price.text = data.price.ToString();
            num.text = data.num.ToString();

            setStatus();
            setTime();
        }

        private void setStatus()
        {
            switch (data.status)
            {
                case ArenaExchangeOrder.UnFilledOrder:
                    status.text = ArenaExchangeOrder.UnFilledOrderText;
                    break;
                case ArenaExchangeOrder.FilledOrder:
                    status.text = ArenaExchangeOrder.FilledOrderText;
                    break;
                case ArenaExchangeOrder.ReachedOrder:
                    status.text = ArenaExchangeOrder.ReachedOrderText;
                    break;
                case ArenaExchangeOrder.AlreadyReceivedOrder:
                    status.text = ArenaExchangeOrder.AlreadyReceivedOrderText;
                    break;
            }
        }

        private void setTime()
        {
            long time = TimeKit.currentTimeMillis;
            string t = TimeKit.format(time,"yyyy-MM-dd hh:mm:ss");
            string yymmdd = t.Substring(0,10);
            string hhmmss = t.Substring(11,8);

            etime.text = yymmdd + "\n" + hhmmss;
        }

        public void setData(ArenaExchangeOrderContentData data)
        {
            this.data = data;
        }

    }
}
