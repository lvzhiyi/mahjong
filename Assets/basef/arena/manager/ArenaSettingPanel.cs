using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 竞赛场统计界面
    /// </summary>
    public class ArenaSettingPanel : UnrealLuaPanel
    {

        /// <summary>
        /// 赛场设置
        /// </summary>
        [HideInInspector] public ArenaSettingView baseSettingView;

        protected override void xinit()
        {
            baseSettingView = this.content.Find("content").Find("view").Find("content").Find("mask").Find("setting").GetComponent<ArenaSettingView>();
            baseSettingView.init();
        }

        protected override void xrefresh()
        {
            baseSettingView.refresh();
        }
    }
}
