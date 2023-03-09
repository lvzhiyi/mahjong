using System;
using System.Reflection;

namespace cambrian.common
{
    /// <summary>
    /// 用反射给指定对象的变量赋值
    /// </summary>
    public class FieldInfoKit
    {
        /* static fields */

        /* static methods */
        /** 获取所有变量 */
        public static FieldInfo[] getFieldInfo(Type myType)
        {
            FieldInfo[] myFieldInfo = myType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            //FieldInfo[] myFieldInfo = myType.GetFields();
            //UnityEngine.Debug.Log("\nThe fields of " +
            //    "ZombiePath are \n");
            //// Display the field information of FieldInfoClass.
            //for (int i = 0; i < myFieldInfo.Length; i++)
            //{
            //    UnityEngine.Debug.Log(myType.Name+"," + myFieldInfo[i].Name);
            //UnityEngine.Debug.Log("Declaring Type  : " + myFieldInfo[i].DeclaringType);
            //UnityEngine.Debug.Log("IsPublic        : " + myFieldInfo[i].IsPublic);
            //UnityEngine.Debug.Log("MemberType      : " + myFieldInfo[i].MemberType);
            //UnityEngine.Debug.Log("FieldType       : " + myFieldInfo[i].FieldType);
            //UnityEngine.Debug.Log("IsFamily        : " + myFieldInfo[i].IsFamily);
            //}
            return myFieldInfo;
        }
        /** 设置变量值(对象,它所拥有的变量,指定变量名,值) */
        public static void setField(object obj, FieldInfo[] fields, string name, object value)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                if (name.Equals(fields[i].Name))
                {
                    //if (log.isDebug)
                    //    log.debug(" old obj=" + obj + ",name=" + name + ",value=" + fields[i].GetValue(obj));
                    //log.debug(fields[i].Name+","+value+","+fields[i].FieldType);
                    if (fields[i].FieldType == typeof (int))
                        fields[i].SetValue(obj, StringKit.parseInt((string)value));
                    else if (fields[i].FieldType == typeof (int[]))
                    {
                        fields[i].SetValue(obj, StringKit.parseInts((string[]) value));
                    }
                    else if (fields[i].FieldType == typeof (int[][]))
                    {
                        fields[i].SetValue(obj, StringKit.parseIntss((string[][]) value));
                    }
                    else if (fields[i].FieldType == typeof (int[][][]))
                    {
                        fields[i].SetValue(obj, StringKit.parseIntsss((string[][][]) value));
                    }
                    else if (fields[i].FieldType == typeof(long))
                    {
                        fields[i].SetValue(obj, StringKit.parseLong((string)value));
                    }
                    else if (fields[i].FieldType == typeof(long[]))
                    {
                        fields[i].SetValue(obj, StringKit.parseLongs((string[])value));
                    }
                    else if (fields[i].FieldType == typeof(long[][]))
                    {
                        fields[i].SetValue(obj, StringKit.parseLongss((string[][])value));
                    }
                    else if (fields[i].FieldType == typeof(long[][][]))
                    {
                        fields[i].SetValue(obj, StringKit.parseLongsss((string[][][])value));
                    }
                    else if (fields[i].FieldType == typeof (bool))
                        fields[i].SetValue(obj, StringKit.parseBool((string)value));
                    else if (fields[i].FieldType == typeof(bool[]))
                    {
                        fields[i].SetValue(obj, StringKit.parseBools((string[])value));
                    }
                    else if (fields[i].FieldType == typeof(bool[][]))
                    {
                        fields[i].SetValue(obj, StringKit.parseBoolss((string[][])value));
                    }
                    else if (fields[i].FieldType == typeof(bool[][][]))
                    {
                        fields[i].SetValue(obj, StringKit.parseBoolsss((string[][][])value));
                    }
                    else if (fields[i].FieldType == typeof (string))
                        fields[i].SetValue(obj, (string) value);
                    else if (fields[i].FieldType == typeof (string[]))
                    {
                        fields[i].SetValue(obj, (string[]) value);
                    }
                    else if (fields[i].FieldType == typeof (string[][]))
                    {
                        fields[i].SetValue(obj, (string[][]) value);
                    }
                    else if (fields[i].FieldType == typeof (string[][][]))
                    {
                        fields[i].SetValue(obj, (string[][][]) value);
                    }
                    else
                        fields[i].SetValue(obj, value);
                    //if (log.isDebug)
                    //    log.debug(" new obj=" + obj + ",name=" + name + ",value=" + fields[i].GetValue(obj));
                    return;
                }
            }
            //if (log.isDebug)
            //    log.debug(" error obj="+obj+",name="+name+",value="+value);
            throw new Exception("no this field:" + obj+","+name);
        }
    }
}