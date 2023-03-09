using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary>
    /// 赛场奖励显示
    /// </summary>
    public class ArenaAwardView : UnrealLuaSpaceObject
    {
        private ScrollContainer container;

        protected override void xinit()
        {
            container = transform.Find("container").GetComponent<ScrollContainer>();
            container.init();
        }

        private InsertArrayList<ExchangeEntery> insert;

        protected override void xrefresh()
        {
            this.container.updateData(updateDatas);
            ArrayList<ExchangeEntery> exchange = Arena.arena.exchangeSettings.list;

            insert = new InsertArrayList<ExchangeEntery>(onlinesCompare);
            for (int i = 0; i < exchange.count; i++)
                insert.add(exchange.get(i));
            this.container.resize(insert.count);
        }

        public int onlinesCompare(ExchangeEntery a, ExchangeEntery b)
        {

            if (a.getValue() < b.getValue())
                return Comparator.COMP_GT;
            else
                return Comparator.COMP_LT;
        }


        public void updateDatas(ScrollBar bar, int index)
        {
            ArenaAwardBar awardBar = (ArenaAwardBar) bar;
            awardBar.setData(insert.get(index));
            awardBar.refresh();
        }
    }
}
