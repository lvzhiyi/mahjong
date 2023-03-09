using System;
using cambrian.common;
using cambrian.game;

namespace scene.game
{
    /// <summary>
    /// 地区
    /// </summary>
    [Serializable]
    public class Region:BytesSerializable
    {
        /// <summary>
        /// 地区id
        /// </summary>
        public int id;
        /// <summary>
        /// 创建规则对应的路径(比如创建绵阳地区的规则，该值对应的是绵阳规则lua中的方法名)
        /// </summary>
        public string newrulepath;
        /// <summary>
        /// 服务器维护公告地址
        /// </summary>
        public string noticeurl;
        /// <summary>
        /// 配置表指向路径
        /// </summary>
        public string cfgpath;

        /// <summary>
        /// 加载每个地区对应的模块
        /// </summary>
        public ModuleManager module;

        public string getNoticeUrl()
        {
            return noticeurl;
        }

       
        /// <summary>
        /// 配置表地址
        /// </summary>
        public string[] samplesurl;
        /// <summary>
        /// 服务器组
        /// </summary>
        public ServerInfos server;
        /// <summary>
        /// 游戏种类
        /// </summary>
        public GameType[] gametype;

        /// <summary>
        /// 加载地区
        /// </summary>
        public void load()
        {
            module=new ModuleManager(cfgpath);
        }

    }
}
