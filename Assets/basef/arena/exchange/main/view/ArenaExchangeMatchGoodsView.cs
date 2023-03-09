using cambrian.common;

namespace basef.arena
{
    /// <summary> 比赛奖励 界面 </summary>
    public class ArenaExchangeMatchGoodsView : UnrealLuaSpaceObject
    {
        UnrealTableContainer content;

        private ArrayList<ExchangeEntery> list;

        protected override void xinit()
        {
            content = this.transform.Find("scrollrect").Find("content").GetComponent<UnrealTableContainer>();
            content.init();
            this.list = new ArrayList<ExchangeEntery>();
        }

        protected override void xrefresh()
        {
            list.clear();
            //从小到大排序
            ExchangeEntery[] value = Arena.arena.exchangeSettings.list.toArray();
            for (int i = 0; i < value.Length; i++)
            {
                for (int j = 0; j < value.Length - i - 1; j++)
                {
                    if (value[j].getValue() > value[j + 1].getValue())
                    {
                        var temp = value[j];
                        value[j] = value[j + 1];
                        value[j + 1] = temp;
                    }
                }
            }
            this.list.add(value);
            content.resize(list.count);
            ArenaExchangeMatchContentBar bar = null;
            for (int i = 0; i < list.count; i++)
            {
                bar = (ArenaExchangeMatchContentBar)content.bars[i];
                bar.setData(list.get(i));
                bar.index_space = i;
                bar.refresh();
            }
            content.resizeDelta();
        }
    }
}
