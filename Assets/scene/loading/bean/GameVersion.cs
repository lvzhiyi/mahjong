using System;

namespace scene.loading
{
    /// <summary> 游戏版本 </summary>
    [Serializable]
    public class GameVersion
    {
        /// <summary> 下载安装包信息 </summary>
        public string downInfoUrl;
        /// <summary> 安卓版本 </summary>
        public PlatformVersion android;
        /// <summary> ios版本 </summary>
        public PlatformVersion ios;

        public string getDownInfoUrl()
        {
            return downInfoUrl;
        }

        public override string ToString()
        {
            return string.Concat("android = ", android.ToString(), " , ios = ", ios.ToString());
        }
    }
}
