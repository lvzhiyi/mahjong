using UnityEngine;
using XLua;

/// <summary>
/// 设备分辨率和游戏逻辑分辨率比例不一样时，自动适应屏幕排版,直接改变了其坐标，不能在游戏中通过transform获取及设置其坐标
/// </summary>
[Hotfix]
public class AutoLayout : MonoBehaviour
{
    public enum Layout
    {
        Top,
        Middle,
        Bottom,
        Left,
        Center,
        Right,
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
    }

    public Layout layout = Layout.MiddleCenter;
    /// <summary>
    /// 其对应gameObject的坐标,设置后要手动调用reLayout();
    /// </summary>
    private float x;
    /// <summary>
    /// 其对应gameObject的坐标,设置后要手动调用reLayout();
    /// </summary>
    private float y;

    UnrealCamera unrealCamera;

    void Start()
    {
        this.unrealCamera = Camera.main.GetComponent<UnrealCamera>();
        if (this.unrealCamera == null)
            throw new UnityException(",必须在根节点上加UnrealCamera");
        this.x = this.transform.position.x;
        this.y = this.transform.position.y;
        this.reLayout();
    }

#if UNITY_EDITOR
    void Update()
    {
        this.reLayout();
    }
#endif

    public void reLayout()
    {
        Vector3 point = this.transform.localPosition;
        switch (this.layout)
        {
            case Layout.Top:
                point.y = this.y + this.unrealCamera.spaceHeight;
                break;
            case Layout.Middle:
                point.y = this.y;
                break;
            case Layout.Bottom:
                point.y = this.y - this.unrealCamera.spaceHeight;
                break;
            case Layout.Left:
                point.x = this.x - this.unrealCamera.spaceWidth;
                break;
            case Layout.Center:
                point.x = this.x;
                break;
            case Layout.Right:
                point.x = this.x + this.unrealCamera.spaceWidth;
                break;
            case Layout.TopLeft:
                point.x = this.x - this.unrealCamera.spaceWidth;
                point.y = this.y + this.unrealCamera.spaceHeight;
                break;
            case Layout.TopCenter:
                point.x = this.x;
                point.y = this.y + this.unrealCamera.spaceHeight;
                break;
            case Layout.TopRight:
                point.x = this.x + this.unrealCamera.spaceWidth;
                point.y = this.y + this.unrealCamera.spaceHeight;
                break;
            case Layout.MiddleLeft:
                point.x = this.x - this.unrealCamera.spaceWidth;
                point.y = this.y;
                break;
            case Layout.MiddleCenter:
                point.x = this.x;
                point.y = this.y;
                break;
            case Layout.MiddleRight:
                point.x = this.x + this.unrealCamera.spaceWidth;
                point.y = this.y;
                break;
            case Layout.BottomLeft:
                point.x = this.x - this.unrealCamera.spaceWidth;
                point.y = this.y - this.unrealCamera.spaceHeight;
                break;
            case Layout.BottomCenter:
                point.x = this.x;
                point.y = this.y - this.unrealCamera.spaceHeight;
                break;
            case Layout.BottomRight:
                point.x = this.x + this.unrealCamera.spaceWidth;
                point.y = this.y - this.unrealCamera.spaceHeight;
                break;
            default:
                break;
        }
        this.transform.localPosition = point;
    }
}