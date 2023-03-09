using System;
using cambrian.common;
using UnityEngine;
using XLua;

/// <summary>
/// 数字框,必须要调用setRange函数
/// </summary>
[Hotfix]
public class UnrealNumberField : UnrealTextField
{
    long _min, _max;

    long _value;
//    /// <summary>
//    /// 是否检查范围
//    /// </summary>
//    [HideInInspector] public bool check=true;

    [HideInInspector] public Action<long> callback;
    /// <summary>
    /// 后缀
    /// </summary>
    [HideInInspector] public string suffix="";
    public bool outofrange
    {
        get
        {
            return (value > this._max)||(value < this._min);
        }
    }
    public long min
    {
        get { return _min; }
    }
    public long max
    {
        get { return _max; }
    }
    public long value
    {
        get { return _value; }
        set
        {
            this._setValue(value);
        }
    }

    /// <summary>
    /// 初始化时设置范围
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="value"></param>
    public void setRange(long min, long max, long value)
    {
        this._min = min;
        this._max = max;
        this._setValue(value);
    }
    /// <summary>
    /// 设置值
    /// </summary>
    /// <param name="value"></param>
    private void _setValue(long value)
    {
//        if (check)
//        {
//            if (value > this._max) value = this._max;
//            else if (value < this._min) value = this._min;
//        }
        this._value = value;
        this.text = StringKit.toStringMoney(this._value) + suffix;
    }
    /// <summary>
    /// 清理
    /// </summary>
    public void clear()
    {
        this._min = 0;
        this._max = 0;
        this._value = 0;
        this.text = "";
        this.textInfo = "";
    }
}