using cambrian.common;
using UnityEngine;
using XLua;

/// <summary>
/// 子空间对象顺序排列的容器
/// </summary>
[Hotfix]
public class UnrealIndexContainer : UnrealContainer
{

    ArrayList<UnrealLuaSpaceObject> childs;

    public override void init()
    {
        base.init();
        this.childs = new ArrayList<UnrealLuaSpaceObject>();
        foreach (Transform tran in this.transform)
        {
            UnrealLuaSpaceObject obj = tran.GetComponent<UnrealLuaSpaceObject>();
            if (obj == null) continue;
            bool b = false;
            for (int i = 0; i < this.childs.count; i++)
            {
                if (obj.transform.localPosition.y > this.childs.get(i).transform.localPosition.y)
                {
                    this.childs.addAt(obj, i);
                    b = true;
                    break;
                }
            }
            if (b) continue;
            this.childs.add(obj);
        }
    }
    public int size
    {
        get { return this.childs.count; }
    }
    public override float getHeight()
    {
        float ch = 0;
        for (int i = 0; i < this.size; i++)
        {
            if (this.childs.get(i).getVisible())
                ch += this.childs.get(i).getHeight();
        }
        return ch;
    }

    public override void relayout()
    {
        float ly = this.childs.get(0).transform.localPosition.y + this.childs.get(0).getHeight()/2;
        float value = 0;

        for (int i = 0; i < this.childs.count; i++)
        {
            UnrealLuaSpaceObject child = this.childs.get(i);
            if (!child.getVisible()) continue;
            DisplayKit.setLocalY(child, ly + value - child.getHeight()/2);
            value -= child.getHeight();
            child.relayout();
        }
    }

    public override void resizeDelta()
    {
        if (this.transform is RectTransform)
        {
            float h = 0;
            for (int i = 0; i < this.size; i++)
            {
                if (this.childs.get(i).getVisible())
                    h += this.childs.get(i).getHeight();
            }
            if (this.drag != null)
            {
                float sh = this.drag.GetComponent<RectTransform>().rect.height;
                if (sh > h) h = sh;
            }
            RectTransform rectTransform = this.GetComponent<RectTransform>();
            Vector2 vector = rectTransform.sizeDelta;
            float offset = (vector.y - h)/2;
            vector.y = h;
            rectTransform.sizeDelta = vector;

            float y = this.transform.localPosition.y;
            DisplayKit.setLocalY(this.gameObject, y + offset);
        }
    }
}