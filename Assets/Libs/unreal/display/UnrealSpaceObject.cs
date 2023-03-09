using cambrian.common;
using cambrian.unreal;
using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 具有宽高的2维空间对象
/// </summary>
[Hotfix]
public class UnrealSpaceObject : UnrealDisplayObject
{
    /// <summary>
    /// 图标背景
    /// </summary>
    [HideInInspector] public Image bar_bg;

    public virtual float getHeight()
    {
        return ((RectTransform) this.transform).rect.height;
    }

    public virtual float getHeight(SizeDeltaLayer layer)
    {
        return getHeight();
    }

    public virtual void setHeight(float height)
    {
        Vector2 sizeDeta= ((RectTransform)this.transform).sizeDelta;
        sizeDeta.y = height;
        ((RectTransform)this.transform).sizeDelta=sizeDeta;
    }

    public virtual void setWidth(float width)
    {
        Vector2 sizeDeta = ((RectTransform)this.transform).sizeDelta;
        sizeDeta.x = width;
        ((RectTransform)this.transform).sizeDelta = sizeDeta;
    }

    public virtual float getWidth()
    {
        return ((RectTransform) this.transform).rect.width;
    }

    public virtual float getWidth(SizeDeltaLayer layer)
    {
        return this.getWidth();
    }
    /// <summary>
    /// 最左边的X值
    /// </summary>
    /// <returns></returns>
    public virtual float getMinX()
    {
        return this.transform.localPosition.x - this.getWidth()/2;
    }
    /// <summary>
    /// 最右边的X值
    /// </summary>
    /// <returns></returns>
    public float getMaxX()
    {
        return this.getMinX() + this.getWidth();
    }
    /// <summary>
    /// 最上边的Y值
    /// </summary>
    /// <returns></returns>
    public virtual float getMaxY()
    {
        return this.transform.localPosition.y + this.getHeight()/2;
    }
    /// <summary>
    /// 最下边的Y值
    /// </summary>
    /// <returns></returns>
    public float getMinY()
    {
        return this.getMaxY() - this.getHeight();
    }

    public void RigisterBtnObjectEvent(string name, EventTriggerListener.VoidDelegate onclick)
    {
        GameObject btn = UnityHelper.FindTheChildNode(this.gameObject, name).gameObject;
        if (btn != null)
            EventTriggerListener.Get(btn).onClick = onclick;
    }

    public override void init()
    {
        base.init();
        if (this.transform.Find("bar_bg") != null)
            this.bar_bg = this.transform.Find("bar_bg").GetComponent<Image>();
    }
    /// <summary>
    /// 斑马纹式设置透明度
    /// </summary>
    /// <param name="i"></param>
    /// <param name="alpha">[0,1]</param>
    public void resetBg(int i, float alpha)
    {
        if (this.bar_bg == null) return;
        if (i%2 == 1)
            ColorKit.setAlpha(this.bar_bg, 1);
        else
        {
            if(alpha==0) this.bar_bg.gameObject.SetActive(false);
            else ColorKit.setAlpha(this.bar_bg, alpha);
        }
    }
    /// <summary>
    /// 斑马纹式设置透明度
    /// </summary>
    /// <param name="i"></param>
    /// <param name="alpha1">[0,1]</param>
    /// <param name="alpha2">[0,1]</param>
    public void resetBg(int i, float alpha1, float alpha2)
    {
        if (this.bar_bg == null) return;
        if (i % 2 == 1)
            ColorKit.setAlpha(this.bar_bg, alpha1);
        else
        {
            if (alpha2 == 0) this.bar_bg.gameObject.SetActive(false);
            else ColorKit.setAlpha(this.bar_bg, alpha2);
        }
    }
}