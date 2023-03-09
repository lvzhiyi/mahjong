using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 竞赛场 导入茶馆界面 </summary>
    public class ArenaImprotTeahousePanel : UnrealLuaPanel
    {
        /// <summary> 内容刷新容器 </summary>
        ScrollContainer container;

        /// <summary> 数据 </summary>
        ArrayList<ArenaImprotTeahouseData> list;

        protected override void xinit()
        {
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

        private void updateScrollData(ScrollBar bars,int index)
        {
            ArenaImprotTeahouseContentBar bar = (ArenaImprotTeahouseContentBar)bars;
            bar.setData(list.get(index));
            bar.index_space = index;
            bar.refresh();
        }

        public void setData(ArrayList<ArenaImprotTeahouseData> list)
        {
            this.list = list;
        }
    }
}
