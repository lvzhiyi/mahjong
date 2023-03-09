using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary>
    /// 辅分改动界面
    /// </summary>
    public class ArenaMsgAuxiliaryScoreView : UnrealLuaSpaceObject
    {

        ArrayList<AuxiliaryScoreEventRecord> list = new ArrayList<AuxiliaryScoreEventRecord>();

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

        public void setData(AuxiliaryScoreEventRecord[] obj)
        {
            if (list == null)
                list = new ArrayList<AuxiliaryScoreEventRecord>();
            else
                list.clear();
            list.add(obj);
            ismore = list.count == 50;
            xrefresh();
        }

        private bool ismore = false;

        protected void updateScrollData(ScrollBar bars, int index)
        {
            ArenaAuxiliaryScoreContentBar bar = (ArenaAuxiliaryScoreContentBar)bars;
            bar.setData(list.get(index));
            bar.index_space = index;
            bar.refresh();
            if (index + 1 == list.count && ismore)
            {
                CommandManager.addCommand(new GetArenaMsgAuxiliaryScoreRecordCommand(list.count), back);
            }
        }

        private void back(object obj)
        {
            if (obj == null) return;
            AuxiliaryScoreEventRecord[] res = (AuxiliaryScoreEventRecord[])obj;
            ismore = (res.Length == 50);
            list.add(res);
            container.incrResize(list.count);
        }
    }
}
