using cambrian.common;
using System;

namespace cambrian.game
{
    /// <summary>
    /// 服务器地址
    /// </summary>
    [Serializable]
    public class Server
    {
        /* static fields */
        private static Logger log = LogFactory.getLogger<Server>(true);
        /// <summary>
        /// 服务器名字 </summary>
        public string name;
        /// <summary>
        /// 选中的id
        /// </summary>
        public string id;

        /// <summary>
        /// 游戏服务器ip或域名 </summary>
        public string host;

#if UNITY_EDITOR
        [NonSerialized]
        /// <summary> 编辑器使用 </summary>
        public bool open;
#endif

        public string getHost()
        {
            return host;
        }

        /// <summary>
        /// 游戏服务器端口号 </summary>
        public int port;

        public int getGamePort()
        {
            return port;
        }

        /// <summary>
        /// 客户端用到的http服务地址 </summary>
        public string httpserverurl;

        public string getHttPServerUrl()
        {
            return httpserverurl;
        }

        /// <summary>
        /// 回放地址 </summary>
        public string replayurl;


        public string getReplayUrl()
        {
            return replayurl;
        }

        /// <summary>
        /// 微信分享地址 </summary>
        public string shareurl;

        public string getShareUrl()
        {
            return shareurl;
        }

       
        /// <summary>
        /// 普通公告地址
        /// </summary>
        public string noticeurl;
        public string getnoticeUrl()
        {
            return noticeurl;
        }

        /// <summary>
        /// 滚动公告地址
        /// </summary>
        public string noticescorllnotice;

        public string getScorllNotice()
        {
            return noticescorllnotice;
        }

        /// <summary>
        /// dsurl地址
        /// </summary>
        public string dsurl;

        public string getDsUrl()
        {
            return dsurl;
        }


        public override string ToString()
        {
            return "ServerAddress,  name=" + this.name +
                   ", host=" + this.host + ", port=" + this.port + ", httpserverurl=" + this.httpserverurl +
                   ", replayurl=" + this.replayurl + ", shareurl=" + this.shareurl +
                   "]";
        }

        /* inner class */
    }
}