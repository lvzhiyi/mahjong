using cambrian.common;
using System;
using XLua;

/// <summary>
/// 倒计时(version：0.1.61)
/// </summary>
[Hotfix]
public class TimerCountdownProcess : TimerListener<UnrealLuaPanel>
{
    const long SPACE_MILLS = 1000L;
    /// <summary>
    /// 上次刷新时间
    /// </summary>
    long lasttime;
    /// <summary>
    /// 时间
    /// </summary>
    long time;

    /// <summary>
    /// 剩余时间
    /// </summary>
    UnrealTextField text;

    Action<long> callback;

    void Awake()
    {
        this.text = this.GetComponent<UnrealTextField>();
    }
    public void setTime(long time)
    {
        this.time = time + 1000;
        this.callback = null;
        this.enabled = true;
    }
    public void setTime(long time, Action<long> callback)
    {
        this.time = time + 1000;
        this.callback = callback;
        this.enabled = true;
    }
    public override void update(long time)
    {
        if (time - this.lasttime < SPACE_MILLS) return;
        this.lasttime = time;
        long value = this.time - time;
        if (value > 0)
        {
            if (this.text != null)
                this.text.text = TimeKit.getCountdown(value);
        }
        else
        {
            if (this.callback != null)
                this.callback(time);
            this.enabled = false;
        }
    }
}