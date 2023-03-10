using System.Xml;
using UnityEngine;

namespace cambrian.common
{
    /**
     * 类说明：加载Resources中的资源
     * 
     * @version 1.0
     * @author maxw<woldits@qq.com>
     */
    public class LoadKit
    {
        /** 日志记录 */
        private static Logger log = LogFactory.getLogger<LoadKit>(false);
//
//        /** 加载指定路径的声音 */
//        public static AudioClip loadAudio(string path)
//        {
//            return (AudioClip)Resources.Load(path, typeof(AudioClip));
//        }

        /** 加载图片Sprite */

        public static Sprite loadSprite(string path)
        {
            if (log.isDebug)
                Debug.Log(log.getMessage(",loadSprite,path=" + path));
            return (Sprite) Resources.Load(path, typeof (Sprite));
        }
        public static Sprite[] loadSprites(string path)
        {
            if (log.isDebug)
                Debug.Log(log.getMessage(",loadSprites,path=" + path));
            object[] objs = Resources.LoadAll(path, typeof (Sprite));
            Sprite[] sprites = new Sprite[objs.Length];
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = (Sprite) objs[i];
            }
            return sprites;
        }
        /** 加载指定路径的图片 */
        public static Texture2D loadImage(string path)
        {
            if (log.isDebug)
                Debug.Log(log.getMessage(",loadImage,path=" + path));
            return (Texture2D) Resources.Load(path, typeof (Texture2D));
        }
        /** 加载指定文件夹中的所有图片 */
        public static Texture2D[] loadImages(string path)
        {
            if (log.isDebug)
                Debug.Log(log.getMessage(",loadImages,path=" + path));
            object[] objs = Resources.LoadAll(path, typeof (Texture2D));
            Texture2D[] imgs = new Texture2D[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                imgs[i] = (Texture2D) objs[i];
            }
            return imgs;
        }
        /** 加载XML */
        public static XmlNode loadXml(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string text = loadText(path);
            if (log.isDebug)
                Debug.Log(log.getMessage(text));
            xmlDoc.LoadXml(text);
            return xmlDoc.DocumentElement;
        }

        public static XmlNode loadXmlText(string text)
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (log.isDebug)
                Debug.Log(log.getMessage(text));
            xmlDoc.LoadXml(text);
            return xmlDoc.DocumentElement;
        }

        /** 加载文本中的字符串 */
        public static string loadText(string path)
        {
            return loadTextAsset(path).text;
        }
        /** 加载文本中的二进制数组 */
        public static ByteBuffer loadByteBuffer(string path)
        {
            return new ByteBuffer(loadBytes(path));
        }
        /** 加载文本中的二进制数组 */
        public static byte[] loadBytes(string path)
        {
            return loadTextAsset(path).bytes;
        }
        /** 加载文本资源 */
        public static TextAsset loadTextAsset(string path)
        {
            TextAsset textAsset = (TextAsset) Resources.Load(path);
            return textAsset;
        }
        /// <summary>
        /// 卸载未使用的资源。有些已加载的资源，最明显的是纹理，即使在场景没有实例，也最占内存。当资源不再需要时，你可以使用Resources.UnloadUnusedAssets回收内存。 
        /// </summary>
        public static void clear()
        {
            Resources.UnloadUnusedAssets();
        }
    }
}