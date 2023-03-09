using cambrian.common;
using System;
using UnityEngine;
using UnityEngine.UI;
using XLua;
using DG.Tweening;

/// <summary>
/// 表格容器,子对象的大小是固定的
/// </summary>
[Hotfix]
public class UnrealTableContainer : UnrealContainer
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
    /// 包含的
    /// </summary>
    [HideInInspector] public UnrealLuaSpaceObject[] bars;
    /// <summary>
    /// 布局,只有左上和居中有效
    /// </summary>
    [HideInInspector] public AutoLayout.Layout layout = AutoLayout.Layout.TopLeft;
    /// <summary>
    /// 顶部介绍
    /// </summary>
    UnrealLuaSpaceObject info;
    /// <summary>
    /// 更多
    /// </summary>
    Transform more;
    /// <summary>
    /// 选中标记
    /// </summary>
    GameObject marking;
    /// <summary>
    /// 选中的
    /// </summary>
    [HideInInspector] public int selected=-1;

    /// <summary>
    /// 游标,位置大小尺寸等于bars[0]
    /// </summary>
    Transform cursor;
    /// <summary>
    /// 初始化高度
    /// </summary>
    float initheight;
    /// <summary>
    /// 上方指示图标(预制件中赋值)
    /// </summary>
    public Image topDirection;
    /// <summary>
    /// 下方指示图标（预制件中赋值)
    /// </summary>
    public Image bottomDirection;

    public int size
    {
        get { return this._size; }
    }

    public float _getHeight()
    {
        float ch = this.bars[0].getHeight() * Mathf.CeilToInt(this.size * 1.0f / this.cols);
        if (this.more != null)
        {
            ch += this.more.GetComponent<UnrealLuaSpaceObject>().getHeight();
        }
        if (this.info != null && this.info.getVisible())
        {
            ch += this.info.getHeight();
        }
        return ch;
    }
    public float _getWidth()
    {
        return this.bars[0].getWidth() * this.cols;
    }

    public override void init()
    {
        base.init();
        this.bars = new UnrealLuaSpaceObject[1];
        this.bars[0] = this.transform.Find("bar_0").GetComponent<UnrealLuaSpaceObject>();
        this.bars[0].init();

        this.cursor = this.transform.Find("cursor");
        if (this.transform.Find("marking") != null)
        {
            this.marking = this.transform.Find("marking").gameObject;
            this.marking.AddComponent<CanvasGroup>().blocksRaycasts = false;
        }
        if (this.transform.Find("info") != null)
        {
            this.info = this.transform.Find("info").GetComponent<UnrealLuaSpaceObject>();
        }
        this.more = this.transform.Find("more");

        initheight = this.GetComponent<RectTransform>().rect.height;

        if (topDirection != null)
        {
            topDirection.DOFade(0.2f, 1.2f).SetLoops(-1,LoopType.Yoyo);   
        }

        if(bottomDirection!=null)
            bottomDirection.DOFade(0.2f, 1.2f).SetLoops(-1, LoopType.Yoyo);
    }

    public void setMoreVisible(bool visible)
    {
        if(this.more==null) return;
        this.more.gameObject.SetActive(visible);
    }
    public void select(UnrealLuaSpaceObject bar)
    {
        int index = this.indexOf(bar);
        this.selected = index;
    }
    private int indexOf(UnrealLuaSpaceObject obj)
    {
        if (obj == null) return -1;
        for (int i = 0; i < this.bars.Length; i++)
        {
            if (this.bars[i] == obj) return i;
        }
        return -1;
    }

    private void setMarking(int index)
    {
        if (this.marking == null) return;
        if (index < 0)
            this.marking.SetActive(false);
        else
        {
            Vector3 vector3 = this.bars[index].transform.localPosition;
            DisplayKit.setLocalXY(this.marking, vector3.x, vector3.y);
            this.marking.SetActive(true);
        }
    }

    private float getCursorX()
    {
        if (this.cursor == null)
            return this.bars[0].transform.localPosition.x;
        if (layout == AutoLayout.Layout.MiddleCenter && this.cols > this.size)
        {
            float sw = this.transform.GetComponent<RectTransform>().rect.width;
            float w = this.cursor.GetComponent<RectTransform>().rect.width * this.size;
            return this.cursor.localPosition.x + (sw - w)/2;
        }
        else return this.cursor.localPosition.x;
    }
    private float getCursorY()
    {
        if (this.cursor == null)
            return this.bars[0].transform.localPosition.y;
//        if (layout == AutoLayout.Layout.MiddleCenter&&this.drag==null)
//        {
//            float sh = this.transform.GetComponent<RectTransform>().rect.height;
//            int row = MathKit.ceil(this.size * 1f / this.cols);
//            float h = this.cursor.GetComponent<RectTransform>().rect.height * row;
//            return this.cursor.localPosition.y - (sh - h) / 2;
//        }
//        else
            return this.cursor.localPosition.y;
    }

    /// <summary>
    /// 预制件中绑定该方法，topDirection,bottomDirection 必然存在
    /// </summary>
    public void onValueChange()
    {
        if (this.drag.vertical)
        {
            if (this.GetComponent<RectTransform>().rect.height <= initheight)
            {
                topDirection.gameObject.SetActive(false);
                bottomDirection.gameObject.SetActive(false);
                return;
            }
            float value= drag.verticalNormalizedPosition;
            topDirection.gameObject.SetActive(false);
            bottomDirection.gameObject.SetActive(false);
            if (value > 0 && value < 1)
            {
                topDirection.gameObject.SetActive(true);
                bottomDirection.gameObject.SetActive(true);

            }
            else if (value >= 1)
            {
                topDirection.gameObject.SetActive(false);
                bottomDirection.gameObject.SetActive(true);
            }
            else
            {
                topDirection.gameObject.SetActive(true);
                bottomDirection.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 设置布局
    /// </summary>
    public override void relayout()
    {
        float lx = this.getCursorX();
        float ly = this.getCursorY();
        for (int i = 0; i < this.size; i++)
        {
            int col = i%this.cols, row = (int) (i/this.cols);
            if (i != 0 && col == 0)
            {
                ly -= this.bars[0].getHeight();
            }
            DisplayKit.setLocalXY(this.bars[i].gameObject, lx + col*this.bars[0].getWidth(), ly);
        }
        if (this.more != null)
        {
            if (this.size > 0)
            {
                ly -= this.bars[0].getHeight();
            }
            DisplayKit.setLocalXY(this.more.gameObject, 0, ly);
        }
        this.setMarking(this.selected);
    }

    /// <summary>
    /// 只适用于pivot x=0时(点在左边)
    /// </summary>
    public void relayoutAuto()
    {
        float lx = this.getCursorX();
        float ly = this.getCursorY();
        for (int i = 0; i < this.size; i++)
        {
            int col = i % this.cols, row = (int)(i / this.cols);
            if (i != 0 && col == 0)
            {
                ly -= this.bars[0].getHeight();
            }
           
            DisplayKit.setLocalXY(this.bars[i].gameObject, lx, ly);
            if (i == 0)
                lx += this.bars[i].getWidth();
            lx += col * this.bars[i].getWidth();
        }
        if (this.more != null)
        {
            if (this.size > 0)
            {
                ly -= this.bars[0].getHeight();
            }
            DisplayKit.setLocalXY(this.more.gameObject, 0, ly);
        }
        this.setMarking(this.selected);
    }

    public override void resizeDelta()
    {
        int n = this._size > cols ? cols : this.size;
        float w = this.bars[0].GetComponent<RectTransform>().rect.width * n;
        int row = MathKit.ceil(this.size*1f/this.cols);
        float h = this.bars[0].GetComponent<RectTransform>().rect.height * row;
        if (this.more != null)
            h += this.more.GetComponent<RectTransform>().rect.height;
        if (this.info != null)
            h += this.info.GetComponent<RectTransform>().rect.height;
        if (this.drag != null)
        {
            float sw = this.drag.GetComponent<RectTransform>().rect.width;
            if (sw > w) w = sw;
            float sh = this.drag.GetComponent<RectTransform>().rect.height;
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

    public void resizeAutoDelta()
    {
        int n = this._size > cols ? cols : this.size;
        float w = 0;
        for (int i = 0; i < n; i++)
        {
            w += this.bars[i].GetComponent<RectTransform>().rect.width;
        }

        int row = MathKit.ceil(this.size * 1f / this.cols);
        float h = this.bars[0].GetComponent<RectTransform>().rect.height * row;
        if (this.more != null)
            h += this.more.GetComponent<RectTransform>().rect.height;
        if (this.info != null)
            h += this.info.GetComponent<RectTransform>().rect.height;
        if (this.drag != null)
        {
            float sw = this.drag.GetComponent<RectTransform>().rect.width;
            if (sw > w) w = sw;
            float sh = this.drag.GetComponent<RectTransform>().rect.height;
            if (sh > h) h = sh;
        }
        RectTransform rectTransform = this.GetComponent<RectTransform>();

        Vector2 vector = rectTransform.sizeDelta;
        float offset = (vector.y - h) / 2;
        vector.x = w;
        vector.y = h;
        rectTransform.sizeDelta = vector;
        float y = this.transform.localPosition.y;
        DisplayKit.setLocalY(this.gameObject, y + offset);
    }

    /// <summary>
    /// 设置可见数量
    /// </summary>
    /// <param name="size"></param>
    public virtual void resize(int size)
    {
        this._size = size;
        this.ensureCapacity(size);
        for (int i = 0; i < this.bars.Length; i++)
        {
            this.bars[i].gameObject.SetActive(i < size);
        }
        if (this.marking != null)
        {
            this.marking.SetActive(false);
            //this.marking.transform.SetAsLastSibling();
            this.marking.transform.SetAsFirstSibling();
        }
        this.relayout();
    }
    public void refresh<R,T>(T[] entities, Action<int, R, T> action) where R : UnrealLuaSpaceObject
    {
        this.refresh(entities,action,0);
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
            if(entities[j]==null) continue;
            this.ensureCapacity(i+1);
            action(i,(R)this.bars[i], entities[j]);
            i++;
        }
        for (int j = 0; j < addtional; j++)
        {
            this.ensureCapacity(i + 1);
            action(i, (R)this.bars[i], default(T));
            i++;
        }
        this.resize(i);
    }
    /// <summary>
    /// 增加容量到指定值,小于当前容量则直接返回
    /// </summary>
    void ensureCapacity(int capacity)
    {
        if (this.bars.Length >= capacity) return;
        UnrealLuaSpaceObject[] bars = new UnrealLuaSpaceObject[capacity];
        for (int i = this.bars.Length; i < capacity; i++)
        {
            string str = "bar_" + i;
            Transform tran = this.transform.Find(str);
            if (tran != null) continue;
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
    }
}