using System;
using cambrian.common;
using UnityEngine;
using UnityEngine.UI;
using XLua;

[Hotfix]
public class AlphaTweener : MonoBehaviour
{
    /// <summary>
    /// 缓动曲线
    /// </summary>
    public static float[][] TWEENER = {
        new float[]{1,1,1,1,1,1,1,1,1,1,1,1,1,0.9f,0.85f,0.8f,0}
    };

    public int t_index = 0;
    public float space = 40;

    long lasttime;
    int index = 0;

    private Action<object> back; 

    public void reset()
    {
        foreach (Transform child in transform)
        {
            Color a = child.GetComponent<Image>().color;
            a.a = 1;
            child.GetComponent<Image>().color = a;
            index = 0;
        }
    }

    public void reset_call_back(Action<object> call)
    {
        this.back = call;
        reset();
    }

    public void resetColor()
    {
        foreach (Transform child in transform)
        {
            Color a = child.GetComponent<Image>().color;
            a.a = 1;
            child.GetComponent<Image>().color = a;
            index = TWEENER[t_index].Length;
        }
    }


    void Update()
    {
        long time = TimeKit.currentTimeMillis;
        if (time < 0) return;
        if (time - this.lasttime < space) return;
        this.lasttime = time;
        index++;
        if (index >= TWEENER[t_index].Length) return;
        foreach (Transform child in transform)
        {
            Color a = child.GetComponent<Image>().color;
            a.a = TWEENER[t_index][index];
            child.GetComponent<Image>().color = a;
        }

        if (index==TWEENER[0].Length-1)
        {
            if (this.back != null)
            {
                this.back(null);
                this.back = null;
            }
        }
    }
}