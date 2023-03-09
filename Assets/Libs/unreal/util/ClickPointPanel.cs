using cambrian.unreal.display;
using UnityEngine;
using XLua;

/// <summary>
/// 点击位置显示
/// </summary>
[Hotfix]
public class ClickPointPanel : UnrealLuaPanel
{

    public static ClickPointPanel panel;

    Transform container;

    protected override void xinit()
    {
        base.xinit();
        this.container = this.transform.Find("Canvas").Find("container");
        this.container.gameObject.AddComponent<CanvasGroup>().blocksRaycasts = false;
        this.container.gameObject.SetActive(false);
        panel = this;
    }

    /// <summary>
    /// 速度
    /// </summary>
    public float speed = 0.05f;
    public void reset()
    {
        Vector2 vector2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.container.position = new Vector3(vector2.x, vector2.y, this.container.position.z);
        this.container.localScale = new Vector3(1, 1, 1);
        this.container.gameObject.SetActive(true);
    }

    protected override void xupdate()
    {
        if (Input.GetMouseButtonDown(0)) this.reset();
        if (!this.container.gameObject.activeSelf) return;
        float value = Time.deltaTime * this.speed;
        this.container.localScale += new Vector3(value, value, 0);
        if (this.container.localScale.x > 1.2f)
        {
            this.container.gameObject.SetActive(false);
        }
    }
}
