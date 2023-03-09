using UnityEngine;
using XLua;

/// <summary>
/// 自适应屏幕高度
/// </summary>
[ExecuteInEditMode]
[Hotfix]
public class UnrealCamera : MonoBehaviour
{
    [HideInInspector] public static UnrealCamera main;
    //    /// <summary>
    //    /// 自适应高度
    //    /// </summary>
    //    public bool isheight;

    /// <summary>
    /// 游戏逻辑中的高度
    /// </summary>
    public float height = 640;

    /// <summary>
    /// 游戏逻辑中的宽度
    /// </summary>
    public float width = 1136;

    /// <summary>
    /// 适配
    /// </summary>
    public static int adaptersX = 0;

    /// <summary>
    /// 运行时自适应后相对高度
    /// </summary>
    [HideInInspector] public float _height = 640;

    /// <summary>
    /// 运行时自适应后相对宽度
    /// </summary>
    [HideInInspector] public float _width = 960;

    /// <summary>
    /// 空白高度*0.5f
    /// </summary>
    public float spaceHeight
    {
        get { return this._spaceHeight; }
    }

    float _spaceHeight;

    /// <summary>
    /// 空白宽度*0.5f
    /// </summary>
    public float spaceWidth
    {
        get { return this._spaceWidth; }
    }

    float _spaceWidth;

    //    void Start()
    //    {
    //        resize();
    //    }

#if UNITY_EDITOR
    void Update()
    {
        this.resize();
    }
#endif

    public void resize()
    {
        main = this;

        //        bool isheight = Screen.width > Screen.height;
        //        if (isheight)
        //        {
        //            Camera.main.orthographicSize = this.height*0.5f*0.01f;
        //            this._spaceWidth = (this.height*(Screen.width*1.0f/Screen.height) - this.width)*0.5f*0.01f;
        //            this._spaceHeight = 0;
        //            this._width = this.height*(Screen.width*1.0f/Screen.height);
        //            this._height = this.height;
        //        }
        //        else
        {
            if (Screen.width * 1.0f / Screen.height <= 1.61f)
            {
                Camera.main.orthographicSize = Screen.height * this.width / (Screen.width - adaptersX) * 0.5f * 0.01f;
                this._spaceWidth = 0;
                this._spaceHeight = (this.width * (Screen.height * 1.0f / (Screen.width - adaptersX)) - this.height) * 0.5f * 0.01f;
                this._width = this.width;
                this._height = this.width * (Screen.height * 1.0f / (Screen.width - adaptersX));
            }
            else
            {
                //Camera.main.orthographicSize = Screen.width * this.height / (Screen.height) * 0.5f * 0.01f;
                Camera.main.orthographicSize = 3.2f;
                this._spaceWidth = (this.height * ((Screen.width - adaptersX) * 1.0f / Screen.height) - this.width) * 0.5f * 0.01f;
                this._spaceHeight = 0;
                this._width = this.height * ((Screen.width - adaptersX) * 1.0f / Screen.height);
                this._height = this.height;
            }
        }
    }



    /// <summary>
    /// 获取分辨率相对于1136，需要缩放的值，达到1136填充的显示效果
    /// </summary>
    /// <returns></returns>
    public float getScaleX()
    {
        //视截体高度 = camera.size
        //视截体宽度 = 视截体高度 * (screenWidth / screenHeight) * (camera.viewportRect.W / camera.viewportRect.H)
//        float defaultwidth = 3.2f*(width / height);
//        float screenwidth = 3.2f * (Screen.width / (Screen.height*1f));
//        float value = screenwidth - defaultwidth;

        //Debug.Log("dsfdsfds="+(_width/ width));

       // Debug.Log("value="+value+",="+screenwidth+",defwidth="+defaultwidth);
        float value = (_width / width)-1;
        if (value <= 0)
            return 0;
        value = ((int)(value * 100))*0.01f;
        return value;
    }
}