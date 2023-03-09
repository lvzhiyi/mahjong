using UnityEngine;
using XLua;

/// <summary>
/// 子相机
/// </summary>
//[ExecuteInEditMode]
[Hotfix]
public class UnrealSubCamera : MonoBehaviour
{
    public UnrealDisplayObject.Layer layer = UnrealDisplayObject.Layer.Default;

    /// <summary>
    /// 要显示的高度
    /// </summary>
    public float height = 960;

    /// <summary>
    /// 要显示的宽度
    /// </summary>
    public float width = 640;

    void Start()
    {
        this.resize();
    }

    public void resize()
    {
       // Camera.main.rect = new Rect(x, 0, 1, 1f);
        Camera camera = this.GetComponent<Camera>();
        if (camera == null)
            camera = this.gameObject.AddComponent<Camera>();
        camera.orthographic = true;
        camera.clearFlags = CameraClearFlags.Depth;
        camera.cullingMask = 1 << (int) this.layer;
        camera.depth = (int) this.layer;
        camera.nearClipPlane = -1;
        camera.farClipPlane = 33;
        camera.orthographicSize = Camera.main.orthographicSize;
        camera.rect = Camera.main.rect;
    }
}