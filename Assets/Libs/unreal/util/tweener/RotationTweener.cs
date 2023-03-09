using System;
using cambrian.common;
using UnityEngine;
using XLua;

/// <summary>
/// 旋转
/// </summary>

[Hotfix]
public class RotationTweener : MonoBehaviour
{
    /// <summary>
    /// 角度
    /// </summary>
    private Vector3 dest;

    /// <summary>
    /// 速度,每秒移动的角度
    /// </summary>
    float speed = 400;

    /// <summary>
    /// 方向
    /// </summary>
    private Vector3 direction;

    /// <summary>
    /// 剩余时间
    /// </summary>
    private float time;

    Action<object> callback;

    private object callbackObj;

    private Action<Transform> back2;

    void Awake()
    {
        this.enabled = false;
    }

    public void start(Vector3 dest, object obj, float speed, Action<object> callback)
    {
        this.dest = dest;
        this.start(speed, obj, callback);
    }

    void start(float speed, object obj, Action<object> callback)
    {

        this.start(speed);
        this.callbackObj = obj;
        this.callback = callback;
    }

    void start(float speed)
    {
        this.speed = speed;
        this.time = MathKit.abs(this.transform.localRotation.eulerAngles.z - dest.z) / this.speed;
        this.direction = (dest - this.transform.localEulerAngles).normalized;
        this.enabled = true;
    }

    void Update()
    {
        this.transform.localEulerAngles += this.direction * this.speed * Time.deltaTime;
        if (this.direction.z == -1&&this.transform.localEulerAngles.z<=MathKit.abs(dest.z))
        {
            this.time = 0;
        }
        else if(this.direction.z!=-1&& this.transform.localEulerAngles.z >= MathKit.abs(dest.z))
        {
            this.time = 0;
        }

        this.time -= Time.deltaTime;
        if (this.time <= 0)
        {
            this.time = 0;
            DisplayKit.setLocalRoateXYZ(this.transform, dest.x, dest.y, dest.z);
            this.enabled = false;
            if (this.callback != null)
                this.callback(callbackObj);
        }
        else
        {
            if (this.back2 != null)
                this.back2(this.transform);
        }
    }
}