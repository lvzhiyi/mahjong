using cambrian.common;

namespace basef.arena
{
    /// <summary> 奖章明细 </summary>
    public class ArenaAccountsMedalPanel : UnrealLuaPanel
    {
        /// <summary> 顶部管理 </summary>
        ArenaAccountsMedalTopView topview;

        /// <summary> 内容管理 </summary>
        ArenaAccountsMedalContainerView contentview;

        public long userid;

        protected override void xinit()
        {
            base.xinit();
            topview = this.content.Find("centers").Find("top").GetComponent<ArenaAccountsMedalTopView>();
            topview.init();

            contentview = this.content.Find("centers").Find("container").GetComponent<ArenaAccountsMedalContainerView>();
            contentview.init();
        }

        protected override void xrefresh()
        {
            base.xrefresh();

            topview.refresh();
            contentview.refresh();
        }

        public void setData(ArrayList<ArenaMedalBill> obj,int medal)
        {
            contentview.setData(obj);
            topview.setData(medal);
        }

        public int getType()
        {
            switch (topview.typeindex)
            {
                case 1: return GetArenaAccountsMedalListCommand.TYPE_INCR_MEDAL;
                case 2: return GetArenaAccountsMedalListCommand.TYPE_ALL;
                default: return GetArenaAccountsMedalListCommand.TYPE_ALL;
            }
        }
    }
}
