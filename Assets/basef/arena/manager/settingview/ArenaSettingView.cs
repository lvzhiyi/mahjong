using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 设置视图
    /// </summary>
    public class ArenaSettingView:UnrealLuaSpaceObject
    {
        [HideInInspector] public UnrealInputTextField areanname;

        [HideInInspector] public UnrealInputTextField notice;

        [HideInInspector] public ArenaSettingOtherView other;

        protected override void xinit()
        {
            areanname=transform.Find("name").GetComponent<UnrealInputTextField>();
            areanname.init();
            notice = transform.Find("notice").GetComponent<UnrealInputTextField>();
            notice.init();
            other = transform.Find("other").GetComponent<ArenaSettingOtherView>();
            other.init();
        }

        protected override void xrefresh()
        {
            areanname.text = Arena.arena.getName();
            notice.text = Arena.arena.getNotice();
            other.setData();
            other.refresh();
        }
    }
}
