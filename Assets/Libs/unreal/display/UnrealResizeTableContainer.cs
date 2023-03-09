using System;
using cambrian.common;
using UnityEngine;
using UnityEngine.UI;
using XLua;

[Hotfix]
public class UnrealResizeTableContainer : UnrealContainer
{
    /// <summary>
    /// 显示数量
    /// </summary>
    int _size = 1;

    /// <summary>
    /// 列数
    /// </summary>
    public int cols = 1;

    /// <summary>
    /// 容量
    /// </summary>
    int capacity = 1;

    /// <summary>
    /// 包含的
    /// </summary>
    [HideInInspector] public UnrealLuaSpaceObject[] bars;

    /// <summary>
    /// 更多
    /// </summary>
    [HideInInspector] public Transform more;

    /// <summary>
    /// 空间对象变化值(x,y)
    /// </summary>
    ArrayList<int[]> arrays;

    /// <summary>
    /// 增加的高度
    /// </summary>
    int incrheight;

    public int size
    {
        get { return this._size; }
    }
    public override float getHeight()
    {
        float ch = this.bars[0].getHeight()*Mathf.CeilToInt(this.size*1.0f/this.cols) + this.incrheight;
        if (this.more != null)
        {
            ch += this.more.GetComponent<UnrealLuaSpaceObject>().getHeight();
        }
        return ch;
    }
    public override float getWidth()
    {
        return this.bars[0].getWidth()*this.cols;
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
        this.arrays = new ArrayList<int[]>();
        this.arrays.add(new int[] {0, 0});
        this.arrays.add(new int[] {20, 50});
        this.arrays.add(new int[] {50, 0});
        this.arrays.add(new int[] {-80, 150});
        this.resize(this.size);
    }
    public void setList(ArrayList<int[]> point)
    {
        this.arrays = point;
    }
    private int indexOf(UnrealLuaSpaceObject obj)
    {
        for (int i = 0; i < this.capacity; i++)
        {
            if (this.bars[i] == obj) return i;
        }
        return -1;
    }
    private int[] getIndexPostion(int index)
    {
        int i = index%this.arrays.count;
        return this.arrays.get(i);
    }
    /// <summary>
    /// 设置布局
    /// </summary>
    public override void relayout()
    {
        this.incrheight = 0;
        float lx = this.bars[0].transform.localPosition.x;
        float ly = this.bars[0].transform.localPosition.y;
        float lz = this.bars[0].transform.localPosition.z;

        int currow = 0;

        float lx_1 = 0;

        for (int i = 0; i < this.size; i++)
        {
            lx_1 = lx;
            int col = i%this.cols, row = (int) (i/this.cols);
            int[] postion = this.getIndexPostion(i);
            if (currow == row)
            {
                ly -= postion[1];
            }
            else
            {
                ly -= this.bars[0].getHeight() + postion[1];
            }
            lx_1 += postion[0];
            this.incrheight += postion[1];
            currow = row;

            DisplayKit.setLocalXYZ(this.bars[i].gameObject, lx_1 + col*this.bars[0].getWidth(), ly, lz);
        }
        if (this.more != null)
        {
            if (this.size > 0)
                ly -= this.bars[0].getHeight();
            DisplayKit.setLocalXYZ(this.more.gameObject, 0, ly, lz);
        }
    }
    public override void resizeDelta()
    {
        this.resizeDelta(false);
    }
    /// <summary>
    /// 排版的子操作,当其为ScrollRect的子gameobject时
    /// </summary>
    /// <param name="center"></param>
    public void resizeDelta(bool center)
    {
        //此处的横向居中是用的一个BUG效果(this.transform.localPosition.x)
        int col = this.cols;
        if (col > this.size) col = this.size;
        float w = this.bars[0].GetComponent<RectTransform>().rect.width*col;
        int row = MathKit.ceil(this.size*1f/this.cols);
        float h = this.bars[0].GetComponent<RectTransform>().rect.height*row + this.incrheight;
        ScrollRect scrollRect = this.transform.parent.GetComponent<ScrollRect>();
        if (scrollRect == null) scrollRect = this.transform.parent.parent.GetComponent<ScrollRect>();
        if (!center && scrollRect != null && scrollRect.content == this.GetComponent<RectTransform>())
        {
            float sw = scrollRect.GetComponent<RectTransform>().rect.width;
            if (sw > w) w = sw;
            float sh = scrollRect.GetComponent<RectTransform>().rect.height;
            if (sh > h) h = sh;
        }
        RectTransform rectTransform = this.GetComponent<RectTransform>();

        Vector2 vector = rectTransform.sizeDelta;
        float offset = (vector.y - h)/2;
        vector.x = w;
        vector.y = h;
        rectTransform.sizeDelta = vector;
        float y = this.transform.localPosition.y;
        DisplayKit.setLocalY(this.gameObject, y + offset);
    }
    public void resize()
    {
        int i = 0;
        Transform tran = this.transform.Find("bar_" + i);
        while (tran != null)
        {
            i++;
            tran = this.transform.Find("bar_" + i);
        }
        this.capacity = i;
        this.bars = new UnrealLuaSpaceObject[this.capacity];
        for (i = 0; i < this.capacity; i++)
        {
            this.bars[i] = this.transform.Find("bar_" + i).GetComponent<UnrealLuaSpaceObject>();
            this.bars[i].init();
        }
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
    public void refresh<R, T>(T[] entities, Action<int, R, T> action) where R : UnrealLuaSpaceObject
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
    public void refresh<R, T>(T[] entities, Action<int, R, T> action, int addtional) where R : UnrealLuaSpaceObject
    {
        int i = 0;
        for (int j = 0; j < entities.Length; j++)
        {
            if (entities[j] == null) continue;
            this.increaseCapacity(i + 1);
            action(i, (R)this.bars[i], entities[j]);
            i++;
        }
        for (int j = 0; j < addtional; j++)
        {
            this.increaseCapacity(i + 1);
            action(i, (R)this.bars[i], default(T));
            i++;
        }
        this.resize(i);
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