using System;
using cambrian.common;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 滑动条主键
/// </summary>
[Hotfix]
public class SliderComponent : UnrealLuaSpaceObject
{
    Text text;

    Slider slider;
    /// <summary>
    /// 增加
    /// </summary>
    Button add;
    /// <summary>
    /// 减少
    /// </summary>
    Button decr;
    /// <summary>
    /// 最大值
    /// </summary>
    long max;
    /// <summary>
    /// 当前值
    /// </summary>
    long cur;
    /// <summary>
    /// 当前值的颜色
    /// </summary>
    public string color;
    /// <summary>
    /// 回调
    /// </summary>
    private Action<long> action;

    protected override void xinit()
    {
        if (this.transform.Find("text") != null)
            this.text = this.transform.Find("text").GetComponent<Text>();
        this.slider = this.transform.Find("slider").GetComponent<Slider>();
        if (this.transform.Find("add") != null)
            this.add = this.transform.Find("add").GetComponent<Button>();
        if (this.transform.Find("decr") != null)
            this.decr = this.transform.Find("decr").GetComponent<Button>();
    }
    public void setText(string str)
    {
        if (this.text == null) return;
        this.text.text = str;
    }

    public long getCur()
    {
        return cur;
    }

    [LuaCallCSharp]
    public void setAction(Action<long> action)
    {
        this.action = action;
    }

    public void setValue(long cur, long max)
    {
        float per = 1.0f * cur / max;
        if (float.IsNaN(per)) per = 0;
        if (per > 1) per = 1;
        if (per < 0) per = 0;
        this.slider.value = per;
        this.cur = cur;
        this.max = max;
        if (color == null || "".Equals(color))
            this.setText(StringKit.toStringMoney(cur) + "/" + StringKit.toStringMoney(max));
        else
            this.setText("<color="+ color + ">"+StringKit.toStringMoney(cur) + "</color>/" + StringKit.toStringMoney(max));
    }
    /// <summary>
    /// 设置进度[0,1]
    /// </summary>
    /// <param name="per"></param>
    public void setValue(float per)
    {
        if (float.IsNaN(per)) per = 0;
        if (per > 1) per = 1;
        if (per < 0) per = 0;
        this.setText((int)(per * 100) + "%");
    }

    public void onValueChange()
    {
        cur=(long)(((slider.value * 10000)*max/10000));
        if (color == null || "".Equals(color))
            this.setText(StringKit.toStringMoney(cur) + "/" + StringKit.toStringMoney(max));
        else
            this.setText("<color=" + color + ">" + StringKit.toStringMoney(cur) + "</color>/" + StringKit.toStringMoney(max));
        if (action != null)
            action(cur);
    }

    public void addOne()
    {
        if (cur >= max) return;
        this.cur++;
        setValue(cur,max);
    }

    public void decrOne()
    {
        if (cur <=1) return;
        this.cur--;
        setValue(cur, max);
    }

}


