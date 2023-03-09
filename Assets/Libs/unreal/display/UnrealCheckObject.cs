using XLua;
/// <summary>
/// 两种状态显示对象
/// </summary>
[Hotfix]
public class UnrealCheckObject : UnrealLuaSpaceObject
{
    /// <summary>
    /// 状态 合并，展开
    /// </summary>
    public const bool NORMAL = false, ACTIVED = true;
    /// <summary>
    /// 按钮状态
    /// </summary>
    private bool _state;

    public bool state
    {
        get { return this.getState(); }
        set { this.setState(value); }
    }

    public virtual bool getState()
    {
        return this._state;
    }
    /// <summary>
    /// 反转
    /// </summary>
    /// <returns></returns>
    public bool reverse()
    {
        this.setState(!state);
        return state;
    }
    public virtual void setState(bool state)
    {
        this._state = state;
    }
}
