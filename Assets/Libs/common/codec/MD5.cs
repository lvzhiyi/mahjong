using System.Text;
/**
 * 类说明：MD5
 * 
 * @version 1.0
 * @author HYZ<huangyz1988@qq.com>
 */

namespace cambrian.common
{
    public class MD5
    {

        public static string encode(string str)
        {
            return encode(Encoding.UTF8.GetBytes(str), false);
        }

        public static string encode(string str, bool lower)
        {
            return encode(Encoding.UTF8.GetBytes(str), lower);
        }

        public static string encode(byte[] bytes)
        {
            return encode(bytes, false);
        }

        /// <summary>
        /// 获取指定字符串的MD5码
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string encode(byte[] bytes, bool lower)
        {
            System.Security.Cryptography.MD5 encoder = System.Security.Cryptography.MD5.Create();
            byte[] data = encoder.ComputeHash(bytes);
            return HexKit.toHex(data, lower);
        }
    }
}