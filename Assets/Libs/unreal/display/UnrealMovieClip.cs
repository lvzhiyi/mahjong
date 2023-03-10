using UnityEngine;
using XLua;

/// <summary>
/// 控制动画播放
/// </summary>
[Hotfix]
public class UnrealMovieClip : MonoBehaviour
{
    /// <summary>
    /// 动画绘制者
    /// </summary>
    public Animator animator;

    public bool isStop;

    void Awake()
    {
        this.animator = this.GetComponent<Animator>();
    }

    public void gotoAndPlay(string label)
    {
        this.animator.speed = 1;
        if (label != null)
            this.animator.Play(label);
    }

    public void gotoAndPlay(int index)
    {
        this.animator.speed = 1; 
        this.animator.Play(index);
    }

    public void stop()
    {
        this.animator.speed = 0;
    }
}
