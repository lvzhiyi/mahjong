using basef.rule;
using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 竞赛场统计界面
    /// </summary>
    public class ArenaWfNextPanel : UnrealLuaPanel
    {

        ArenaLockRule rule;

        /// <summary>
        /// 防沉迷视图
        /// </summary>
        [HideInInspector] public ArenaIndulgeView indulgeView;

        protected override void xinit()
        {
            indulgeView = content.Find("content").Find("view").Find("othersetting").GetComponent<ArenaIndulgeView>();
            indulgeView.init();
        }

        public void setData(ArenaLockRule rule)
        {
            this.rule = rule;
        }

        protected override void xrefresh()
        {
            indulgeView.setData(rule);
            indulgeView.refresh();
        }
    }
}
