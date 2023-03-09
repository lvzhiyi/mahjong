using UnityEngine;
using XLua;

/// <summary>
/// 带文本的按钮
/// </summary>

[Hotfix]
public class UnrealTextButton : UnrealScaleButton
{
    [HideInInspector] public UnrealTextField text;

    protected override void xinit()
    {
        base.xinit();
        this.text = this.transform.Find("text").GetComponent<UnrealTextField>();
        this.text.init();
    }
}