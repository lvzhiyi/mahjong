using cambrian.unreal.display;
using System;
using UnityEngine;
using XLua;

/// <summary>
/// 只是地图拖动的辅助
/// </summary>
[Hotfix]
public class ScrollPanel : UnrealLuaPanel
{
    [HideInInspector] public Action<Vector3> callback;
    public bool draging
    {
        get
        {
            if(drag.draging) return true;
            return Time.time - this.drag.lasttime < 0.3f;
        }
    }
    Transform container;

    ScrollView drag;

    Vector3 lastposition;

    protected override void xinit()
    {
        base.xinit();
        this.container = this.transform.Find("Canvas").Find("container");
        this.lastposition = this.container.position;
        this.drag = this.transform.Find("Canvas").Find("drag").GetComponent<ScrollView>();
    }
    public void scaling()
    {
        this.lasttime = Time.time;
        this.drag.inertia = false;
    }
    float lasttime = 0;
    public void onScroll()
    {
        Vector3 delta = this.container.position - this.lastposition;
        if (this.drag.horizontal)
            delta.x = delta.x*1.6f;
        else delta.x = 0;
        if (!this.drag.vertical) delta.y = 0;
        this.lastposition = this.container.position;
        if (Time.time - this.lasttime < 0.5f) return;
        if (!this.drag.inertia) this.drag.inertia = true;
        this.callback(delta);
    }
}