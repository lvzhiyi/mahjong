
using UnityEngine;

namespace cambrian.common
{
    /// <summary>
    /// 屏蔽字
    /// </summary>
    public class MaskWord
    {
        private static string[] words = new string[]
       {

       };
        /// <summary>
        /// 替换屏蔽字为*
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string replace(string str)
        {
            for (int i = 0; i < words.Length; i++)
            {
                int index = str.IndexOf(words[i]);
                if (index < 0) continue;
                string value = "";
                if (index > 0) value = str.Substring(index, words[i].Length);
                for (int j = 0; j < words[i].Length; j++)
                {
                    value += '*';
                }
                if (str.Length > index + words[i].Length) value += str.Substring(index + words[i].Length);
                str = value;
            }
            return str;
        }

        public static void loadMaskWord()
        {
            if (words.Length == 0)
            {
                string text = ((TextAsset)Resources.Load("maskword")).text;
                words = text.Split(new char[] {'\n'});
            }
        }
    }
}