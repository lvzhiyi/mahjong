using basef.player;
using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 竞赛场 切换界面 </summary>
    public class ArenaCutPanel : UnrealLuaPanel
    {
        private ArrayList<Arena> list;

        ScrollContainer container;

        UnrealTextScaleButton createBtn;

        protected override void xinit()
        {
            base.xinit();
            container = this.content.Find("centers").Find("content").Find("container").GetComponent<ScrollContainer>();
            container.init();

            createBtn = this.content.Find("centers").Find("down").Find("createArena").GetComponent<UnrealTextScaleButton>();
            createBtn.init();
        }

        public void setData(ArrayList<Arena> list)
        {
            this.list = new ArrayList<Arena>();

            for (int i = 0; i < list.count; i++)
            {
                if (list.get(i).getMaster()== Player.player.userid)
                    this.list.add(list.get(i));
            }

            for (int i = 0; i < list.count; i++)
            {
                if (list.get(i).getMaster() != Player.player.userid)
                    this.list.add(list.get(i));
            }
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            createBtn.setVisible(PlayerToken.instance.isPromoter());
            container.updateData(updateScrollData);
            container.resize(list.count);
            container.resizeDelta();
        }

        protected void updateScrollData(ScrollBar bars,int index)
        {
            ArenaCutContentBar bar = (ArenaCutContentBar)bars;
            Arena arena= list.get(index);
            bar.setData(arena,arena.getId()==Arena.arena.getId());
            bar.index_space = index;
            bar.refresh();
        }
    }
}
