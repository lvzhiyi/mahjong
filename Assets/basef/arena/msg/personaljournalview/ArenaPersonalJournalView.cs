using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary>
    /// 辅分改动界面
    /// </summary>
    public class ArenaPersonalJournalView : UnrealLuaSpaceObject
    {
        ArrayList<PersonalJournalEventRecord> list = new ArrayList<PersonalJournalEventRecord>();

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

        public void setData(PersonalJournalEventRecord[] obj)
        {
            if (list == null)
                list = new ArrayList<PersonalJournalEventRecord>();
            else
                list.clear();
            list.add(obj);
            //ismore = list.count == 50;
            xrefresh();
        }

        //private bool ismore = false;

        protected void updateScrollData(ScrollBar bars, int index)
        {
            ArenaPersonalJournalBar bar = (ArenaPersonalJournalBar)bars;
            bar.setData(list.get(index));
            bar.index_space = index;
            bar.refresh();
            //if (index + 1 == list.count && ismore)
            //{
            //    CommandManager.addCommand(new GetArenaPersonalJournalRecordCommand(), back);
            //}
        }

        private void back(object obj)
        {
            if (obj == null) return;
            PersonalJournalEventRecord[] res = (PersonalJournalEventRecord[])obj;
            //ismore = (res.Length == 50);
            list.add(res);
            container.incrResize(list.count);
        }
    }
}
