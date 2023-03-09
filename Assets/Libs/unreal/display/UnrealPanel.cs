using cambrian.unreal;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 载入prefab类的基类
/// </summary>
[Hotfix]
public class UnrealPanel : UnrealDisplayObject
{
    /// <summary>
    /// 高度:标题-80,一级页卡-80,聊天条-60,关闭条-100
    /// </summary>
    public const int TITLE_HEIGHT = 80, TAB_HEIGHT = 80, MSG_HEIGHT = 60, EXIT_HEIGHT = 100;

    public int id;

    /// <summary>
    /// 出现是是否缓动
    /// </summary>
    [HideInInspector] public bool tweener = false;

    /// <summary>
    /// 打开当前界面的处理器
    /// </summary>
    [HideInInspector] public IProcess process;

    Canvas canvas;

    protected RectTransform top;

    protected RectTransform mask;

    protected RectTransform center;

    protected RectTransform bottom;

    protected UnrealContainer maskedContainer;

    protected RectTransform left;

    protected RectTransform right;

    /// <summary>
    /// 记录上一次打开的面板
    /// </summary>
    private UnrealPanel lastPanel;

    /// <summary>
    /// 
    /// </summary>
    public Layer layer = Layer.Default;

    public Transform content;
    /// <summary>
    /// 关闭是否摧毁
    /// </summary>
    public bool isDestory;
    void Awake()
    {
        this.setLayer(this.transform, (int) this.layer);
        int z = 32 - (int) this.layer;
        this.transform.localPosition = new Vector3(0, 0, z);
        Transform canvas = this.transform.Find("Canvas");
        //地图显示用的是3d
        if (canvas != null)
            this.canvas = canvas.GetComponent<Canvas>();

        this.resizeDelta(new Margin());
    }

    public override void init()
    {
        content = this.transform.Find("Canvas");
        //2d
        if (content != null)
        {
            this.top = (RectTransform) content.transform.Find("top");
            if (content.transform.Find("center") != null)
            {
                this.center = (RectTransform) content.transform.Find("center");

                this.mask = (RectTransform) this.center.Find("mask");
                if (this.mask != null)
                {
                    this.maskedContainer = this.mask.Find("container").GetComponent<UnrealContainer>();
                }
            }
            this.bottom = (RectTransform) content.transform.Find("bottom");
            this.left = (RectTransform) content.transform.Find("left");
            this.right = (RectTransform) content.transform.Find("right");
        }
    }

    /// <summary>
    /// 设置最后打开的面板
    /// </summary>
    public void setLastPanel(UnrealPanel panel)
    {
        this.lastPanel = panel;
    }

    /// <summary>
    /// 最后打开的面板
    /// </summary>
    public UnrealPanel getLastPanel()
    {
        return this.lastPanel;
    }

    public void RigisterBtnObjectEvent(string name, EventTriggerListener.VoidDelegate onclick)
    {
        GameObject btn = UnityHelper.FindTheChildNode(this.gameObject, name).gameObject;
        if (btn != null)
            EventTriggerListener.Get(btn).onClick = onclick;
    }

    /// <summary>
    /// 设置自己及其子对象的所属层级
    /// </summary>
    /// <param name="tran"></param>
    /// <param name="layer"></param>
    protected void setLayer(Transform tran, int layer)
    {
        tran.gameObject.layer = layer;
        foreach (Transform child in tran)
        {
            this.setLayer(child, layer);
        }
    }

    public float getScreenHeight()
    {
        return UnrealRoot.root.GetComponent<UnrealCamera>()._height;
    }

    public float getTopHeight()
    {
        float screenH = UnrealRoot.root.GetComponent<UnrealCamera>()._height;
        //顶 顶端
        float top = TITLE_HEIGHT;
        if (this.top != null) top += this.top.rect.height;
        return (screenH/2 - top)/100;
    }

