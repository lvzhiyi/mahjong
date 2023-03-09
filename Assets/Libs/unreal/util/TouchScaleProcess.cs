using System;
using UnityEngine;
using XLua;

/// <summary>
/// 手势识别
/// </summary>

[Hotfix]
public class TouchScaleProcess : MonoBehaviour
{
    /// <summary>
    /// 要控制的对象,不设置则为自己
    /// </summary>
    public Transform tran;

    /// <summary>
    /// 缩放最小值
    /// </summary>
    public float minScale = 0.5f;
    /// <summary>
    /// 缩放极限最小值
    /// </summary>
    public float limitMinScale = 0.25f;

    /// <summary>
    /// 缩放最大值
    /// </summary>
    public float maxScale = 1;

    /// <summary>
    /// 缩放极限最大值
    /// </summary>
    public float limitMaxScale = 1.2f;

    /// <summary>
    /// 初始距离,即缩放为1时的距离
    /// </summary>
    float distance;
    /// 当前的缩放值
    /// </summary>
    [HideInInspector] public float scale;

    [HideInInspector] public Action<float> callback;



    void Start()
    {
        if (this.tran == null)
            this.tran = this.transform;
        this.distance = 0;
        this.scale = this.tran.localScale.x;
    }
    /// <summary>
    /// 临时显示为最小缩放值
    /// </summary>
    public void showTempLimitMinScale()
    {
        DisplayKit.setLocalScaleXY(this.tran, this.limitMinScale);
        if (this.callback != null)
            this.callback(this.limitMinScale);
    }
    /// <summary>
    /// 从临时缩放值放回到正常
    /// </summary>
    public void goBackScaleFromTemp()
    {
        DisplayKit.setLocalScaleXY(this.tran,this.scale);
        if (this.callback != null)
            this.callback(this.scale);
    }

    void Update()
    {
        if (Input.touchCount > 1)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            //初始
            if (this.distance == 0)
            {
                this.distance = Vector2.Distance(touch1.position, touch2.position)/this.scale;
                return;
            }
            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float distance = Vector2.Distance(touch1.position, touch2.position);
                float scale = distance/this.distance*this.scale;
                this.distance = distance;
                if (scale > this.limitMaxScale || scale < this.limitMinScale) return;
                if (Mathf.Approximately(this.scale, scale)) return;
                this.scale = scale;
                Vector3 vector3 = this.tran.localScale;
                vector3.x = scale;
                vector3.y = scale;
                this.tran.localScale = vector3;

                if (this.callback != null)
                    this.callback(this.scale);
            }
        }
        else
        {
            this.distance = 0;
            if (this.scale < this.minScale)
            {
                this.scale += 0.01f;
                Vector3 vector3 = this.tran.localScale;
                vector3.x = this.scale;
                vector3.y = this.scale;
                this.tran.localScale = vector3;
                if (this.callback != null)
                    this.callback(this.scale);
            }
            else if (this.scale > this.maxScale)
            {
                this.scale -= 0.01f;
                Vector3 vector3 = this.tran.localScale;
                vector3.x = this.scale;
                vector3.y = this.scale;
                this.tran.localScale = vector3;
                if (this.callback != null)
                    this.callback(this.scale);
            }
        }
    }
}