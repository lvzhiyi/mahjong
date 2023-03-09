using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    public class ArenaMsgJoin
    {
        /// <summary> 邀请加入 </summary>
        public const int Join_Invite = 0;
    }

    /// <summary> 赛场消息 加入申请 界面 </summary>
    public class ArenaMsgJoinView : UnrealLuaSpaceObject
    {
        ArrayList<ArenaMsgJoinContentData> list = new ArrayList<ArenaMsgJoinContentData>();

        ScrollContainer container;

        protected override void xinit()
        {
            container = this.transform.Find("container").GetComponent<ScrollContainer>();
            container.init();
        }

        protected override void xrefresh()
        {
            if (list == null || list.count == 0) return;

            container.updateData(updateScrollData);
            container.resize(list.count);
        }

        protected void updateScrollData(ScrollBar bars,int index)
        {
            ArenaMsgJoinContentBar bar = (ArenaMsgJoinContentBar)bars;
            bar.setData(list.get(index));
            bar.index_space = index;
            bar.refresh();
        }

        public void setData(ArrayList<ArenaMsgJoinContentData> obj = null)
        {
            this.list = obj;
            xrefresh();
        }

        public ArrayList<ArenaMsgJoinContentData> getList()
        {
            return list;
        }
    }
}