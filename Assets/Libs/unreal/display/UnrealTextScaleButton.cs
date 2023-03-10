using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 带文字的按钮
/// </summary>

[Hotfix]
public class UnrealTextScaleButton : UnrealScaleButton
{
    /// <summary>
    /// 文字
    /// </summary>
    protected Text _text;

    /// <summary>
    /// 一般状态的文字颜色（16进制）[文本颜色,描边颜色,阴影颜色]
    /// </summary>
    public Color[] normal_color;

    /// <summary>
    /// 选中状态的文字颜色（16进制）[文本颜色,描边颜色,阴影颜色]
    /// </summary>
    public Color[] activie_color;

    /// <summary>
    /// 禁用状态的文字颜色（16进制）[文本颜色,描边颜色,阴影颜色]
    /// </summary>
    public Color[] disable_color;

    /// <summary>
    /// 描边
    /// </summary>
    private Outline outline;
    /// <summary>
    /// 阴影
    /// </summary>
    private Shadow shadow;


    public Text getText()
    {
        return _text;
    }

    public string text
    {
        get { return this._text.text; }
        set { this._text.text = value; }
    }

    public override void setState(int state)
    {
        base.setState(state);
        if (state == NORMAL)
        {
            if (normal_color == null||normal_color.Length==0) return;
            setTextColor(normal_color);
        }
        else if(state==ACTIVED)
        {
            if (activie_color == null||activie_color.Length==0) return;
            setTextColor(activie_color);
        }
        else if(state==DISABLE)
        {
            if (disable_color == null||disable_color.Length==0) return;
            setTextColor(disable_color);
        }
    }

    private void setTextColor(Color[] colors)
    {
        this._text.color = colors[0];
        if (colors.Length == 2)
        {
            if (outline != null)
            {
                this.outline.effectColor = colors[1];
            }
        }

        if (colors.Length == 3)
        {
            if (shadow != null)
            {
                this.shadow.effectColor = colors[2];
            }
        }
    }

    protected override void xinit()
    {
        base.xinit();
        this._text = this.transform.Find("text").GetComponent<Text>();
        this.outline = this._text.GetComponent<Outline>();
        this.shadow = this._text.GetComponent<Shadow>();
    }


}