using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 赛场消息 房间超时 界面 </summary>
    public class ArenaMsgTimeOutView : UnrealLuaSpaceObject
    {
        ArrayList<ArenaRoomEvent> list = new ArrayList<ArenaRoomEvent>();

        ScrollContainer container;


        protected override void xinit()
        {
            container = this.transform.Find("container").GetComponent<ScrollContainer>();
            container.init();
        }

        protected override void xrefresh()
        {
            container.updateData(updateScrollData);
            container.resize(list.count);
        }

        protected void updateScrollData(ScrollBar bars,int index)
        {
            ArenaMsgTimeOutContentBar bar = (ArenaMsgTimeOutContentBar)bars;
            bar.setData(list.get(index));
            bar.index_space = index;
            bar.refresh();
        }

        public void setData(ArrayList<ArenaRoomEvent> obj)
        {
            if (list == null)
                list = new ArrayList<ArenaRoomEvent>();
            else
                list.clear();
            list.add(obj.toArray());
            xrefresh();
        }

        public ArrayList<ArenaRoomEvent> getList()
        {
            return list;
        }
    }
}
