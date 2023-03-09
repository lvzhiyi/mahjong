using cambrian.common;
using scene.game;
using System.IO;

namespace scene.loading
{
    /// <summary>
    /// 资源更新模块
    /// </summary>
    public class ResUpdateModule
    {
        /// <summary>
        /// 实例
        /// </summary>
        public static ResUpdateModule res = new ResUpdateModule();

        /// <summary>
        /// 后缀名
        /// </summary>
        public static string suffixName = ".upk";

        /// <summary>
        /// 获取需要更新
        /// </summary>
        /// <param name="res">服务器上的资源列表</param>
        /// <returns></returns>
        public ResData[] getNeedUpdateRes(ResUpdateList res)
        {
            ArrayList<ResData> list = new ArrayList<ResData>();
            for (int i = 0; i < res.resdata.Length; i++)
            {
                ResData resdata = res.resdata[i];
                if (!Directory.Exists(CacheLocalPath.AB_RESROUCE_PATH + resdata.packname))
                    list.add(resdata);
            }

            for (int i = list.count - 1; i > 0; i--)
            {
                string path = CacheLocalPath.AB_RESROUCE_PATH + list.get(i).packname + suffixName;
                if (File.Exists(path))
                    list.removeAt(i);
            }

            return list.toArray();
        }

        /// <summary>
        /// 获取需要解压的资源文件
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public ResData[] getUnCompressRes(ResUpdateList res)
        {
            ArrayList<ResData> list=new ArrayList<ResData>();
            for (int i = 0; i < res.resdata.Length; i++)
            {
                string path= CacheLocalPath.AB_RESROUCE_PATH + res.resdata[i].packname + suffixName;
                if (File.Exists(path))
                    list.add(res.resdata[i]);
            }

            return list.toArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResData[] getResourcePackage(ResUpdateList res)
        {
            ArrayList<ResData> list = new ArrayList<ResData>();
            for (int i = 0; i <res.resdata.Length; i++)
            {
                string path = CacheLocalPath.AB_RESROUCE_PATH + res.resdata[i].packname + suffixName;
                if (File.Exists(path))
                    list.add(res.resdata[i]);
            }

            return list.toArray();
        }


        public void deleteDirectory(string[] name)
        {
            for (int i = 0; i < name.Length; i++)
            {
                string path = CacheLocalPath.AB_RESROUCE_PATH +name[i];
                if (Directory.Exists(path))
                    Directory.Delete(path,true);
            }
        }
        /// <summary>
        /// 删除单个目录
        /// </summary>
        /// <param name="name"></param>
        public void deleteSingleDirectory(string name)
        {
            string path = CacheLocalPath.AB_RESROUCE_PATH + name;
            if (Directory.Exists(path))
                Directory.Delete(path,true);
        }
    }
}
