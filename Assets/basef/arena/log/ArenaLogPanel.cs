using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 竞赛场日志面板
    /// </summary>
    public class ArenaLogPanel : UnrealLuaPanel
    {
        ScrollContainer container;

        ArrayList<ArenaLog> list = new ArrayList<ArenaLog>();

        [HideInInspector] public long minUid;

        protected override void xinit()
        {
            container = content.Find("content").Find("container").GetComponent<ScrollContainer>();
            container.init();
        }

        public void setData(ArenaLog[] obj)
        {
            minUid = 0;
            if (obj.Length != 0) minUid = obj[obj.Length - 1].uuid;
            if (list == null)
                list = new ArrayList<ArenaLog>();
            else
                list.clear();
            list.add(obj);
            ismore = list.count == 50;
        }

        protected override void xrefresh()
        {
            container.updateData(updateScrollData);
            container.resize(list.count);
        }

        private bool ismore = false;
        protected void updateScrollData(ScrollBar bars, int index)
        {
            ArenaLogBar bar = (ArenaLogBar)bars;
            bar.setData(list.get(index));
            bar.index_space = index;
            bar.refresh();
            if (index + 1 == list.count && ismore)
            {
                CommandManager.addCommand(new GetArenaLogCommand(minUid), back);
            }
        }

        public void back(object obj)
        {
            if (obj == null) return;
            ArenaLog[] res = (ArenaLog[])obj;
            minUid = res[res.Length - 1].uuid;
            ismore = (res.Length == 50);
            list.add(res);
            container.incrResize(list.count);
        }
    }
}