    /// <summary>
    /// 根据屏幕重设显示区域大小及位置(ugui)
    /// </summary>
    protected virtual void resizeDelta()
    {
        this.resizeDelta(new Margin(Margin.VERTICAL, MSG_HEIGHT + TITLE_HEIGHT, EXIT_HEIGHT));
    }

    protected void resizeDelta(Margin margin)
    {
        //整体
        //总高度
        float screenH = UnrealCamera.main._height;
        float screenW = UnrealCamera.main._width;

        //顶 
        float topH = margin.top;
        //底 
        float bottomH = margin.bottom;

        //本界面本身
        //内容区域(2D)
        RectTransform tran = this.transform.Find("Canvas").GetComponent<RectTransform>();
        tran.sizeDelta = new Vector2(screenW, screenH); //便于计算坐标

        if (tran.Find("right") != null)
        {
            RectTransform right = tran.Find("right").GetComponent<RectTransform>();
            Vector2 vector = right.sizeDelta;
            vector.y = screenH - topH - bottomH;
            right.sizeDelta = vector;

            //拖动上下边的差值的一半
            Vector3 position = right.localPosition;
            position.y = (bottomH - topH)/2;
            right.localPosition = position;
        }
        //内容 顶
        if (tran.Find("top") != null)
        {
            RectTransform top = tran.Find("top").GetComponent<RectTransform>();
            Vector3 vector3 = top.localPosition;
            vector3.y = screenH/2 - topH - top.rect.height/2;
            top.localPosition = vector3;
            topH += top.rect.height;
        }
        //内容 底
        if (tran.Find("bottom") != null)
        {
            RectTransform bottom = tran.Find("bottom").GetComponent<RectTransform>();
            Vector3 vector3 = bottom.localPosition;
            vector3.y = -(screenH/2 - bottomH - bottom.rect.height/2);
            bottom.localPosition = vector3;
            bottomH += bottom.rect.height;
        }
        //内容 中
        if (tran.Find("center") != null)
        {
            RectTransform center = tran.Find("center").GetComponent<RectTransform>();
            Vector2 vector = center.sizeDelta;
            vector.y = screenH - topH - bottomH;
            center.sizeDelta = vector;

            //拖动上下边的差值的一半
            Vector3 position = center.localPosition;
            position.y = (bottomH - topH)/2;
            center.localPosition = position;

            ScrollRect drag = center.GetComponent<ScrollRect>();
            if (drag != null)
            {
                drag.content.GetComponent<UnrealContainer>().drag = drag;
                drag.verticalNormalizedPosition = 1;
            }
        }
    }

    /// <summary>
    /// 是否绘制
    /// </summary>
    private bool _enableRender
    {
        set
        {
            if (this.canvas == null) return;
            if (value)
                this.canvas.sortingOrder = (int) this.layer;
            else
                this.canvas.sortingOrder = -10;
        }
    }

    public override void setVisible(bool b)
    {
        this._enableRender = b;
        base.setVisible(b);

        if (!b)
            isDestoryUnrealPanel();
    }

    /// <summary>
    /// 是否销毁面板
    /// </summary>
    public void isDestoryUnrealPanel()
    {
        if (isDestory)
        {
            UnrealRoot.root.clearGameObject(name);
        }
    }


    /// <summary>
    /// 有变化过程
    /// </summary>
    /// <param name="b"></param>
    public virtual void setCVisible(bool b)
    {
        this._enableRender = b;
        if (!b)
            base.setVisible(b);

        if (b)
        {
            DisplayKit.setLocalScaleXY(this.transform, 0.6f);
            Sequence s = DOTween.Sequence();
            base.setVisible(b);
            s.Append(this.transform.DOScale(new Vector3(1, 1, 1), 0.35f).SetEase(Ease.InOutBack));
            s.InsertCallback(0.01f, () => {  });
        }

        if (!b)
            isDestoryUnrealPanel();
    }
}