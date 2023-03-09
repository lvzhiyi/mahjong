using cambrian.common;
using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 代金币按钮
/// </summary>
[Hotfix]
public class UnrealPointButton : UnrealTextScaleButton
{

    /// <summary>
    /// 代金币
    /// </summary>
    Text _point;

    Transform icon;

    public long point
    {
        get
        {
            string str = this._point.text;
            if (str.Equals("")) return 0;
            int index = str.IndexOf("\n");
            str = str.Substring(0, index);
            return StringKit.parseInt(str);
        }
        set
        {
            if (value > 0)
            {
                this._point.text = value + "\n"+this.text;
                this.icon.gameObject.SetActive(true);
                this._point.gameObject.SetActive(true);
                this._text.gameObject.SetActive(false);

            }
            else
            {
                this._point.text = "";
                this.icon.gameObject.SetActive(false);
                this._point.gameObject.SetActive(false);
                this._text.gameObject.SetActive(true);
            }
        }
    }

    void Awake()
    {
        this.init();
    }

    protected override void xinit()
    {
        base.xinit();
        this._point = this.transform.Find("point").GetComponent<Text>();
        this.icon = this.transform.Find("icon");
    }
}