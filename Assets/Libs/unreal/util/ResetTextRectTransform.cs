using System;
using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
///  重设文本框的大小
/// </summary>
[Hotfix]
public class ResetTextRectTransform : MonoBehaviour
{
    [HideInInspector] public Action<Vector2> callback;
    /// <summary>
    /// 最小高度
    /// </summary>
    [HideInInspector] public float min;

    Text text;
    void Awake()
    {
        UnrealTextField text = this.GetComponent<UnrealTextField>();
        text.resettext = this;
        this.text = this.transform.Find("text").GetComponent<Text>();
        this.enabled = false;
    }

    void Update()
    {
        if (this.text.cachedTextGenerator == null || this.text.cachedTextGenerator.lineCount == 0) return;
        this.enabled = false;
        float value = 0;
        UILineInfo[] infos = this.text.cachedTextGenerator.GetLinesArray();
        for (int i = 0; i < infos.Length; i++)
        {
            value += infos[i].height;
        }

        RectTransform rectTransform = this.GetComponent<RectTransform>();
        Vector2 sizedelta = rectTransform.sizeDelta;
        if (this.min > 0 && min > value) value = min;
        sizedelta.y = value;
        rectTransform.sizeDelta = sizedelta;
        if (this.callback != null) this.callback(sizedelta);
    }
}