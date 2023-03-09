using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 赛场消息 改动界面 界面 </summary>
    public class ArenaMsgChangeView : UnrealLuaSpaceObject
    {
        ArrayList<ArenaMemberChangeRecord> list = new ArrayList<ArenaMemberChangeRecord>();

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

        public void setData(ArenaMemberChangeRecord[] obj)
        {
            if(list==null)
                list=new ArrayList<ArenaMemberChangeRecord>();
            else
                list.clear();
            list.add(obj);
            ismore = list.count == 50;
            xrefresh();
        }

        private bool ismore = false;

        protected void updateScrollData(ScrollBar bars, int index)
        {
            ArenaMsgChangeContentBar bar = (ArenaMsgChangeContentBar)bars;
            bar.setData(list.get(index));
            bar.index_space = index;
            bar.refresh();
            if (index + 1 == list.count && ismore)
            {
               CommandManager.addCommand(new GetArenaMemeberChangeRecordCommand(list.count),back);
            }
        }

        public void back(object obj)
        {
            if (obj == null) return;
            ArenaMemberChangeRecord[] res= (ArenaMemberChangeRecord[])obj;
            ismore = (res.Length == 50);
            list.add(res);
            container.incrResize(list.count);
        }
    }
}
