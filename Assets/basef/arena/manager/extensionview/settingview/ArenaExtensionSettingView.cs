using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 推广设置
    /// </summary>
    public class ArenaExtensionSettingView:UnrealLuaSpaceObject
    {
        private ScrollContainer container;

        [HideInInspector] public RebateList list;

        protected override void xinit()
        {
            container = transform.Find("container").GetComponent<ScrollContainer>();
            container.init();
        }

        public void setData(RebateList list)
        {
            this.list = list;
        }

        protected override void xrefresh()
        {
            container.updateData(updateBack);
            container.resize(list.list.count);
        }

        public void updateBack(ScrollBar bar,int index)
        {
            ArenaExtensionSettingBar setting=(ArenaExtensionSettingBar) bar;
            setting.setData(list.list.get(index));
            setting.refresh();

        }
    }
}
