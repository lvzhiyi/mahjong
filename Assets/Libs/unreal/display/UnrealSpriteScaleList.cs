using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using XLua;

[Hotfix]
public class UnrealSpriteScaleList : MonoBehaviour
{
    public List<Image> sprites;

    /// <summary>
    /// true:变大,false:变小
    /// </summary>
    public bool isBigger;

    /// <summary>
    /// 初始缩放值
    /// </summary>
    public Vector2 initVector;

    /// <summary>
    /// 目标值
    /// </summary>
    public Vector2 targetVector;

    /// <summary>
    /// 是否是显示执行
    /// </summary>
    public bool isEnableExecute;

    /// <summary>
    /// 是否是循环执行
    /// </summary>
    public bool loop;

    /// <summary>
    /// 是否来回播放(eg:变大后，要变小)
    /// </summary>
    public bool isbackAndForth;

    /// <summary>
    /// 间隔时间
    /// </summary>
    public float intervaltime;

    /// <summary>
    /// 缩放时间(单位秒)
    /// </summary>
    public float scaletime;

    /// <summary>
    /// 索引
    /// </summary>
    private int index;

    void Awake()
    {
        if (sprites != null)
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                Image image = sprites[i];
                image.transform.localScale = initVector;
            }
        }
    }

    public void start(int index)
    {
        if (sprites == null) return;
        Image image = sprites[index];
        if (isbackAndForth)
            image.transform.DOScale(targetVector, scaletime).SetRelative().SetLoops(2, LoopType.Yoyo).onComplete = next;
        else
            image.transform.DOScale(targetVector, scaletime).onComplete = next;
    }

    private void next()
    {
        CancelInvoke("invokeExecute");
        sprites[index].transform.localScale = initVector;
        if (index == sprites.Count - 1 && !loop) return;
        if (index == sprites.Count - 1 && loop)
            index = 0;
        else
            index++;
        Invoke("invokeExecute", intervaltime);

    }

    public void invokeExecute()
    {
        start(index);
    }

    /// <summary>
    /// 第一次开始
    /// </summary>
    private bool isStart = false;

    void OnEnable()
    {
        if (isEnableExecute && !isStart)
        {
            isStart = true;
            start(index);
        }
    }

    void OnDisable()
    {

    }
}

