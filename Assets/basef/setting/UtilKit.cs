using cambrian.common;
using cambrian.game;
using System;
using basef.lobby;
using UnityEngine;

namespace basef.setting
{
    public class UtilKit
    {
        /// <summary>
        /// 托管中
        /// </summary>
        public static bool auto = false;

        public static string text_Shareinfo =
            "推荐有礼! 即日起，分享游戏到微信，让你的亲朋好友下载起来吧！\n凡新用户下载安装并进入游戏，推荐人即可获得我们提供的房卡奖励；更多的，当新用户充值时，直接推荐人会获得30%的房卡奖励，间接推荐人会获得20%的房卡奖励！\n此外，新用户进入游戏时会获得我们赠送的房卡！";

        public static string text_LoginTokenSystem = "登录房卡平台可查看<color=red>奖励明细</color>,并可<color=red>转送房卡</color>给亲朋好友";

        public static string text_invite = "分享游戏到微信，让你的亲朋好友下载并进入游戏，即可获得我们提供的房卡奖励";

        /// <summary>
        /// 把玩家数据转化为base64
        /// </summary>
        /// <returns></returns>
        public static string userToBase64()
        {
            ByteBuffer data = new ByteBuffer();
            User.user.bytesWrite(data);
            data.writeLong(TimeKit.currentTimeMillis);
            byte[] bytes = data.toArray();
            return Convert.ToBase64String(bytes);
        }

        public static void openSocialURL()
        {
            Application.OpenURL("http://center.wemywin.com?" + UtilKit.userToBase64());
            //Application.OpenURL("http://192.168.2.201/qipai/scxz/center/index.html?" + UtilKit.userToBase64());
        }

        /// <summary>
        /// 刷新玩家房卡显示
        /// </summary>
        public static void refreshPlayerToken()
        {
            ShowLobbyPanel.refreshLobbyPanel();
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="source"></param>
        /// <param name="status"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int setStatus(int source, int status, bool b)
        {
            if (b)
                source |= status;
            else
                source &= (~status);
            return source;
        }

        /// <summary>
        /// 是否拥有状态
        /// </summary>
        /// <param name="source"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool isStatus(int source, int status)
        {
            return (source & status) == status;
        }

        public static string getLimitString(bool b, string value)
        {
            if (b) return getLimitNormalString(value);
            else return getLimitWarningString(value);
        }

        public static string getLimitNormalString(string value)
        {
            //return "<color=#FFFFFFFF>" + value + "</color>";
            return "<color=#000000B4>" + value + "</color>";
        }

        public static string getLimitWarningString(string value)
        {
            //return "<color=#E97246FF>" + value + "</color>";
            return "<color=#D0021BFF>" + value + "</color>";
        }

        /// <summary>
        /// 把byte数组的前4个转换为一个int
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static int toInt(byte[] bytes)
        {
            int value = (bytes[3] & 0xFF);
            value |= (bytes[2] & 0xFF) << 8;
            value |= (bytes[1] & 0xFF) << 16;
            value |= (bytes[0] & 0xFF) << 24;
            return value;
        }

        /// <summary>
        /// 把一个int转换为byte数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] toBytes(int value)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte) (value >> 24);
            bytes[1] = (byte) (value >> 16);
            bytes[2] = (byte) (value >> 8);
            bytes[3] = (byte) value;
            return bytes;
        }

        /// <summary>
        /// 排序显示对象
        /// </summary>
        /// <param name="start"></param>
        /// <param name="incr"></param>
        /// <param name="args"></param>
        public static void sortByLocalX(float start, bool incr, params UnrealLuaSpaceObject[] args)
        {
            float x = start;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].getVisible())
                {
                    DisplayKit.setLocalX(args[i], x);
                    if (incr) x += args[i].getWidth();
                    else x -= args[i].getWidth();
                }
            }
        }

        /// <summary>
        /// 是否为空字符,null或长度为0
        /// </summary>
        /// <param name="strs">
        /// @return </param>
        public static bool isNullString(params string[] strs)
        {
            for (int i = 0; i < strs.Length; i++)
            {
                if (string.ReferenceEquals(strs[i], null))
                {
                    return true;
                }
                if (strs[i].Length == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool startsWith(string str, string prefix)
        {
            if (isNullString(str)) return false;
            return str.StartsWith(prefix);
        }

        /// <summary>
        /// 是否为sql注入攻击
        /// </summary>
        /// <param name="strs">
        /// @return </param>
        public static bool isSqlAttack(params string[] strs)
        {
            for (int i = 0; i < strs.Length; i++)
            {
                if (string.ReferenceEquals(strs[i], null))
                {
                    continue;
                }
                int index = strs[i].IndexOf('\'');
                if (index >= 0)
                {
                    return true;
                }
                index = strs[i].IndexOf("--", StringComparison.Ordinal);
                if (index >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static void forEach<T>(T[] array, Action<T> action)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                {
                    continue;
                }
                action(array[i]);
            }
        }
    }
}
