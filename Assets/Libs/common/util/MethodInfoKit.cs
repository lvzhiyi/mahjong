using System;
using System.Reflection;

namespace cambrian.common
{
    /// <summary>
    /// 用反射给执行指定对象的方法
    /// </summary>
    public class MethodInfoKit
    {
        /** 获取所有方法 */
        public static MethodInfo[] getMethodInfo(Type myType)
        {
            MethodInfo[] myMethodInfo = myType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            //for (int i = 0; i < myMethodInfo.Length; i++)
            //{
            //    UnityEngine.Debug.Log(myType.Name + "," + myMethodInfo[i].Name);
            //    UnityEngine.Debug.Log("Declaring Type  : " + myMethodInfo[i].DeclaringType);
            //    UnityEngine.Debug.Log("IsPublic        : " + myMethodInfo[i].IsPublic);
            //    UnityEngine.Debug.Log("MemberType      : " + myMethodInfo[i].MemberType);
            //    UnityEngine.Debug.Log("IsFamily        : " + myMethodInfo[i].IsFamily);
            //}
            return myMethodInfo;
        }
        /** 执行方法(对象,它所拥有的方法,指定方法名,方法参数) */
        public static void setMethodField(object obj, MethodInfo[] methods, string name, object value)
        {
            for (int i = 0; i < methods.Length; i++)
            {
                if (name.Equals(methods[i].Name))
                {
                    methods[i].Invoke(obj, new object[] {value});
                    //if (log.isDebug)
                    //    log.debug(methods[i].Name + "," + value);
                    return;
                }
            }
            //if (log.isDebug)
            //    log.debug(" error obj=" + obj + ",name=" + name + ",value=" + value);
            throw new Exception("no this method:" + obj + "," + name);
        }
    }
}