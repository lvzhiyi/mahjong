using UnityEngine;
using XLua;

/// <summary>
/// 按钮不同状态大小变化
/// </summary>

[Hotfix]
public class UnrealScaleButton : UnrealButton
{
    /// <summary> 当前缩放值 </summary>
    Vector3 scale;

    protected override void xinit()
    {
        base.xinit();
        this.scale = this.transform.localScale;
    }

    public override void setState(int state)
    {
        base.setState(state);
        if (state == NORMAL || state == DISABLE)
        {
            this.transform.localScale = this.scale;
        }
        else
        {
            Vector3 scale = this.transform.localScale;
            scale.x = this.scale.x*1.1f;
            scale.y = this.scale.y*1.1f;
            this.transform.localScale = scale;
        }
    }
}
