using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 闪烁
/// </summary>

[Hotfix]
public class SimpleAlphaTweener : MonoBehaviour
{
    /// <summary>
    /// 透明度比例
    /// </summary>
    public float min = 0.6f, max = 1f;

    /// <summary>
    /// 速度
    /// </summary>
    public float speed = 1f;

    float cur = 1;
    bool b = true;

    Image img;

    void Update()
    {
        float value;
        if (b) value = cur + ((max - min) * Time.deltaTime * this.speed);
        else value = cur - ((max - min) * Time.deltaTime * this.speed);
        if (value > max)
        {
            b = false;
            cur = max;
        }
        else if (value < min)
        {
            b = true;
            cur = min;
        }
        else cur = value;

        if (this.img == null) this.img = this.GetComponent<Image>();
        Color a = this.img.color;
        a.a = cur;
        this.img.color = a;
    }
}