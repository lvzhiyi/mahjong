using cambrian.common;
using scene.game;
using System.IO;
using UnityEngine;

namespace cambrian.game
{
    /// <summary>
    /// 文件缓存管理器
    /// </summary>
    public class FileCachesManager
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        private static cambrian.common.Logger log = LogFactory.getLogger<FileCachesManager>(true);

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="server"></param>
        /// <param name="userid"></param>
        /// <param name="key"></param>
        /// <param name="coded"></param>
        /// <returns></returns>
        public static ByteBuffer load(string server, long userid, string key, bool coded)
        {
            string path = getPath(server, userid, key);
            byte[] bytes = FileKit.read(path);
            if (bytes == null || bytes.Length == 0)
                return null;
            if (coded)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = newbyte(bytes[i]);
                }
            }
            if (log.isDebug)
                Debug.Log(log.getMessage("load ,path=" + path + ",len=" + bytes.Length));
            return new ByteBuffer(bytes);
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="server"></param>
        /// <param name="userid"></param>
        /// <param name="key"></param>
        /// <param name="coded"></param>
        /// <param name="data"></param>
        public static void save(string server, long userid, string key, bool coded, ByteBuffer data)
        {
            string path = getPath(server, userid, key);
            byte[] bytes = data.toArray();
            if (coded)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = newbyte(bytes[i]);
                }
            }
            if (log.isDebug)
                Debug.Log(log.getMessage("save ,path=" + path + ",len=" + bytes.Length));
            FileKit.write(path, bytes, false);
        }
        /// <summary>
        /// 获取缓存路径
        /// </summary>
        /// <param name="server"></param>
        /// <param name="userid"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string getPath(string server, long userid, string key)
        {
            return CacheLocalPath.APPLICATION_PATH + "/" + server + "/user/" + userid + "/" + key + "_" + userid + ".dat";
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="path">全路径</param>
        /// <param name="coded"></param>
        /// <param name="data"></param>
        public static void savePath(string path, bool coded, ByteBuffer data)
        {
            byte[] bytes = data.toArray();
            if (coded)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = newbyte(bytes[i]);
                }
            }
            if (log.isDebug)
                Debug.Log(log.getMessage("save ,path=" + path + ",len=" + bytes.Length));
            FileKit.write(path, bytes, false);
        }

        public static void savePathData(string path, bool coded, ByteBuffer data)
        {
            path = getPath(path);
            byte[] bytes = data.toArray();
            if (coded)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = newbyte(bytes[i]);
                }
            }
            if (log.isDebug)
                Debug.Log(log.getMessage("save ,path=" + path + ",len=" + bytes.Length));
            FileKit.write(path, bytes, false);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="path">全路径</param>
        /// <param name="coded"></param>
        /// <returns></returns>
        public static ByteBuffer loadPath(string path, bool coded)
        {
            byte[] bytes = FileKit.read(path);
            if (bytes == null || bytes.Length == 0)
                return null;
            if (coded)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = newbyte(bytes[i]);
                }
            }
            if (log.isDebug)
                Debug.Log(log.getMessage("load ,path=" + path + ",len=" + bytes.Length));
            return new ByteBuffer(bytes);
        }

        /// <summary>
        /// 加载指定目录下的服务id
        /// </summary>
        /// <param name="serverid"></param>
        /// <param name="name"></param>
        /// <param name="coded"></param>
        /// <returns></returns>
        public static ByteBuffer loadPathData(string name,bool coded)
        {
            string path = getPath(name);
            byte[] bytes = FileKit.read(path);
            if (bytes == null || bytes.Length == 0)
                return null;
            if (coded)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = newbyte(bytes[i]);
                }
            }
            if (log.isDebug)
                Debug.Log(log.getMessage("load ,path=" + path + ",len=" + bytes.Length));
            return new ByteBuffer(bytes);
        }

        /// <summary>
        /// 获取缓存路径
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string getPath(string name)
        {
            return CacheLocalPath.APPLICATION_PATH + "/" + ServerInfos.server.id + "/"+name;
        }

        /// <summary>
        /// 删除指定路径下的数据
        /// </summary>
        /// <param name="path"></param>
        public static void deletePathData(string path)
        {
            File.Delete(getPath(path));
        }

        /// <summary>
        /// 简单的编码加密
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static byte newbyte(byte num)
        {
            int state = (num & 0xFF);
            int n = 2;
            for (int i = 0; i < 8; i++)
            {
                if (n == 64) continue;
                if ((state & n) == n)
                {
                    state &= (~n);
                }
                else
                {
                    state |= n;
                }
                n = n*2;
            }
            return (byte) state;
        }
    }
}