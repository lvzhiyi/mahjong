using System;
using cambrian.common;
using UnityEngine;
using XLua;

/// <summary>
/// 缩放(暂时变大)
/// </summary>

[Hotfix]
public class ScaleTweener : MonoBehaviour
{ 
    void Awake()
    {
        this.enabled = false;
    }

    /// <summary>
    /// 缩放比例
    /// </summary>
    public float max=1.05f;

    /// <summary>
    /// 速度
    /// </summary>
    float speed = 0.04f;

    float cur = 1f;

    private bool b = false;

    private bool isDa;


    Action<object> callback;

    private object obj;

    public void start(float min,float max,object obj,float speed,Action<object> callback)
    {
        this.callback = callback;
        this.max = max;
        this.callback = callback;
        this.obj = obj;
        this.cur = min;
        this.speed = speed;
        this.enabled = true;
        b = true;
    }

    private long time;

    void Update()
    {
        if (!b) return;

        long cur_time = TimeKit.currentTimeMillis;
        if (time == 0)
            time = cur_time;

        if (cur_time - time > 20)
        {
            time = cur_time;
            cur += this.speed;
            if (cur > max)
            {
                cur = max;
                this.enabled = false;
                b = false;
                if (callback != null)
                    callback(obj);
            }
            this.transform.localScale = new Vector3(cur, cur, 1);
        }
    }
}