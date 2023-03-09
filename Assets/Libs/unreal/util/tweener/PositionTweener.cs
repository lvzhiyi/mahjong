using System;
using UnityEngine;
using XLua;

/// <summary>
/// 缓动-位置
/// </summary>
[Hotfix]
public class PositionTweener : MonoBehaviour
{
    /// <summary>
    /// 当前位置索引
    /// </summary>
    private int index;

    /// <summary>
    /// 路径
    /// </summary>
    private Vector3[] path;

    /// <summary>
    /// 速度,每秒移动的距离
    /// </summary>
    public float speed = 100;

    /// <summary>
    /// 方向
    /// </summary>
    private Vector3 direction;

    /// <summary>
    /// 剩余时间
    /// </summary>
    public float time;

    
    public delegate void CallBack();

    public CallBack callback;



    public delegate void CallObjs(object obj);

    public CallObjs callobjs;

    public delegate void Back2(Transform tr);

    public Back2 back2;


    private object objs;

    void Awake()
    {
        this.enabled = false;
    }

    public void start(Vector3 dest)
    {
        this.start(new Vector3[] {dest}, this.speed);
    }

    public void start(Vector3 dest,float spped, CallBack callback)
    {
        this.start(new Vector3[] {dest}, spped, callback);
    }

    public void start(Vector3 dest, float spped,object obj, CallObjs callback)
    {
        this.callobjs = callback;
        this.objs = obj;
        this.start(new Vector3[] { dest }, spped);
    }

    public void start(Vector3 dest, float spped, object obj, CallObjs callback, Back2 back2)
    {
        this.callobjs = callback;
        this.back2 = back2;
        this.objs = obj;
        this.start(new Vector3[] { dest }, spped);
    }


    public void start(Vector3 dest,float speed,CallBack callback, Back2 back2)
    {
        this.start(new Vector3[] { dest }, speed);
        this.callback = callback;
        this.back2 = back2;
    }

    private void start(Vector3[] path, float speed, CallBack callback)
    {
        this.start(path, speed);
        this.callback = callback;
    }

    private void start(Vector3[] path, float speed)
    {
        this.path = path;
        this.speed = speed;
        this.index = 0;
        this._start(path[this.index]);
        this.enabled = true;
        Application.targetFrameRate = 60;
    }

    private void _start(Vector3 dest)
    {
        this.time = Vector2.Distance(dest, this.transform.localPosition)/this.speed;
        this.direction = ((Vector2) (dest - this.transform.localPosition)).normalized;
    }

    void FixedUpdate()
    {
        this.transform.localPosition += this.direction*this.speed*Time.deltaTime;
      
        this.time -= Time.deltaTime;
        if (this.time <= 0)
        {
            this.time = 0;
            DisplayKit.setLocalXY(this.transform, this.path[this.index].x, this.path[this.index].y);
            this.index++;
            if (this.index >= this.path.Length)
            {
                this.enabled = false;

                if (this.callobjs != null)
                {
                    this.callobjs(objs);
                    callobjs = null;
                }


                if (this.callback != null)
                {
                    this.callback();
                    callback = null;
                }

                this.back2 = null;
            }
            else
            {
                this._start(this.path[this.index]);
            }
        }
        else
        {
            if (this.back2 != null)
            {
                this.back2(this.transform);
            }
        }
    }
}