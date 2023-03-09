namespace Framework
{
    using System;
    using System.IO;
    using UnityEditor;

    public class EditorKit
    {

        /// <summary> 程序执行时间测试 </summary>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <returns> 返回(秒)单位 </returns> 比如: 0.00239秒
        public static string ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
        {
            var ts1 = new TimeSpan(dateBegin.Ticks);
            var ts2 = new TimeSpan(dateEnd.Ticks);
            var ts3 = ts1.Subtract(ts2).Duration();
#if UNITY_2018_3 || UNITY_2019
            return ts3.ToString("c").Substring(0, 8); //转换的格式  00:00:00
#else
            return ts3.ToString().Substring(0, 8);
#endif
        }

        /// <summary> 复制文件夹及文件 部分 </summary> 根文件名不会复制 适合重命名
        /// <param name="sourceFolder">原文件路径</param>
        /// <param name="destFolder">目标文件路径</param>
        public static void CopyFolderPart(string sourceFolder, string destFolder)
        {
            try
            {
                var destfolderdir = Path.Combine(destFolder, Path.GetFileName(sourceFolder));
                foreach (string file in Directory.GetFileSystemEntries(sourceFolder))//遍历所有的文件和目录
                {
                    if (Directory.Exists(file))
                    {
                        var currentdir = Path.Combine(destfolderdir, Path.GetFileName(file));
                        if (!Directory.Exists(currentdir)) Directory.CreateDirectory(currentdir);
                        CopyFolderPart(file, destfolderdir);
                    }
                    else
                    {
                        var srcfileName = Path.Combine(destfolderdir, Path.GetFileName(file));
                        if (!Directory.Exists(destfolderdir)) Directory.CreateDirectory(destfolderdir);
                        File.Copy(file, srcfileName);
                    }
                }
            }
            catch (Exception e) { EditorUtility.DisplayDialog("提示", e.Message, "确定"); }

        }

        /// <summary> 复制文件夹及文件 全部 </summary> 根文件名一起复制
        /// <param name="sourceFolder">原文件路径</param>
        /// <param name="destFolder">目标文件路径</param>
        public static void CopyFolderAll(string sourceFolder, string destFolder, bool overwirte = false)
        {
            try
            {   //如果目标路径不存在,则创建目标路径
                if (!Directory.Exists(destFolder)) Directory.CreateDirectory(destFolder);
                foreach (string file in Directory.GetFiles(sourceFolder))
                {   //得到原文件根目录下的所有文件
                    var dest = Path.Combine(destFolder, Path.GetFileName(file));
                    File.Copy(file, dest, overwirte);//复制文件
                }
                foreach (string folder in Directory.GetDirectories(sourceFolder))
                {   //得到原文件根目录下的所有文件夹
                    var dest = Path.Combine(destFolder, Path.GetFileName(folder));
                    CopyFolderAll(folder, dest, overwirte);//构建目标路径,递归复制文件
                }
            }
            catch (Exception e) { EditorUtility.DisplayDialog("提示", e.Message, "确定"); }
            finally { }
        }

        /// <summary> 删除文件夹 </summary> 根文件名一起复制     
        public static void FileDelete(string sourceFolder)
        {
            try
            {   //如果目标路径不存在
                if (!Directory.Exists(sourceFolder)) return;
                foreach (string file in Directory.GetFiles(sourceFolder))
                {   //得到原文件根目录下的所有文件
                    var dest = Path.Combine(sourceFolder, Path.GetFileName(file));
                    File.Delete(file);
                }
                foreach (string folder in Directory.GetDirectories(sourceFolder))
                {   //得到原文件根目录下的所有文件夹
                    var dest = Path.Combine(sourceFolder, Path.GetFileName(folder));
                    FileDelete(folder);
                }
            }
            catch (Exception e) { EditorUtility.DisplayDialog("提示", e.Message, "确定"); }
            finally { }
        }
    }
}
