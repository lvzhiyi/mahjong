using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

namespace cambrian.common
{
    /**
	 * 类说明：
	 * 
	 * @version 1.0
	 * @author maxw<woldits@qq.com>
	 */
    public class StringKit
    {
        /// <summary>
        /// 判断字符串是否是汉字
        /// </summary>
        /// <returns></returns>
        public static bool isName(string str)
        {
            if (StringKit.isNullString(str))
                return false;

            if (!Regex.IsMatch(str, @"[\u4e00-\u9fbb]{2,8}$"))
            {
                return false;
            }
            return true;
        }
        public static bool isIphone(string str)
        {
            if (Regex.IsMatch(str, @"1[0-9]{10}"))
                return true;
            return false;
        }
        /// <summary> 转换带有多个参数的语言文案</summary>
        public static string trans(string text, params object[] values)
        {
            try
            {
                text = String.Format(text, values);
            }
            catch (Exception)
            {
            }
            return text;
        }
        /// <summary>
        /// 转换为格式:00,000,000
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string toStringMoney(long value)
        {
            long tmp = value%1000;
            string str = "" + tmp;
            value /= 1000;
            while (value > 0)
            {
                if (tmp >= 0 && tmp < 10) str = "00" + str;
                else if (tmp >= 10 && tmp < 100) str = "0" + str;

                tmp = value%1000;
                str = tmp + "," + str;
                value /= 1000;
            }
            return str;
        }
        public static bool isNullString(string str)
        {
            if(str==null) return true;
            if(str.Length==0) return true;
            return false;
        }
        /** 解析字符串为数字 */
        public static int parseInt(string value)
        {
            if (value == null || value.Equals("")) return 0;
            return Int32.Parse(value);
        }

        public static bool isInt(string value)
        {
            return Regex.IsMatch(value, "^-?\\d+$");
        }

        public static bool isNumber(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        public static long parseLong(string value)
        {
            if (value == null || value.Equals("")) return 0L;
            return long.Parse(value);
        }

        public static string substring(string value,int startindex,int len)
        {
            if (value == null || "".Equals(value)) return null;
            if (value.Length > len)
                return value.Substring(startindex, len);
            return value;
        }

        public static float parseFloat(string value)
        {
            if (value == null || value.Equals("")) return 0F;
            return float.Parse(value);
        }

        public static double parseDouble(string value)
        {
            if (value == null || value.Equals("")) return 0F;
            return double.Parse(value);
        }

        /** 解析字符串(以,分割)为一维数字数组 */
        public static long[] parseLongs(string value)
        {
            if (value == null || value.Equals(""))
                return new long[0];
            string[] strs = value.Split(new char[] { ',' });
            long[] returns = new long[strs.Length];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = parseInt(strs[i]);
            }
            return returns;
        }
        /** 解析字符串(以,|分割)为二维数字数组 */
        public static long[][] parseLongss(string value)
        {
            if (value == null || value.Equals(""))
                return new long[0][];
            string[] strs = value.Split(new char[] { '|' });
            long[][] returns = new long[strs.Length][];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = parseLongs(strs[i]);
            }
            return returns;
        }

