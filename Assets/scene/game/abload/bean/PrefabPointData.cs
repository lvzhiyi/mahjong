using System;
using UnityEngine;

namespace scene.game
{
    /// <summary>
    /// 预制件指向的资源包
    /// </summary>
    [Serializable]
    public class PrefabPointData
    {
        [SerializeField]
        public PrefabPoint[] data;

        public string getABName(string prefabname)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (prefabname==data[i].name)
                    return data[i].abname;
            }
            return null;
        }
    }
}
