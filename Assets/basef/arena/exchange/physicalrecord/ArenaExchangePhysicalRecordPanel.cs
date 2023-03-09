using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 商品兑换记录 面板 </summary>
    public class ArenaExchangePhysicalRecordPanel : UnrealLuaPanel
    {
        ScrollContainer container;

        ArrayList<ArenaExchangePhysicalRecordData> list;

        int listlength = 50;

        bool ismore;

        protected override void xinit()
        {
            list = new ArrayList<ArenaExchangePhysicalRecordData>();

            base.xinit();
            container = this.content.Find("centers").Find("container").GetComponent<ScrollContainer>();
            container.init();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            container.updateData(updateScrollData);
            container.resize(list.count);
        }

        protected void updateScrollData(ScrollBar bars,int index)
        {
            ArenaExchangePhysicalRecordConteneBar bar = (ArenaExchangePhysicalRecordConteneBar)bars;
            bar.setData(list.get(index));
            bar.index_space = index;
            bar.refresh();
            ismore = (list.count == listlength);
            if (index + 1 == list.count && ismore)
            {
                CommandManager.addCommand(new GetArenaExchangePhysicalRecordCommand(list.getLast().uuid),back);
            }
        }

        public void setData(object obj)
        {
            this.list = (ArrayList<ArenaExchangePhysicalRecordData>)obj;
            ismore = (list.count == listlength);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            ArrayList<ArenaExchangePhysicalRecordData> ll = (ArrayList<ArenaExchangePhysicalRecordData>)obj;
            ismore = (ll.count == listlength);
            list.add(ll.toArray());
        }
    }
}
