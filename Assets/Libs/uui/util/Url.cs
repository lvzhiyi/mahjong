using UnityEngine;
using XLua;

namespace cambrian.uui
{
    /// <summary>
    /// 统一资源定位符
    /// </summary>
    [Hotfix]
    public class Url
    {
        /// <summary>
        /// http协议
        /// </summary>
        public static string HTTP = "http://";
        /// <summary>
        /// https协议
        /// </summary>
        public static string HTTPS = "https://";
        /// <summary>
        /// ftp协议
        /// </summary>
        public static string FTP = "ftp://";
        /// <summary>
        /// file协议
        /// </summary>
        public static string FILE = "file://";
        /// <summary>
        /// res协议
        /// </summary>
        public static string RES = "res://";

        /// <summary>
        /// 协议
        /// </summary>
        public string protocol;
        /// <summary>
        /// 路径
        /// </summary>
        public string path;
        /// <summary>
        /// 后缀
        /// </summary>
        public string suffix;

        public Url(string url, string suffix = null)
        {
            if (url.StartsWith(HTTP)) this.protocol = HTTP;
            else if (url.StartsWith(HTTPS)) this.protocol = HTTPS;
            else if (url.StartsWith(FTP)) this.protocol = FTP;
            else if (url.StartsWith(FILE)) this.protocol = FILE;
            else if (url.StartsWith(RES)) this.protocol = RES;
            else throw new UnityException("LoadFileInfo error protocol ,url=" + url);
            this.path = url.Substring(this.protocol.Length);
            this.suffix = suffix;
        }
        public Url(string protocol, string path, string suffix)
        {
            this.protocol = protocol;
            this.path = path;
            this.suffix = suffix;
        }

        public bool isProtocol(string protocol)
        {
            return this.protocol.Equals(protocol);
        }

        public override string ToString()
        {
            if (this.isProtocol(RES)) return path;
            if (this.suffix == null) return this.protocol + path;
            else return this.protocol + path + "." + this.suffix;
        }
    }
}