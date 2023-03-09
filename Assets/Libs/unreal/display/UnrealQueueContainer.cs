using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 队列容器，只适用于单个单个添加，并拥有固定容量的列表
/// </summary>
[Hotfix]
public class UnrealQueueContainer : UnrealContainer
{

    /// <summary> 固定容量，初始化后改变无效 </summary>
    public int capacity = 1;

    /// <summary> 当前显示数量 </summary>
    [HideInInspector] private int _size = 0;

    /// <summary> 头索引 </summary>
    [HideInInspector] private int head = 0;

    /// <summary> 元素 </summary>
    [HideInInspector] private UnrealLuaSpaceObject[] bars;

    /// <summary>
    /// 每行的间距(默认无间距)
    /// </summary>
    public int lineSpace=0;

    public int size
    {
        get { return this._size; }
    }

    public override void init()
    {
        if (this.bars == null || this.bars.Length == 0)
        {
            if (this.capacity < 1) throw new UnityException("invalid capacity: " + this.capacity);
            this.bars = new UnrealLuaSpaceObject[this.capacity];
            for (int i = 0; i < this.capacity; i++)
            {
                GameObject obj = null;
                if (i == 0)
                    obj = this.transform.Find("bar_0").gameObject;
                else
                    obj = this.create(this.bars[0].gameObject, "bar_" + i, false);
                this.bars[i] = obj.GetComponent<UnrealLuaSpaceObject>();
                this.bars[i].init();
                this.bars[i].setVisible(false);
            }
        }
        this.resizeDelta();
    }

    public override float getHeight()
    {
        float ch = 0;
        for (int i = 0; i < this._size; i++)
        {
            int index = this.head + i;
            if (index >= this.bars.Length) index -= this.bars.Length;
            ch += this.bars[index].getHeight();
            if (i > 0) ch += this.lineSpace;
        }
        float sh = this.getScrollH();
        if (sh >= ch) ch = sh;
        return ch;
    }

    public override float getWidth()
    {
        return this.bars[0].getWidth();
    }

    private GameObject create(GameObject sample, string name, bool destory)
    {
        GameObject obj = this.cloneAdd(sample);
        if (destory) Destroy(sample);
        obj.name = name;
        obj.transform.localScale = new Vector3(1, 1, 1);
        return obj;
    }

    private float getScrollH()
    {
        ScrollRect scrollRect = this.transform.parent.parent.GetComponent<ScrollRect>();
        float sh = scrollRect==null?0:scrollRect.GetComponent<RectTransform>().rect.height;
        return sh;
    }

    public override void relayout()
    {
        if (this._size == 0) return;
        float y = this.getHeight()/2;
        for (int i = 0; i < this._size; i++)
        {
            int index = this.head + i;
            if (index >= this.bars.Length) index -= this.bars.Length;
            this.bars[index].relayout();
            float bar_h = this.bars[index].getHeight();
            DisplayKit.setLocalY(this.bars[index].gameObject, y - bar_h/2);
            y -= bar_h + this.lineSpace;
        }
    }

    public override void resizeDelta()
    {
        this.relayout();
        if (this.transform is RectTransform)
        {
            RectTransform rectTransform = (RectTransform)this.transform;
            float h = this.getHeight();
            Vector2 vector = rectTransform.sizeDelta;
            vector.y = h;
            rectTransform.sizeDelta = vector;
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, h);// 显示脚
        }
    }

    /// <summary>获取下一个显示</summary>
    public UnrealLuaSpaceObject next()
    {
        int index = this.head + this._size;
        if (index >= this.bars.Length) index -= this.bars.Length;
        if (++this._size > this.bars.Length)
        {
            this._size = this.bars.Length;
            if (++this.head >= this.bars.Length) this.head -= this.bars.Length;
        }
        this.bars[index].setVisible(true);
        return this.bars[index];
    }

    public void clearShowBars()
    {
        for (int i = 0; i < this.bars.Length; i++)
        {
            this.bars[i].setVisible(false);
        }
        this._size = 0;
        this.head = 0;
    }
}