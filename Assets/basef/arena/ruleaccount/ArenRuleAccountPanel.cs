using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary>
    /// 推广员规则结算面板
    /// </summary>
    public class ArenRuleAccountPanel : UnrealLuaPanel
    {
        private ScrollTableContainer container;

        public ArrayList<ArenaRuleAccountcs> list;

        public long dest;

        protected override void xinit()
        {
            base.xinit();
            container = content.Find("content").Find("container").GetComponent<ScrollTableContainer>();
            container.init();
        }

        public void setRebate(ArrayList<ArenaRuleAccountcs> list, long dest)
        {
            this.list = list;
            this.dest = dest;
        }

        protected override void xrefresh()
        {
            container.updateData(back);
            container.resize(list.count);
        }

        public void back(ScorllTableBar bar,int index)
        {
            ArenRuleAccountBar radio =(ArenRuleAccountBar)bar;
            radio.setData(list.get(index));
            radio.index_space = index;
            radio.refresh();
        }
    }
}
