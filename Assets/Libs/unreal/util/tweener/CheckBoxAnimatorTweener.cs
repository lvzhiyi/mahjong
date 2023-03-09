using cambrian.unreal.display;
using XLua;
/// <summary>
/// 缓动-两帧切换动画
/// </summary>
[Hotfix]
public class CheckBoxAnimatorTweener : TimerListener<UnrealLuaPanel>
{
    /// <summary>
    /// 每帧间隔时间(ms)
    /// </summary>
    public int space = 200;

    UnrealCheckBox check;

    /// <summary>
    /// 上次刷新时间
    /// </summary>
    long lasttime;

    void Awake()
    {
        this.check = this.transform.GetComponent<UnrealCheckBox>();
        this.check.init();
    }

    public override void update(long time)
    {
        base.update(time);
        if (time - this.lasttime < this.space) return;
        this.lasttime = time;
        this.check.setState(!this.check.getState());
    }
}