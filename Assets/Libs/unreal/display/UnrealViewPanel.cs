using UnityEngine;
using XLua;

[Hotfix]
public class UnrealViewPanel : UnrealLuaPanel
{
    private Vector2 sizeDelta;
    /// <summary>
    /// 总的内容容器
    /// </summary>
    protected UnrealIndexContainer content_conainer;

    string _title;

    protected UnrealTextField title;

    protected override void xinit()
    {
        base.xinit();
        this.content_conainer = this.transform.Find("Canvas").Find("content").GetComponent<UnrealIndexContainer>();
        this.top = (RectTransform) this.content_conainer.transform.Find("top");
        this.center = (RectTransform) this.content_conainer.transform.Find("center");
        if (this.center != null)
        {
            this.mask = (RectTransform)this.center.Find("mask");
            if (this.mask != null)
            {
                this.maskedContainer = this.mask.Find("container").GetComponent<UnrealContainer>();
            }
        }
        this.bottom = (RectTransform) this.content_conainer.transform.Find("bottom");
        this.content_conainer.init();
        if (this.top != null && this.top.Find("title")!=null)
           this.title = this.top.Find("title").GetComponent<UnrealTextField>();
    }
    public void setTitle(string title)
    {
        this._title = title;
    }
    protected override void xrefresh()
    {
        base.xrefresh();
        if(this.title!=null)
            this.title.text = this._title;
    }
    protected override void xrelayout()
    {
        base.xrelayout();
        if (this.maskedContainer != null)
        {
            this.maskedContainer.relayout();
            this.maskedContainer.resizeDelta();
        }
        this.content_conainer.relayout();
        this.content_conainer.resizeDelta();
        DisplayKit.setLocalY(this.content_conainer, 0);
    }
    protected override void xresizeDelta()
    {
        //整体
        //总高度
        float screenH = UnrealRoot.root.GetComponent<UnrealCamera>()._height;
        float screenW = UnrealRoot.root.GetComponent<UnrealCamera>()._width;
        //本界面本身
        //内容区域(2D)
        RectTransform tran = this.transform.Find("Canvas").GetComponent<RectTransform>();
        this.sizeDelta = new Vector2(screenW, screenH);
        tran.sizeDelta = this.sizeDelta; //便于计算坐标
    }
}