        /** 解析字符串(以,分割)为一维数字数组 */
        public static int[] parseInts(string value)
        {
            if (value == null || value.Equals(""))
                return new int[0];
            string[] strs = value.Split(new char[] {','});
            int[] returns = new int[strs.Length];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = parseInt(strs[i]);
            }
            return returns;
        }
        /** 解析字符串(以,分割)为一维数字数组 */
        public static int[] parseInts(string value,char split)
        {
            if (value == null || value.Equals(""))
                return new int[0];
            string[] strs = value.Split(new char[] { split });
            int[] returns = new int[strs.Length];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = parseInt(strs[i]);
            }
            return returns;
        }
        /** 解析字符串(以,|分割)为二维数字数组 */
        public static int[][] parseIntss(string value)
        {
            if (value == null || value.Equals(""))
                return new int[0][];
            string[] strs = value.Split(new char[] {'|'});
            int[][] returns = new int[strs.Length][];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = parseInts(strs[i]);
            }
            return returns;
        }

        /** 解析字符串(以,|,:分割)为三维数字数组 */
        public static int[][][] parseIntsss(string value)
        {
            if (value == null || value.Equals(""))
                return new int[0][][];
            string[] strs = value.Split(new char[] {':'});
            int[][][] returns = new int[strs.Length][][];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = parseIntss(strs[i]);
            }
            return returns;
        }

        /** 解析字符串(以,分割)为一维数字数组 */
        public static string[] parseStrings(string value)
        {
            if (value == null || value.Equals(""))
                return new string[0];
            string[] strs = value.Split(new char[] {','});
            string[] returns = new string[strs.Length];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = strs[i];
            }
            return returns;
        }

        public static string[] parseStrings(string value, char split)
        {
            if (value == null || value.Equals(""))
                return new string[0];
            string[] strs = value.Split(new char[] { split });
            string[] returns = new string[strs.Length];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = strs[i];
            }
            return returns;
        }

        /** 解析字符串(以,|分割)为二维数字数组 */
        public static string[][] parseStringss(string value)
        {
            if (value == null || value.Equals(""))
                return new string[0][];
            string[] strs = value.Split(new char[] {'|'});
            string[][] returns = new string[strs.Length][];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = parseStrings(strs[i]);
            }
            return returns;
        }
        /** 解析字符串(以,|分割)为三维数字数组 */
        public static string[][][] parseStringsss(string value)
        {
            if (value == null || value.Equals(""))
                return new string[0][][];
            string[] strs = value.Split(new char[] {':'});
            string[][][] returns = new string[strs.Length][][];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = parseStringss(strs[i]);
            }
            return returns;
        }

        /** 解析字符串为指定大小的一维数组 */

        public static string[] parseStrings(string value, int size)
        {
            if (value == null || value.Equals("") || size <= 0)
                return new string[0];
            if (value.Length <= size)
                return new string[] {value};
            char[] chars = value.ToCharArray();
            CharBuffer buffer = new CharBuffer();

            int count = chars.Length/size;
            int len = chars.Length%size > 0 ? (count + 1) : count;
            string[] strs = new string[len];

            for (int i = 0; i < strs.Length; i++)
            {
                buffer.clear();
                for (int j = i*size; j < chars.Length && j < size*(i + 1); j++)
                {
                    buffer.append(chars[j]);
                }
                strs[i] = buffer.getString();
            }

            return strs;
        }

        public static int[] parseInts(string[] value)
        {
            int[] array = new int[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = parseInt(value[i]);
            }
            return array;
        }

        public static int[][] parseIntss(string[][] value)
        {
            int[][] array = new int[value.Length][];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = parseInts(value[i]);
            }
            return array;
        }
        public static int[][][] parseIntsss(string[][][] value)
        {
            int[][][] array = new int[value.Length][][];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = parseIntss(value[i]);
            }
            return array;
        }
        public static bool parseBool(string value)
        {
            if (value == null || value.Equals("")) return false;
            return bool.Parse(value);
        }

        public static bool[] parseBools(string[] value)
        {
            bool[] array = new bool[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = parseBool(value[i]);
            }
            return array;
        }

        public static bool[][] parseBoolss(string[][] value)
        {
            bool[][] array = new bool[value.Length][];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = parseBools(value[i]);
            }
            return array;
        }
        public static bool[][][] parseBoolsss(string[][][] value)
        {
            bool[][][] array = new bool[value.Length][][];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = parseBoolss(value[i]);
            }
            return array;
        }

        public static long[] parseLongs(string[] value)
        {
            long[] array = new long[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = parseLong(value[i]);
            }
            return array;
        }

        public static long[][] parseLongss(string[][] value)
        {
            long[][] array = new long[value.Length][];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = parseLongs(value[i]);
            }
            return array;
        }
        public static long[][][] parseLongsss(string[][][] value)
        {
            long[][][] array = new long[value.Length][][];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = parseLongss(value[i]);
            }
            return array;
        }

        public static string[] splitOnce(string src, char ch)
        {
            if (src == null) return null;
            if (src.Length == 0) return new string[0];
            int index = src.IndexOf(ch);
            if (index == -1) return new string[] {src};
            string[] array = new string[2];
            array[0] = (index == 0 ? "" : src.Substring(0, index));
            array[1] = (index == src.Length - 1 ? "" : src.Substring(index+1));
            return array;
        }

        public static string[] split(string src, char ch)
        {
            if (src == null) return null;
            if (src.Length == 0) return new string[0];
            int i = 0, j = 0, k = 1;
            while ((j = src.IndexOf(ch, i)) >= 0)
            {
                i = j + 1;
                k++;
            }
            string[] array = new string[k];
            if (k == 1)
            {
                array[0] = src;
                return array;
            }
            i = j = k = 0;
            while ((j = src.IndexOf(ch, i)) >= 0)
            {
                array[k++] = (i == j ? "" : src.Substring(i, j-i));
                i = j + 1;
            }
            array[k] = (i >= src.Length ? "" : src.Substring(i));
            return array;
        }


        /** 将字符串以行拆分为数组 */
        public static string[] splitLine(string str)
        {
            string[] array = split(str, '\n');
            for (int k = 0; k < array.Length; k++)
            {
                int i = 0;
                int j = array[k].Length;
                if (j != 0)
                {
                    if (array[k][0] == '\r')
                    {
                        i++;
                        j--;
                    }
                    if (array[k][array[k].Length - 1] == '\r') j--;
                    if (j <= 0)
                        array[k] = "";
                    else if (j < array[k].Length) array[k] = array[k].Substring(i, j);
                }
            }
            return array;
        }

        //        /** 打印数组信息 */
        //        public static string toString(Array array)
        //        {
        //            if (array == null) return "null";
        //            int i = 0;
        //            string s = "[";
        //            foreach (object v in array)
        //            {
        //                s += i == 0 ? "" + v : "," + v;
        //                i++;
        //            }
        //            s += "]";
        //            return s;
        //        }

        /// <summary>
        /// 打印对象信息，对象可以是数组，维度不限
        /// </summary>
        public static string toString(object obj)
        {
            if (obj == null) return "null";
            if (obj is Array)
            {
                string sp = "";
                string str = "[";
                foreach (object v in (Array) obj)
                {
                    str += sp + toString(v);
                    sp = ", ";
                }
                return str += "]";
            }
            else
                return obj.ToString();
        }

        public static string toString2Array(Array array)
        {
            if (array == null) return "null";
            int i = 0;
            string s = "[";
            foreach (object v in array)
            {
                s += i == 0 ? "" + toString((Array) v) : "," + toString((Array) v);
                i++;
            }
            s += "]";
            return s;
        }

        public static string to8Binary(int value)
        {
            string str = "";
            int t = 1;
            for (int i = 0; i < 8; i++)
            {
                if ((value & t) == 0)
                    str = 0 + str;
                else
                    str = 1 + str;
                t = t << 1;
            }
            return str;
        }

        //{"status":1,"info":{"id":1292526,"name":null,"list":[{"id":1292526,"name":"哈哈哈哈","date":"03-10"},{"id":1303274,"name":"阿萨德呵呵度","date":"03-10"},{"id":8832902,"name":"纯属乱打","date":"03-10"},{"id":1297588,"name":"的共同点速度","date":"03-10"}],"date":"03-10"}}
        public static Hashtable[] getJson(string text)
        {
            int index = text.IndexOf("\"list\"");
            index = text.IndexOf("{", index);
            text = text.Split(new char[] {'[', ']'})[1];
            //Console.WriteLine(text);
            string[] ts = text.Split(new string[] {"},{"}, StringSplitOptions.None);
            Hashtable[] list = new Hashtable[ts.Length];
            for (int i = 0; i < ts.Length; i++)
            {
                // Console.WriteLine(ts[i]);
                Hashtable ht = new Hashtable();
                string[] tss = ts[i].Split(new char[] {','});
                for (int j = 0; j < tss.Length; j++)
                {
                    string[] items = tss[j].Split(new char[] {':'});
                    // Console.WriteLine(items[0] + " --> " + items[1]);

                    string key = items[0].Split(new char[] {'"'})[1];
                    string value = null;
                    if (items[1].IndexOf('"') >= 0)
                        value = items[1].Split(new char[] {'"'})[1];
                    else
                        value = items[1];
                    // Console.WriteLine(key + "----" + value);
                    ht[key] = value;
                }
                list[i] = ht;
            }
            return list;
        }

        public static string getValue(string key, string json)
        {
            key = "\"" + key + "\":";
            int index = json.IndexOf(key) + key.Length;
            //Console.WriteLine(key + " " + index);
            string value = json.Substring(index);
            //Console.WriteLine(value);
            if (value.IndexOf('"') != 0)
                value = value.Split(new char[] {','})[0];
            else
                value = value.Split(new char[] {'"'})[1];
            //Console.WriteLine(value);
            return value;
        }

        /// <summary>
        /// sql注入检查
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool sqlStringCheck(string str)
        {
            int index = str.IndexOf('\'');
            if (index >= 0) return false;
            index = str.IndexOf("--");
            if (index >= 0) return false;
            return true;
        }

        /// <summary>
        /// 检查名字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool checkName(string str)
        {
            if (str.Length < 2 || str.Length > 16) return false;
            return sqlStringCheck(str);
        }
        /// <summary>
        /// 检查简介信息
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool checkInfo(string str)
        {

            return sqlStringCheck(str);
        }

    }
}