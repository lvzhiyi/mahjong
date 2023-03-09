using System;
using UnityEngine;

namespace scene.game
{
    [Serializable]
    public class PrefabPoint
    {
        /// <summary>
        /// 预制件名称
        /// </summary>
        [SerializeField]
        public string name;
        /// <summary>
        /// 预制件对应的资源包名
        /// </summary>
        [SerializeField]
        public string abname;
    }
}
