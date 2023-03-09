using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary>
    /// 同桌限制组面板
    /// </summary>
    public class ArenaLimitDeskPanel:UnrealLuaPanel
    {

        ScrollContainer container;

        private ArenaMutex[] mutexs;

        protected override void xinit()
        {
            base.xinit();
            container = content.transform.Find("content").Find("container").GetComponent<ScrollContainer>();
            container.init();
        }

        public void setData(ArenaMutex[] mutexs)
        {
            this.mutexs = mutexs;
        }

        protected override void xrefresh()
        {
            container.updateData(updatedata);
            container.resize(mutexs.Length);
        }


        public void updatedata(ScrollBar bar,int index)
        {
            ArenaLimitBar limit=(ArenaLimitBar)bar;
            limit.index_space = index;
            limit.setData(mutexs[index]);
            limit.refresh();
        }
    }
}
