using cambrian.common;
using System.Collections;
using System.IO;
using UnityEngine;

namespace scene.game
{
    /// <summary>
    /// 资源加载模块
    /// </summary>
    public class ResLoadModule : Module
    {
        //--------------------------必须加载项---------------------------------
        public string soundname = "sound";
        /// <summary>
        /// 新增的图片,必须加载，否则将找不到。
        /// </summary>
        public string imgname = "imgs";
        /// <summary>
        /// 声音
        /// </summary>
        AssetBundle sound { get; set; }

        public void loadSound(string name)
        {
            if (name == null) return;
            if (sound == null)
            {
                sound = AssetBundle.LoadFromFile(CacheLocalPath.AB_RESROUCE_PATH + name);
            }
        }

        public AudioClip playSound(string path)
        {
            if (sound == null) return null;
            return sound.LoadAsset<AudioClip>(path);
        }

        /// <summary>
        /// 加载图片资源
        /// </summary>
        /// <param name="name"></param>
        public void loadImgRes(string[] name)
        {
            if (name == null) return;
            for (int i = 0; i < name.Length; i++)
               AssetBundle.LoadFromFile(CacheLocalPath.AB_RESROUCE_PATH + name[i]);
        }


        //----------------以下内容根据具体要求来加载-----------------

        public string prefabPointPath = "prefabpath";

        private PrefabPointData prefabPointData;
        public void loadPrefabPoints(string name)
        {
            prefabPointData = null;
            if (name == null) return;
            string path = CacheLocalPath.AB_RESROUCE_PATH + name;
            if (File.Exists(path))
            {
                string text=FileKit.readText(path);
                prefabPointData = JsonUtility.FromJson<PrefabPointData>(text);
                JsonUtility.FromJsonOverwrite(text, prefabPointData);
            }
        }

        /// <summary>
        /// 资源包
        /// </summary>
        private Hashtable abs=new Hashtable();

        public GameObject load(string name)
        {
            if (prefabPointData == null) return null;
            string abName=prefabPointData.getABName(name);
            if (abName == null) return null;

            if (abs[abName] == null)
            {
                AssetBundle bundle = AssetBundle.LoadFromFile(CacheLocalPath.AB_RESROUCE_PATH + abName);
                if (bundle != null)
                {
                    abs[abName] = bundle;
                    GameObject obj = bundle.LoadAsset<GameObject>(name);
                    if (null != obj)
                        return obj;
                }
            }
            else
            {
                AssetBundle prefab = (AssetBundle)abs[abName];
                if (prefab != null)
                {
                    GameObject obj = prefab.LoadAsset<GameObject>(name);
                    if (null != obj)
                        return obj;
                }
            }
            return null;
        }

    }
}
