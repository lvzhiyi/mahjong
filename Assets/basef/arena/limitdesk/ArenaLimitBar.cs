using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.arena
{
    public class ArenaLimitBar: ScrollBar
    {
        UnrealTableContainer container;

        [HideInInspector] public ArenaMutex mutex;

        private UnrealTextField id;

        protected override void xinit()
        {
            container = transform.Find("container").GetComponent<UnrealTableContainer>();
            container.init();
            id = transform.Find("id").GetComponent<UnrealTextField>();
            id.init();
        }

        public void setData(ArenaMutex mutex)
        {
            this.mutex = mutex;
        }

        protected override void xrefresh()
        {

            id.text = index_space+1 + "";

            int cols = 6;
            if (mutex.getList().count < 6)
                cols = mutex.getList().count;
            container.cols = cols;
            container.resize(cols);

            for (int i = 0; i < cols; i++)
            {
                AreanLimitMemberBar bar=(AreanLimitMemberBar) container.bars[i];
                bar.setData(mutex.getList().get(i));
                bar.refresh();
            }
            container.resizeDelta();
        }
    }
}
