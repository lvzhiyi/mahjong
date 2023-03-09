using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;

namespace cambrian.common
{
    /// <summary>
    /// 文件读写操作工具集
    /// </summary>
    public class FileKit
    {
        /// <summary> 空字节数组 </summary>
        public static readonly byte[] EMPTY_BYTES = {};

        /// <summary> 将字符串按照UTF-8写入文件，覆盖源文件 </summary>
        public static void writeText(string path, string text)
        {
            writeText(path, text, false);
        }

        /// <summary> 将字符串按照UTF-8写入文件，是否追加到文件尾 </summary>
        public static void writeText(string path, string text, bool concat)
        {
            writeText(path, text, "utf-8", concat);
        }

        /// <summary> 将字符串按照指定编码写入文件，是否追加到文件尾 </summary>
        public static void writeText(string path, string text, string charset, bool concat)
        {
            byte[] bytes = Encoding.GetEncoding(charset).GetBytes(text);
            write(path, bytes, concat);
        }

        ///** 加载XML */
        public static XmlNode loadXml(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string text = loadText(path);
            xmlDoc.LoadXml(text);
            return xmlDoc.DocumentElement;
        }
        ///** 加载文本中的字符串 */
        public static string loadText(string path)
        {
            if (File.Exists(path))
            {
                //return File.ReadAllText(path, Encoding.UTF8);
                return readText(path);
            }
            return null;
        }
        /// <summary> 按照UTF-8读取文本文件 </summary>
        public static string readText(string path)
        {
            return readText(path, "utf-8");
        }

        /// <summary> 按照指定编码读取文本文件 </summary>
        public static string readText(string path, string charset)
        {
            return Encoding.GetEncoding(charset).GetString(read(path));
        }

        /// <summary> 将数据写入文件，覆盖源文件 </summary>
        public static void write(string path, byte[] bytes)
        {
            write(path, bytes, false);
        }

        /// <summary> 将数据写入文件，是否追加到文件尾 </summary>
        public static void write(string path, byte[] bytes, bool concat)
        {
            write(path, bytes, 0, bytes.Length, concat);
        }

        /// <summary> 将指定数据从offset开始写入length长度到文件中，是否追加到文件尾 </summary>
        public static void write(string path, byte[] bytes, int offset, int length, bool concat)
        {
            path = Path.GetFullPath(path); //转换为绝对路径
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            FileMode mode = (concat) ? FileMode.Append : FileMode.Create;
            using (FileStream fs = new FileStream(path, mode, FileAccess.Write))
            {
                fs.Write(bytes, offset, length);
            }
        }

        /// <summary> 从文件中读取数据 </summary>
        public static byte[] read(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    using (FileStream fsSource = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        byte[] bytes = new byte[fsSource.Length];
                        int numBytesToRead = (int) fsSource.Length;
                        int numBytesRead = 0;
                        while (numBytesToRead > 0)
                        {
                            int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);
                            if (n == 0)
                                break;
                            numBytesRead += n;
                            numBytesToRead -= n;
                        }
                        numBytesToRead = bytes.Length;
                        return bytes;
                    }
                }
                catch (FileNotFoundException ioEx)
                {
                    Debug.LogWarning(ioEx.Message);
                    return EMPTY_BYTES;
                }
            }
            return EMPTY_BYTES;
        }

        /// <summary>读取图片 </summary>
        public static byte[] readPhoto(string path)
        {
            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                return br.ReadBytes((int) fs.Length);
            }
            return EMPTY_BYTES;
        }

        /// <summary> 删除指定文件 </summary>
        public static void delete(string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }
    }
}