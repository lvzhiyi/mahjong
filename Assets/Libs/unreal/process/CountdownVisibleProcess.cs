using cambrian.common;
using System;
using XLua;

/// <summary>
/// 倒计时N毫秒后不可见(version：0.1.61)
/// </summary>
[Hotfix]
public class CountdownVisibleProcess : TimerListener<UnrealLuaPanel>
{
    const long SPACE_MILLS = 1000L;
    /// <summary>
    /// 显示时间ms
    /// </summary>
    public long countdown = 3000;
    /// <summary>
    /// 显示时的时间
    /// </summary>
    public long starttime;

    public Action callback;


    [CSharpCallLua]
    public void setAction(Action callback)
    {
        this.callback = callback;
        this.starttime = TimeKit.currentTimeMillis;
    }


    public override void update(long time)
    {
        if (this.starttime > 0)
        {
            if (this.starttime + countdown > time) return;
            this.starttime = 0;

            if (callback != null)
                callback();
            this.setVisible(false);

        }
        else
        {
            this.starttime = time;
        }
    }
}