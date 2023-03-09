using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 图片
/// </summary>

[Hotfix]
public class UnrealSprite : UnrealLuaSpaceObject
{

    Image image;

    protected override void xinit()
    {
        base.xinit();
        this.image = this.GetComponent<Image>();
    }

    public void setSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
    public void setNativeSize()
    {
        image.SetNativeSize();
    }
}