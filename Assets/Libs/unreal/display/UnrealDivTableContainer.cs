using UnityEngine;
using XLua;

/// <summary>
/// 不定表格容器,根据子对象位置及大小排列
/// </summary>
[Hotfix]
public class UnrealDivTableContainer : UnrealContainer
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
    /// 包含的
    /// </summary>
    [HideInInspector] public UnrealDisplayObject[] bars;

    public int size
    {
        get { return this._size; }
    }

    public UnrealDisplayObject getLast(int indexoflast = 0)
    {
        return bars[size - 1 - indexoflast];
    }

    public override void init()
    {
        if (this.bars == null || this.bars.Length == 0)
        {
            this.bars = new UnrealDisplayObject[this.capacity];
            for (int i = 1; i < this.capacity; i++)
            {
                if (this.transform.Find("bar_" + i) != null) continue;
                GameObject obj = this.cloneAdd(this.transform.Find("bar_0").gameObject);
                obj.name = "bar_" + i;
            }
        }
        for (int i = 0; i < this.capacity; i++)
        {
            this.bars[i] = this.transform.Find("bar_" + i).GetComponent<UnrealDisplayObject>();
            this.bars[i].init();
        }
        this.resize(this.size);
        this.relayout();
    }

    /// <summary>
    /// 设置可见数量
    /// </summary>
    /// <param name="size"></param>
    public void resize(int size)
    {
        this._size = size;
        if (size > capacity) this.increaseCapacity(size);

        for (int i = 0; i < this.bars.Length; i++)
        {
            this.bars[i].gameObject.SetActive(i < size);
        }
    }

    /// <summary>
    /// 增加容量到指定值,小于当前容量则直接返回
    /// </summary>
    public void increaseCapacity(int capacity)
    {
        if (this.capacity >= capacity) return;
        UnrealDisplayObject[] bars = new UnrealDisplayObject[capacity];

        for (int i =this.capacity; i < capacity; i++)
        {
            if (transform.Find("bar_" + i) != null) continue;
            GameObject obj = this.cloneAdd(this.transform.Find("bar_0").gameObject);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.name = "bar_" + i;
        }

        for (int i = 0; i < capacity; i++)
        {
            if (i>this.capacity-1)
            {
                bars[i] = this.transform.Find("bar_" + i).GetComponent<UnrealDisplayObject>();
                bars[i].init();
            }
            else
            {
                bars[i] = this.bars[i];
                this.bars[i].init();
            }
        }

        this.bars = bars;
        this.capacity = capacity;
        relayout();
    }
}