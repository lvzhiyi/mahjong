using cambrian.common;

namespace basef.arena
{
    /// <summary> 实物奖励 界面 </summary>
    public class ArenaExchangePhysicalGoodsView : UnrealLuaSpaceObject
    {
        UnrealTableContainer content;

        ArrayList<ArenaExchangePhysicalGoodsData> list;

        protected override void xinit()
        {
            list = new ArrayList<ArenaExchangePhysicalGoodsData>();

            content = this.transform.Find("scrollrect").Find("content").GetComponent<UnrealTableContainer>();
            content.init();
        }

        protected override void xrefresh()
        {
            content.resize(list.count);
            ArenaExchangePhysicalContentBar bar;
            for (int i = 0; i < list.count; i++)
            {
                bar = (ArenaExchangePhysicalContentBar)content.bars[i];
                bar.setData(list.get(i));
                bar.index_space = i;
                bar.refresh();
            }
            content.resizeDelta();
        }

        public void setData(ArrayList<ArenaExchangePhysicalGoodsData> list)
        {
            this.list = list;
        }
    }
}
