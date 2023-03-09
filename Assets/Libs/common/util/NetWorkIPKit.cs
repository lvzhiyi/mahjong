using System;
using System.Collections;
using UnityEngine;

namespace cambrian.common
{
    public class NetWorkIPKit:MonoBehaviour
    {
        /// <summary>
        /// 获取外网ip
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public  void getOutIp(Action<string> obj)
        {
            StartCoroutine(getIP()); 
        }

        IEnumerator getIP()
        {
            WWW www = new WWW("http://pv.sohu.com/cityjson");
            yield return www;
            if (www.isDone || www.error == null)
            {
                string text = www.text;
            }
        }


    }
}
