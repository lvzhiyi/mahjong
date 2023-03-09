using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    public class ArenaExchangeRecord
    {
        /// <summary> 兑换成功 </summary>
        public const int Succeed = 1;
        public const string SucceedText = "兑换成功";

        /// <summary> 兑换中 </summary>
        public const int Underway = 0;
        public const string UnderwayText = "兑换中";

        /// <summary> 兑换失败 </summary>
        public const int Fail = -1;
        public const string FailText = "兑换失败";
    }

    /// <summary> 兑换记录 面板 </summary>
    public class ArenaExchangeRecordPanel : UnrealLuaPanel
    {
        ScrollContainer container;

        ArrayList<ArenaExchangeRecordContentData> list = new ArrayList<ArenaExchangeRecordContentData>();

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
            ArenaExchangeRecordConteneBar bar = (ArenaExchangeRecordConteneBar)bars;
            bar.setData(list.get(index));
            bar.index_space = index;
            bar.refresh();
        }

        public void setData(object obj)
        {
            this.list = (ArrayList<ArenaExchangeRecordContentData>)obj;
        }
    }
}
