using cambrian.common;
using UnityEngine;
using XLua;

/// <summary>
/// 可重用子对象的容器
/// </summary>
[Hotfix]
public class UnrealReusableContainer : UnrealContainer
{
    /// <summary>
    /// 当前容量
    /// </summary>
    int capacity = 1;
    /// <summary>
    /// 每次扩容个数
    /// </summary>
    int space = 2;

    /// <summary>
    /// 包含的子显示对象
    /// </summary>
    [HideInInspector] public UnrealLuaSpaceObject[] bars;
    /// <summary>
    /// 空闲的
    /// </summary>
    ArrayList<int> idles;


    public override void init()
    {
        this.idles = new ArrayList<int>();
        if (this.bars == null || this.bars.Length == 0)
        {
            this.bars = new UnrealLuaSpaceObject[this.capacity];
            for (int i = 1; i < this.capacity; i++)
            {
                GameObject obj = this.cloneAdd(this.transform.Find("bar_0").gameObject);
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.name = "bar_" + i;
                this.idles.add(i);
            }
        }
        for (int i = 0; i < this.capacity; i++)
        {
            this.bars[i] = this.transform.Find("bar_" + i).GetComponent<UnrealLuaSpaceObject>();
            this.bars[i].index_space = i;
            this.bars[i].init();
            this.bars[i].setVisible(false);
        }
    }
    /// <summary>
    /// 取出一个未使用的
    /// </summary>
    /// <returns></returns>
    public UnrealLuaSpaceObject takeout()
    {
        if (this.idles.count == 0)
        {
            this.increaseCapacity(this.capacity + this.space);
        }
        int index = this.idles.removeLast();
        return this.bars[index];
    }
    /// <summary>
    /// 还回容器
    /// </summary>
    /// <param name="bar"></param>
    public void putin(UnrealLuaSpaceObject bar)
    {
        for (int i = 0; i < this.idles.count; i++)
        {
            if (this.idles.get(i) == bar.index_space) return;
        }
        this.idles.add(bar.index_space);
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
            this.idles.add(i);
        }
        for (int i = 0; i < capacity; i++)
        {
            bars[i] = this.transform.Find("bar_" + i).GetComponent<UnrealLuaSpaceObject>();
            bars[i].index_space = i;
            bars[i].init();
            if (i >= this.capacity)
                bars[i].setVisible(false);
        }
        this.bars = bars;
        this.capacity = capacity;
    }
}