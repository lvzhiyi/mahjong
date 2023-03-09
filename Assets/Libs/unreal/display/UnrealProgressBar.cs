using cambrian.common;
using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 简单的进度条
/// </summary>
[Hotfix]
public class UnrealProgressBar : UnrealLuaSpaceObject
{
    public static Color32 color_1 = ColorKit.getColor(0xd0, 0x02, 0x1b);//d0021bff 红
    public static Color32 color_2 = ColorKit.getColor(0x8a, 0xd7, 0x1a);//8ad71aff 绿
    public static Color32 color_3 = ColorKit.getColor(0xF7, 0xA0, 0x1E);//F7A01EFF 橙

    Image bar;

    Image bg;

    Text text;

    public Color32 barColor
    {
        set { this.bar.color = value; }
    }

    protected override void xinit()
    {
        base.xinit();
        this.bar = this.transform.Find("bar").GetComponent<Image>();
        this.bar.type = Image.Type.Filled;
        this.bar.fillMethod = Image.FillMethod.Horizontal;
        this.bg = this.transform.Find("bg").GetComponent<Image>();
        if (this.transform.Find("text") != null)
            this.text = this.transform.Find("text").GetComponent<Text>();
    }
    public void setText(string str)
    {
        if (this.text == null) return;
        this.text.text = str;
    }
    public void setValue(long cur, long max)
    {
        float per = 1.0f*cur/max;
        if (float.IsNaN(per)) per = 0;
        if (per > 1) per = 1;
        if (per < 0) per = 0;
        this.bar.fillAmount = per;
        this.setText(StringKit.toStringMoney(cur) + "/" + StringKit.toStringMoney(max));
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
        this.bar.fillAmount = per;
        this.setText((int) (per*100) + "%");
    }
}