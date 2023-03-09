using UnityEngine;
using UnityEngine.UI;
using XLua;

[Hotfix]
public class ProcessBar : UnrealLuaSpaceObject
{
    ProxyProcess proxy;
    public IProcess process
    {
        get { return this.proxy.process; }
        set
        {
            this.proxy.process = value;
            if (this.icon != null)
            {
                this.icon.sprite = value.getSprite();
                this.icon.SetNativeSize();
            }
            if (this.text != null) this.text.text = this.process.getTitle();
        }
    }
    /// <summary>
    /// 显示的文字
    /// </summary>
    Text text;
    /// <summary>
    /// 图标
    /// </summary>
    [HideInInspector] public Image icon;


    protected override void xinit()
    {
        base.xinit();
        this.proxy = this.GetComponent<ProxyProcess>();
        if (this.transform.Find("icon"))
            this.icon = this.transform.Find("icon").GetComponent<Image>();
        if (this.transform.Find("text"))
            this.text = this.transform.Find("text").GetComponent<Text>();
    }
}