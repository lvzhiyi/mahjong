using UnityEngine;
using XLua;

[Hotfix]
public class DisplayKit
{

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

    public static void setLocalX(GameObject obj, float x)
    {
        Vector3 point = obj.gameObject.transform.localPosition;
        point.x = x;
        obj.gameObject.transform.localPosition = point;
    }
    public static void setLocalX(UnrealDisplayObject obj, float x)
    {
        Vector3 point = obj.transform.localPosition;
        point.x = x;
        obj.transform.localPosition = point;
    }
    public static void setLocalY(GameObject obj, float y)
    {
        Vector3 point = obj.transform.localPosition;
        point.y = y;
        obj.transform.localPosition = point;
    }
    public static void setLocalY(MonoBehaviour behaviour, float y)
    {
        Vector3 point = behaviour.transform.localPosition;
        point.y = y;
        behaviour.transform.localPosition = point;
    }
    public static void setLocalY(UnrealDisplayObject obj, float y)
    {
        Vector3 point = obj.gameObject.transform.localPosition;
        point.y = y;
        obj.gameObject.transform.localPosition = point;
    }
    public static void setLocalXY(Transform tran, float x, float y)
    {
        Vector3 point = tran.localPosition;
        point.x = x;
        point.y = y;
        tran.localPosition = point;
    }
    public static void setLocalXY(GameObject obj, float x, float y)
    {
        Vector3 point = obj.transform.localPosition;
        point.x = x;
        point.y = y;
        obj.transform.localPosition = point;
    }
    public static void setLocalXYZ(GameObject obj, float x, float y, float z)
    {
        Vector3 point = obj.transform.localPosition;
        point.x = x;
        point.y = y;
        point.z = z;
        obj.transform.localPosition = point;
    }

    public static void setLocalVector2(GameObject obj, Vector3 vector3)
    {
        setLocalXY(obj, vector3.x, vector3.y);
    }
    public static void setX(GameObject obj, float x)
    {
        Vector3 point = obj.transform.position;
        point.x = x;
        obj.transform.position = point;
    }
    public static void setY(GameObject obj, float y)
    {
        Vector3 point = obj.transform.position;
        point.y = y;
        obj.transform.position = point;
    }
    public static void setXY(GameObject obj, float x, float y)
    {
        Vector3 point = obj.transform.position;
        point.x = x;
        point.y = y;
        obj.transform.position = point;
    }
    public static void setZ(GameObject obj, float z)
    {
        Vector3 point = obj.transform.position;
        point.z = z;
        obj.transform.position = point;
    }
    public static void setLocalZ(GameObject obj, float z)
    {
        Vector3 point = obj.transform.localPosition;
        point.z = z;
        obj.transform.localPosition = point;
    }

    public static void setLocalScaleXY(Transform tran, float value)
    {
        Vector3 scale = tran.localScale;
        scale.x = value;
        scale.y =value;
        tran.localScale = scale;
    }

    public static void setLocalScaleX(Transform tran, float value)
    {
        Vector3 scale = tran.localScale;
        scale.x = value;
        tran.localScale = scale;
    }

    /// <summary>
    /// 设置宽度高度
    /// </summary>
    public static void setWH(Transform tran, float width,float height)
    {
        RectTransform rect= tran.GetComponent<RectTransform>();
        rect.sizeDelta=new Vector2(width,height);
    }

    public static void setLocalRoateXYZ(Transform tran,float x,float y,float z)
    {
        Vector3 roate = tran.localEulerAngles;
        roate.x = x;
        roate.y = y;
        roate.z = z;
        tran.localEulerAngles = roate;
    }

    public static void setLocalRoateZ(Transform tran,float z)
    {
        Vector3 roate = tran.localEulerAngles;
        roate.z = z;
        tran.localEulerAngles = roate;
    }
}