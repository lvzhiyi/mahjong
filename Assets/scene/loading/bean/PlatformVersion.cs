using cambrian.game;
using System;

namespace scene.loading
{
    /// <summary> 平台版本 </summary>
    [Serializable]
    public class PlatformVersion
    {
        /// <summary> 版本号 </summary>
        public string version;

        /// <summary> 资源更新地址 </summary>
        public string url;

        public string getUrl()
        {
            return url;
        }

        /// <summary> 配置文件指向的路径 </summary>
        public string cfgurl;

        /// <summary> 安装包下载地址 </summary>
        public string downurl;

        public override string ToString()
        {
            return string.Concat("version = ", version, " , url = " + url);
        }
    }
}
