using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XLua;

[Hotfix]
public class ScrollView : ScrollRect
{
    [HideInInspector] public bool draging = false;
    [HideInInspector] public float lasttime = 0;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        this.draging = true;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        this.draging = false;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        this.lasttime = Time.time;
    }
}