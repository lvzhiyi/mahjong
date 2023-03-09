using UnityEngine;
using XLua;

/// <summary>
/// 面板出现时缓动
/// </summary>

[Hotfix]
public class PanelPositionTweener : MonoBehaviour
{
    Vector3 start;
    Vector3 end;

    int n = 0;
    
    public void reset()
    {
        this.start = this.transform.position;
        this.start.y = -5;
        this.end = this.transform.position;
        this.end.y = 0;
        this.transform.position = this.start;
        this.n = 25;
        this.enabled = true;
    }
    
    void Update()
    {
        this.transform.Translate(Vector3.up*Time.deltaTime*this.n);
        if(this.n>10)
        this.n -= 1;
        if (this.transform.position.y >= 0)
        {
            this.enabled = false;
            this.transform.position = this.end;
        }
    }
}