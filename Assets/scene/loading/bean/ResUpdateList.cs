using cambrian.common;
using System;

namespace scene.loading
{
    [Serializable]
    public class ResUpdateList : BytesSerializable
    {
        public ResData[] resdata;

        public string[] delteresdata;
    }

    [Serializable]
    public class ResData : BytesSerializable
    {
        /// <summary>
        /// 包名
        /// </summary>
        public string packname;
        /// <summary>
        /// 下载地址
        /// </summary>
        public string url;

        public string getUrl()
        {
            return url;
        }
    }
}
