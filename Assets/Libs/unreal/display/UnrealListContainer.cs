using System;
using UnityEngine;
using XLua;

/// <summary>
/// 列表容器
/// </summary>
[Hotfix]
public class UnrealListContainer : UnrealContainer
{
    /// <summary>
    /// 显示数量
    /// </summary>
    int _size = 1;

    /// <summary>
    /// 容量
    /// </summary>
    int capacity = 1;

    /// <summary>
    /// 顶部介绍
    /// </summary>
    UnrealLuaSpaceObject info;
    /// <summary>
    /// 底部
    /// </summary>
    Transform more;

    /// <summary>
    /// 包含的
    /// </summary>
    [HideInInspector] public UnrealLuaSpaceObject[] bars;


    public int size
    {
        get { return this._size; }
    }

    public override float getHeight()
    {
        float ch = 0;
        for (int i = 0; i < this.size; i++)
        {
            ch += this.bars[i].getHeight();
        }
        if (this.info != null && this.info.getVisible())
        {
            ch += this.info.getHeight();
        }
        if (this.more != null)
        {
            ch += this.more.GetComponent<UnrealLuaSpaceObject>().getHeight();
        }
        return ch;
    }

    public override float getWidth()
    {
        return this.bars[0].getWidth();
    }

    public override void init()
    {
        base.init();
        if (this.bars == null || this.bars.Length == 0)
        {
            this.bars = new UnrealLuaSpaceObject[this.capacity];
            for (int i = 1; i < this.capacity; i++)
            {
                string str = "bar_" + i;
                Transform tran = this.transform.Find(str);
                if (tran != null) continue;
                GameObject obj = this.cloneAdd(this.transform.Find("bar_0").gameObject);
                obj.name = str;
            }
        }
        for (int i = 0; i < this.capacity; i++)
        {
            this.bars[i] = this.transform.Find("bar_" + i).GetComponent<UnrealLuaSpaceObject>();
            this.bars[i].init();
        }
        if (this.transform.Find("info") != null)
        {
            this.info = this.transform.Find("info").GetComponent<UnrealLuaSpaceObject>();
        }
        this.more = this.transform.Find("more");
        this.resize(this.size);
    }

    public void setMoreVisible(bool visible)
    {
        if(this.more==null) return;
        this.more.gameObject.SetActive(visible);
    }

    public override void relayout()
    {
        float ly = this.bars[0].transform.localPosition.y;
        for (int i = 0; i < this.size; i++)
        {
            DisplayKit.setLocalY(this.bars[i].gameObject, ly);
            ly -= this.bars[i].getHeight();
            this.bars[i].relayout();
        }
        if (this.more != null)
            DisplayKit.setLocalY(this.more.gameObject, ly + this.bars[0].GetComponent<RectTransform>().rect.height / 2 - this.more.GetComponent<RectTransform>().rect.height / 2);
    }
    public override void resizeDelta()
    {
        if (this.transform is RectTransform)
        {
            float h = 0;
            for (int i = 0; i < this.size; i++)
            {
                h += this.bars[i].getHeight();
            }

            if (this.more != null)
                h += this.more.GetComponent<RectTransform>().rect.height;
            if (this.info != null)
                h += this.info.GetComponent<RectTransform>().rect.height;
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

    public void refresh<R, T>(T[] entities, Action<int,int, R, T> action) where R : UnrealLuaSpaceObject
    {
        this.refresh(entities, action, 0);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="action"></param>
    /// <param name="addtional">显示空白的</param>
    public void refresh<R, T>(T[] entities, Action<int,int, R, T> action, int addtional) where R : UnrealLuaSpaceObject
    {
        int i = 0;
        for (int j = 0; j < entities.Length; j++)
        {
            if (entities[j] == null) continue;
            this.increaseCapacity(i + 1);
            action(i,j, (R)this.bars[i], entities[j]);
            i++;
        }
        for (int j = 0; j < addtional; j++)
        {
            this.increaseCapacity(i + 1);
            action(i,-1, (R)this.bars[i], default(T));
            i++;
        }
        this.resize(i);
    }
    /// <summary>
    /// 设置可见数量
    /// </summary>
    /// <param name="size"></param>
    public void resize(int size)
    {
        this._size = size;
        if (size > this.capacity) this.increaseCapacity(size);
        for (int i = 0; i < this.bars.Length; i++)
        {
            this.bars[i].gameObject.SetActive(i < size);
        }
        this.relayout();
    }

    /// <summary>
    /// 增加容量到指定值,小于当前容量则直接返回
    /// </summary>
    public void increaseCapacity(int capacity)
    {
        if (this.capacity >= capacity) return;
        UnrealLuaSpaceObject[] bars = new UnrealLuaSpaceObject[capacity];
        bars = new UnrealLuaSpaceObject[capacity];
        for (int i = this.capacity; i < capacity; i++)
        {
            string str = "bar_" + i;
            Transform tran = this.transform.Find(str);
            if (tran != null) continue;
            GameObject obj = this.cloneAdd(this.transform.Find("bar_0").gameObject);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.name = str;
        }
        for (int i = 0; i < capacity; i++)
        {
            bars[i] = this.transform.Find("bar_" + i).GetComponent<UnrealLuaSpaceObject>();
            bars[i].init();
        }
        this.bars = bars;
        this.capacity = capacity;
    }
}