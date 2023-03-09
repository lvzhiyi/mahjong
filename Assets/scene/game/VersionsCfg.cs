using cambrian.common;
using cambrian.game;
using UnityEngine;

namespace scene.game
{
    /// <summary>
    /// 本地资源版本和ABbundle.cfg指向路径
    /// </summary>
    public class VersionsCfg
    {
        /// <summary>
        /// 整包下载,更新,无更新
        /// </summary>
        public const int DOWNLOAD = 1, UPDATE_AB = 2, NO_UPDATE = 3;

        public static VersionsCfg cfg;
        /// <summary>
        /// 版本
        /// </summary>
        public Versions versions;
        /// <summary>
        /// 资源路径
        /// </summary>
        public string resoucePath;


        public VersionsCfg()
        {
            this.versions=new Versions();
        }

        /// <summary>
        /// server lua 调用
        /// </summary>
        /// <param name="version"></param>
        /// <param name="resoucePath"></param>
        public void setData(string version,string resoucePath)
        {
            this.versions=new Versions(version);
            this.resoucePath = resoucePath;
        }

        public void bytesWrite(ByteBuffer data)
        {
            this.versions.bytesWrite(data);
            data.writeUTF(resoucePath);
        }

        public void bytesRead(ByteBuffer data)
        {
            this.versions=new Versions();
            this.versions.bytesRead(data);
            this.resoucePath = data.readUTF();
        }

        /// <summary>
        /// 比较版本
        /// </summary>
        /// <param name="versions"></param>
        /// <returns></returns>
        public static int compareVersion(string localVersion,string serverVersion)
        {
            int[] loc = StringKit.parseInts(localVersion, '.');
            int[] remote = StringKit.parseInts(serverVersion, '.');

            if (loc.Length != remote.Length) return DOWNLOAD;

            int[] v = new int[3];

            for (int i = 0; i < loc.Length; i++)
            {
                if (remote[i] > loc[i]) v[i] = 1; //服务起端版本大于本地版本
                if (remote[i] == loc[i]) v[i] = 2; //等于
                if (remote[i] < loc[i]) v[i] = 3; //小于
            }

            if (remote[0] > loc[0]) return UPDATE_AB;
            if (remote[1] > loc[1]) return DOWNLOAD;
            if (remote[2] > loc[2]) return UPDATE_AB;
            return NO_UPDATE;
        }


        public static void load()
        {
            ByteBuffer data=FileCachesManager.loadPath(CacheLocalPath.VERSION_CFG_PATH, false);
            if (data != null)
            {
                VersionsCfg cfg=new VersionsCfg();
                cfg.bytesRead(data);
                VersionsCfg.cfg = cfg;
            }
        }

        public static void save()
        {
            if (cfg != null)
            {
                ByteBuffer data=new ByteBuffer();
                cfg.bytesWrite(data);
                FileCachesManager.savePath(CacheLocalPath.VERSION_CFG_PATH, false,data);
            }
        }
    }
}
