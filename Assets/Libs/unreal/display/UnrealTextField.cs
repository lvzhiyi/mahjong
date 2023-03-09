using cambrian.uui;
using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 文本框
/// </summary>
[Hotfix]
public class UnrealTextField : UnrealLuaSpaceObject
{
    /// <summary>
    ///  重设文本框的大小
    /// </summary>
    [HideInInspector] public ResetTextRectTransform resettext;
    /// <summary>
    /// 包含的文字
    /// </summary>
    public string text
    {
        get
        {
            if (this.textField == null) this.init();
            return this.textField.text;
        }
        set
        {
            if (this.textField == null) this.init();
            this.textField.text = value==null?"":value;
            if (this.resettext != null) this.resettext.enabled = true;
        }
    }

    public Text getText
    {
        get { return this.textField; }
    }



    public float alpha
    {
        get
        {
            if (this.textField == null) this.init(); 
            return this.textField.color.a;
        }
        set
        {
            if (this.textField == null) this.init();
            Color32 color = this.textField.color;
            color.a = (byte)(value*255);
            this.textField.color = color;
            if (this.textInfoField == null) return;
            this.textInfoField.color = color;
        }
    }
    public Color32 color
    {
        set
        {
            if (this.textField == null) this.init();
            this.textField.color = value;
        }
    }

    public Color32 outlineColor
    {
        set
        {
            if(this.textField==null) this.init();
            for (int i = 0; i < this.outline.Length; i++)
            {
                this.outline[i].effectColor = value;
            }
        }
    }

    public Color32 shadowColor
    {
        set
        {
            if (this.textField == null) this.init();
            for (int i = 0; i < this.shadow.Length; i++)
            {
                this.shadow[i].effectColor = value;
            }
        }
    }

    public Color32 gradientTop
    {
        set
        {
            if (this.textField == null) this.init();
                this.gradient.colorTop= value;
        }
    }

    public Color32 gradientBottom
    {
        set
        {
            if (this.textField == null) this.init();
            this.gradient.colorBottom = value;
        }
    }

    /// <summary>
    /// 文字的说明
    /// </summary>
    public string textInfo
    {
        get
        {
            if (this.textField == null) this.init();
            if (this.textInfoField == null) return "";
            return this.textInfoField.text;
        }
        set
        {
            if (this.textField == null) this.init();
            if (this.textInfoField == null) return;
            this.textInfoField.text = value;
        }
    }

    /// <summary>
    /// 是否可编辑
    /// </summary>
    bool _editable;
    public bool editable
    {
        get { return this._editable; }
        set
        {
            this._editable = value;
            if (this.transform.Find("edit_icon") != null)
                this.transform.Find("edit_icon").gameObject.SetActive(value);
        }
    }
    /// <summary>
    /// 可交互的
    /// </summary>
    public bool interactive
    {
        get { return this._editable; }
        set
        {
            this._editable = value;
            if (this.transform.Find("edit_icon") != null)
                this.transform.Find("edit_icon").gameObject.SetActive(value);
            if (this.transform.Find("interactive") != null)
                this.transform.Find("interactive").gameObject.SetActive(value);
        }
    }
    /// <summary>
    /// 显示
    /// </summary>
    Text textField;
    /// <summary>
    /// 显示
    /// </summary>
    [HideInInspector] public Text textInfoField;

    private Outline[] outline;

    private Shadow[] shadow;

    private cambrian.uui.Gradient gradient;

    protected override void xinit()
    {
        base.xinit();
        this.textField = this.transform.Find("text").GetComponent<Text>();
        this.outline = this.textField.GetComponents<Outline>();

        this.shadow = this.textField.GetComponents<Shadow>();

        this.gradient = this.textField.GetComponent<cambrian.uui.Gradient>();

        if (this.transform.Find("text_info") != null)
            this.textInfoField = this.transform.Find("text_info").GetComponent<Text>();
    }

}