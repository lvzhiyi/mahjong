using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 开关按钮框
/// </summary>
[Hotfix]
public class UnrealCheckBox : UnrealCheckObject
{
    /// <summary>
    /// 显示的文字
    /// </summary>
    Text _text;
    /// <summary>
    /// 图标
    /// </summary>
    [HideInInspector] public Image icon;

    /// <summary>
    /// 一般
    /// </summary>
    [HideInInspector] public Transform _normal;
    /// <summary>
    /// 激活
    /// </summary>
    [HideInInspector] public Transform _actived;

    void Awake()
    {
        this.init();
    }

    protected override void xinit()
    {
        base.xinit();
        this._normal = this.transform.Find("normal");
        this._actived = this.transform.Find("actived");
        if (this._actived == null)
            this._actived = this._normal;
        this.setState(this.state);

        if (this.transform.Find("text") != null)
            this._text = this.transform.Find("text").GetComponent<Text>();
        if (this.transform.Find("icon") != null)
            this.icon = this.transform.Find("icon").GetComponent<Image>();
    }

    public string text
    {
        get { return this._text.text; }
        set
        {
            if (this._text != null)
                this._text.text = value;
        }
    }

    /// <summary>
    /// 设置状态
    /// </summary>
    public override void setState(bool state)
    {
        base.setState(state);
        if (state)
        {
            if (this._normal != null)
                this._normal.gameObject.SetActive(false);
            if (this._actived != null)
                this._actived.gameObject.SetActive(true);
        }
        else
        {
            if (this._actived != null)
                this._actived.gameObject.SetActive(false);
            if (this._normal != null)
                this._normal.gameObject.SetActive(true);
        }
    }
}