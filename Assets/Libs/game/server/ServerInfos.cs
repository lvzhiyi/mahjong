using cambrian.common;
using System;

namespace cambrian.game
{
    /// <summary>
    /// 服务器地址信息
    /// </summary>
    [Serializable]
    public class ServerInfos
    {
        /* static fields */
        private static cambrian.common.Logger log = LogFactory.getLogger<ServerInfos>(true);

        public static ServerInfos addresses { get; set; }

        /// <summary>
        /// 选中的服务器
        /// </summary>
        public static Server server { get; set; }

        /// <summary>
        /// 当前可用服务器地址 </summary>
        public Server[] servers;
    }
}