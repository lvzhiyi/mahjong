using cambrian.common;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using XLua;
using UnityEngine;

namespace cambrian.uui
{
    /// <summary>
    /// 资源加载器
    /// </summary>
    [Hotfix]
    public class Loader : MonoBehaviour
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        private static common.Logger log = LogFactory.getLogger<Loader>();

        private static Loader loader;

        /// <summary>
        /// 获取资源加载器
        /// </summary>
        /// <returns></returns>
        public static Loader getLoader()
        {
            if (loader == null)
                loader = UnrealRoot.root.gameObject.AddComponent<Loader>();
            return loader;
        }

        /// <summary>
        /// 加载指定路径上的内容
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="action"></param>
        public void load<P, T>(Url url, P param, Action<P, T> action) where T : UnityEngine.Object
        {
            if (log.isDebug) Debug.Log(log.getMessage("load , T=" + typeof (T).Name + " ,url=" + url));
            if (url.isProtocol(Url.HTTP)|| url.isProtocol(Url.HTTPS))
            {
                this.StartCoroutine(wwwLoad<P, T>(url.ToString(), param, (p, obj) => { action(p, (T) obj); }));
            }
            else if (url.isProtocol(Url.RES))
            {
                T t = (T) Resources.Load(url.ToString(), typeof (T));
                action(param, t);
            }
            else
            {
                action(param, null);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        IEnumerator wwwLoad<P, T>(string url, P param, Action<P, object> action)
        {
            using (WWW www = new WWW(url))
            {

                yield return www;
                if (www.error == null)
                {
                    if (typeof (T) == typeof (Texture2D))
                    {
                        if (log.isDebug) Debug.Log(log.getMessage("wwwLoad ok, texture=" + www.texture));
                        action(param, www.texture);
                    }
                    else if (typeof(T) == typeof(Sprite))
                    {
                        Texture2D pic = www.texture;
                        Sprite sprite = Sprite.Create(pic, new Rect(0, 0, pic.width, pic.height),
                            new Vector2(0.5f, 0.5f)); //new Vector2(0.5f, 0.5f)设定精灵的轴心点
                        if (log.isDebug) Debug.Log(log.getMessage("wwwLoad ok, sprite=" + sprite));
                        action(param, sprite);
                    }
                    else
                        action(param,null);
                }
                else
                {
                    if (log.isDebug) Debug.LogWarning(log.getMessage("wwwLoad error, error=" + www.error));
                    action(param, null);
                }
            }
        }


        /// <summary>
        /// 获取文本信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userid"></param>
        /// <param name="action"></param>
        [CSharpCallLua]
        public void loadText(string url, int sid, Action<object, object> action)
        {
            StartCoroutine(getContxt(url, sid, action));
        }

        IEnumerator getContxt(string url, int sid, Action<object, object> action)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();
                if (www.isDone && !www.isNetworkError)
                {
                    string text = www.downloadHandler.text;
                    if (www.responseCode != 200)
                        text = "";
                    action(sid, text);
                }
                else
                {
                    action(null, null);
                }
            }
        }

        /// <summary>
        /// 请求二进制数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        [CSharpCallLua]
        public void loadBytes(string url, Action<byte[]> action)
        {
            StartCoroutine(getBytes(url, action));
        }

        IEnumerator getBytes(string url, Action<byte[]> action)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();
                if (www.isDone && !www.isNetworkError)
                    action(www.downloadHandler.data);
                else
                    action(null);
            }
        }


        /// <summary>
        /// 加载指定路径上的多个内容
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="action"></param>
        public void loads<P, T>(Url url, P param, Action<P, T[]> action) where T : UnityEngine.Object
        {
            if (log.isDebug) Debug.Log(log.getMessage("loads , T=" + typeof (T).Name + " ,url=" + url));
            if (url.isProtocol(Url.HTTP)|| url.isProtocol(Url.HTTPS))
            {
                throw new UnityException("LoadKit loads, 暂未实现");
            }
            else if (url.isProtocol(Url.RES))
            {
                object[] objs = Resources.LoadAll(url.ToString(), typeof (T));
                T[] ts = new T[objs.Length];
                for (int i = 0; i < ts.Length; i++)
                {
                    ts[i] = (T) objs[i];
                }
                action(param, ts);
            }
            else
            {
                action(param,null);
            }
        }

        [CSharpCallLua]
        public void post(string url, ByteBuffer data, Action<object> action)
        {
            StartCoroutine(post_send(action, url, data.toArray()));
        }

        IEnumerator post_send(Action<object> obj, string url, byte[] data)
        {
            using (UnityWebRequest www = UnityWebRequest.Put(url, data))
            {
                yield return www.SendWebRequest();
                if (www.isDone && !www.isNetworkError)
                {
                    byte[] results = www.downloadHandler.data;
                    obj(results);
                }
                else
                {
                    obj(null);
                }
            }
        }

        /// <summary>
        /// 卸载Resources(res://)的未使用的资源。有些已加载的资源，最明显的是纹理，即使在场景没有实例，也最占内存。当资源不再需要时，你可以使用Resources.UnloadUnusedAssets回收内存。
        /// </summary>
        public static void unloadUnusedAssets()
        {
            Resources.UnloadUnusedAssets();
        }
    }
}