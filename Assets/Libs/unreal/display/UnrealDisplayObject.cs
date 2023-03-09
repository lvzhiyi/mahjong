using cambrian.common;
using UnityEngine;
using XLua;

/// <summary>
/// 显示对象的基类，既然是显示对象类中就不要有process、command
/// </summary>
[Hotfix]
public class UnrealDisplayObject : MonoBehaviour
{
    /// <summary>
    /// 是否需要显示，不受关闭其他面板的影响
    /// </summary>
    public bool isShow1 = false;

    /// <summary>
    /// 该脚本的GameObject是否活动
    /// </summary>
    /// <param name="behaviour"></param>
    /// <returns></returns>
    public static bool getVisible(MonoBehaviour behaviour)
    {
        return behaviour.gameObject.activeSelf;
    }
    /// <summary>
    /// 该脚本的GameObject活动状态,是否活动还受限于其父对象的状态
    /// </summary>
    /// <param name="behaviour"></param>
    /// <param name="b"></param>
    public static void setVisible(MonoBehaviour behaviour, bool b)
    {
        behaviour.gameObject.SetActive(b);
    }

    public enum Layer
    {
        Default = 0, // 默认层 
        Default_1 = 1, // 默认层
        Default_2 = 2, // 默认层
        Default_3 = 3, // 默认层
        Default_4 = 4, // 默认层
        Default_5 = 5, // 默认层

        //8
        Tab_1 = 9, //页卡层1
        Panel_1 = 10, // 面板层1
        //14
        //15
        Tab_2 = 16, //页卡层2
        Panel_2 = 17, // 面板层2
        //21
        //22
        Tab_3 = 23, //页卡层3
        Panel_3 = 24, // 面板层3
        Default_28 = 28, //28层
        Loading = 29, //加载显示层
        Alert = 30, //提示框层
        Top = 31, //最顶层
    }

    /// <summary>
    /// 日志
    /// </summary>
    protected static cambrian.common.Logger log = LogFactory.getLogger<UnrealDisplayObject>(true);

    /// <summary>
    /// 设置UI
    /// </summary>
    public virtual void init()
    {

    }

    /// <summary>
    /// 根据当前数据刷新UI
    /// </summary>
    public virtual void refresh()
    {

    }
    /// <summary>
    /// 更新,需外部驱动
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public virtual long base_update(long time)
    {
        return -1;
    }

    void Update()
    {
        update();
    }

    public virtual void update()
    {
    }
    /// <summary>
    /// 重新排版
    /// </summary>
    public virtual void relayout()
    {

    }
    /// <summary>
    /// 聚焦obj到点value
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="value"></param>
    public virtual void focus(UnrealDisplayObject obj, Vector3 value)
    {

    }

    public enum Layout
    {
        Width = 1,
        Height = 2,
    }

    public enum SizeDeltaLayer
    {
        Null = 0,
        Width = 1,
        Height = 2,
        WidthAndHeight = 3,
    }
    public void setLocalRotation(float value)
    {
        Vector3 point = this.transform.localEulerAngles;
        point.z = value;
        this.transform.localEulerAngles = point;
    }
    public void setLocalX(float x)
    {
        Vector3 point = this.transform.localPosition;
        point.x = x;
        this.transform.localPosition = point;
    }
    public void setLocalY(float y)
    {
        Vector3 point = this.transform.localPosition;
        point.y = y;
        this.transform.localPosition = point;
    }
    public void setLocalZ(float z)
    {
        Vector3 point = this.transform.localPosition;
        point.z = z;
        this.transform.localPosition = point;
    }

    public void setLocalXY(float x, float y)
    {
        Vector3 point = this.transform.localPosition;
        point.x = x;
        point.y = y;
        this.transform.localPosition = point;
    }
    public void setLocalXYZ(float x, float y, float z)
    {
        Vector3 point = this.transform.localPosition;
        point.x = x;
        point.y = y;
        point.z = z;
        this.transform.localPosition = point;
    }
    public void setLocalVector3(Vector3 vector3)
    {
        this.transform.localPosition = vector3;
    }
    public void setX(float x)
    {
        Vector3 point = this.transform.position;
        point.x = x;
        this.transform.position = point;
    }
    public void setY(float y)
    {
        Vector3 point = this.transform.position;
        point.y = y;
        this.transform.position = point;
    }
    public void setZ(float z)
    {
        Vector3 point = this.transform.position;
        point.z = z;
        this.transform.position = point;
    }

    public void setXY(float x, float y)
    {
        Vector3 point = this.transform.position;
        point.x = x;
        point.y = y;
        this.transform.position = point;
    }
    public void setXYZ(float x, float y, float z)
    {
        Vector3 point = this.transform.position;
        point.x = x;
        point.y = y;
        point.z = z;
        this.transform.position = point;
    }

    public void setVector3(Vector3 vector3)
    {
        this.transform.position = vector3;
    }
    /// <summary>
    /// 逻辑上是否显示。因绘制原因 getVisible和其并不同步
    /// </summary>
    [HideInInspector] public bool visible = true;
    /// <summary>
    /// 显示上可见
    /// </summary>
    /// <returns></returns>
    public bool getVisible()
    {
//        return visible;
        return this.gameObject.activeSelf;
    }

    public virtual void setVisible(bool b)
    {
        this.visible = b;
        this.gameObject.SetActive(b);
    }

    public virtual void OnDestroy()
    {

    }
}