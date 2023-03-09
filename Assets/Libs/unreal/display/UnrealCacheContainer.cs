using UnityEngine;
using XLua;

/// <summary>
/// 可重用的容器
/// </summary>
[Hotfix]
public class UnrealCacheContainer : UnrealContainer
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
    [HideInInspector] public UnrealLuaSpaceObject[] bars;

    public int size
    {
        get { return this._size; }
    }



    public override void init()
    {
        if (this.bars == null || this.bars.Length == 0)
        {
            this.bars = new UnrealLuaSpaceObject[this.capacity];
            for (int i = 1; i < this.capacity; i++)
            {
                GameObject obj = this.cloneAdd(this.transform.Find("bar_0").gameObject);
                obj.name = "bar_" + i;
            }
        }
        for (int i = 0; i < this.capacity; i++)
        {
            this.bars[i] = this.transform.Find("bar_" + i).GetComponent<UnrealLuaSpaceObject>();
            this.bars[i].init();
        }
        this.resize(this.size);
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
            GameObject obj = this.cloneAdd(this.transform.Find("bar_0").gameObject);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.name = "bar_" + i;
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