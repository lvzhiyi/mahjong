using cambrian.common;
using XLua;

/// <summary>
/// 控制对象自身的显示状态
/// </summary>
[Hotfix]
public class CheckSelfObjectProcess : MouseClickProcess
{
    public override void execute()
    {
        UnrealCheckObject check = this.transform.GetComponent<UnrealCheckObject>();
        check.setState(!check.state);
    }
}