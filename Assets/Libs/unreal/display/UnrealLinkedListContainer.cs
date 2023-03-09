
using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
public class UnrealLinkedListContainer : UnrealContainer
{
    /// <summary>
    /// 布局方式。横向、纵向
    /// </summary>
    public Layout layout = Layout.Height;
    /// <summary>
    /// 缓存,用于重复使用
    /// </summary>
    LinkedList<UnrealLuaSpaceObject> caches;
    /// <summary>
    /// 显示的
    /// </summary>
    LinkedList<UnrealLuaSpaceObject> bars;
    /// <summary>
    /// 顶部介绍
    /// </summary>
    Transform info;
    /// <summary>
    /// 更多
    /// </summary>
    Transform more;
    /// <summary>
    /// 游标,位置
    /// </summary>
    Transform cursor;

    public override void init()
    {
        base.init();
        this.caches = new LinkedList<UnrealLuaSpaceObject>();
        this.bars = new LinkedList<UnrealLuaSpaceObject>();
        this.cursor = this.transform.Find("cursor");
        this.info = this.transform.Find("info");
        this.more = this.transform.Find("more");
    }

    public int size
    {
        get { return this.bars.Count; }
    }
    public override float getHeight()
    {
        float ch = 0;
        foreach (UnrealLuaSpaceObject bar in this.bars)
        {
            ch += bar.getHeight();
        }
        if (this.info != null)
        {
            ch += this.info.GetComponent<UnrealLuaSpaceObject>().getHeight();
        }
        if (this.more != null)
        {
            ch += this.more.GetComponent<UnrealLuaSpaceObject>().getHeight();
        }
        return ch;
    }
    protected float getCursorX()
    {
        return this.cursor.localPosition.x;
    }

    protected float getCursorY()
    {
        return this.cursor.localPosition.y;
    }


    /// <summary>
    /// 取出一个未使用的或拷贝一个
    /// </summary>
    /// <returns></returns>
    public UnrealLuaSpaceObject takeout(UnrealLuaSpaceObject source, Func<UnrealLuaSpaceObject, UnrealLuaSpaceObject, bool> func)
    {
        UnrealLuaSpaceObject bar = null;
        if (this.caches.Count > 0)
        {
            LinkedListNode<UnrealLuaSpaceObject> node = this.caches.First;
            while (node != null)
            {
                bool b = func(source, node.Value);
                if (b)
                {
                    bar = node.Value;
                    this.caches.Remove(bar);
                    break;
                }
                node = node.Next;
            }
        }
        if (bar == null)
        {
            bar = this.cloneAddBar(source);
            bar.init();
        }
        return bar;
    }
    /// <summary>
    /// 添加到头
    /// </summary>
    /// <param name="bar"></param>
    public void addFirst(UnrealLuaSpaceObject bar)
    {
        bar.gameObject.name = "bar_" + this.size;
        this.bars.AddFirst(bar);
        bar.setVisible(true);
    }
    /// <summary>
    /// 添加到尾
    /// </summary>
    /// <param name="bar"></param>
    public void addLast(UnrealLuaSpaceObject bar)
    {
        bar.gameObject.name = "bar_" + this.size;
        this.bars.AddLast(bar);
        bar.setVisible(true);
    }

    UnrealLuaSpaceObject cloneAddBar(UnrealLuaSpaceObject bar)
    {
        GameObject obj = this.cloneAdd(bar.gameObject);
        bar.gameObject.name = "cache_bar_" + this.size;
        obj.transform.localPosition = this.cursor.localPosition;
        obj.transform.localScale = this.cursor.localScale;
        obj.transform.localEulerAngles = this.cursor.localEulerAngles;
        return obj.GetComponent<UnrealLuaSpaceObject>();
    }

    /// <summary>
    /// 重置
    /// </summary>
    public void reset()
    {
        foreach (UnrealLuaSpaceObject bar in this.bars)
        {
            bar.gameObject.name = "cache_" + bar.gameObject.name;
            bar.setVisible(false);
            this.caches.AddLast(bar);
        }
        this.bars.Clear();
    }
    /// <summary>
    /// 设置布局
    /// </summary>
    public override void relayout()
    {
        if (this.layout == Layout.Height)
        {
            float ly = this.getCursorY();
            LinkedListNode<UnrealLuaSpaceObject> node = this.bars.First;
            while (node != null)
            {
                DisplayKit.setLocalY(node.Value, ly);
                ly -= node.Value.getHeight();
                node = node.Next;
            }
        }
        else
        {
            float lx = this.getCursorX();
            LinkedListNode<UnrealLuaSpaceObject> node = this.bars.First;
            while (node != null)
            {
                DisplayKit.setLocalX(node.Value, lx);
                lx += node.Value.getWidth();
                node = node.Next;
            }
        }
    }
}

