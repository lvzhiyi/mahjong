using System.Collections;
using UnityEngine;

namespace cambrian.game
{
    public class IconManager
    {
        /// <summary>
        /// 玩家头像缓存(并没有存本地文件),包括其他玩家的头像
        /// </summary>
        private static Hashtable headpic = new Hashtable();

        public static void saveHeadPic(long userid, Sprite sprite)
        {
            headpic[userid] = sprite;
        }

        public static Sprite getHeadPic(long userid)
        {
            if (headpic[userid] != null)
                return (Sprite)headpic[userid];
            return null;
        }

        /// <summary>
        /// 检查头像地址是否有效
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static bool checkHeadUrl(string head)
        {
            if (head == null || head.Length <= 2) return false;
            return true;
        }

    }
}
