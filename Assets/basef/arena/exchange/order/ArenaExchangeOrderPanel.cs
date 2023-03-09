using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    public class ArenaExchangeOrder
    {
        /// <summary> 未发货 </summary>
        public const int UnFilledOrder = 0;
        public const string UnFilledOrderText = "未发货";

        /// <summary> 已发货 </summary>
        public const int FilledOrder = 1;
        public const string FilledOrderText = "已发货";
        
        /// <summary> 已送达 </summary>
        public const int ReachedOrder = 2;
        public const string ReachedOrderText = "已送达";

        /// <summary> 已接收 </summary>
        public const int AlreadyReceivedOrder = 3;
        public const string AlreadyReceivedOrderText = "已接收";
    }

    /// <summary> 查看订单 界面 </summary>
    public class ArenaExchangeOrderPanel : UnrealLuaPanel
    {
        ScrollContainer container;

        ArrayList<ArenaExchangeOrderContentData> list = new ArrayList<ArenaExchangeOrderContentData>();

        protected override void xinit()
        {
            base.xinit();
            container = this.content.Find("centers").Find("container").GetComponent<ScrollContainer>();
            container.init();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            if (list == null || list.count == 0) return;
            container.updateData(updateScrollData);
            container.resize(list.count);
        }

        protected void updateScrollData(ScrollBar bars,int index)
        {
            ArenaExchangeOrderConteneBar bar = (ArenaExchangeOrderConteneBar)bars;
            bar.setData(list.get(index));
            bar.index_space = index;
            bar.refresh();
        }

        public void setData(object obj)
        {
            this.list = (ArrayList<ArenaExchangeOrderContentData>)obj;
        }
    }
}
