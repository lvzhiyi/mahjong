using cambrian.common;
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace cambrian.game
{
    public class User
    {
        /** 日志记录 */
        protected static cambrian.common.Logger log = LogFactory.getLogger<User>(true);

        /** 账号状态 (已关联手机号) */
        public const int STATUS_LINK_MOBILE = 1 << 0;
        /** 账号状态 (已经修改过密码) */
        public const int STATE_UPDATE_PWD = 1 << 1;
        /** 账号状态 (已经绑定了闲聊账号) */
        public const int STATE_BIND_XIANLIAO = 1 << 3;
        /// <summary>
        /// 已经身份认证  4
        /// </summary>
        public const int STATE_SET_SFRZ= 1<<4;

        public static User user;
      

        private const string USER = "user";

        /// <summary> 加载用户数据 </summary>
        public static User load(string server, long userid)
        {
            try
            {
                ByteBuffer data = FileCachesManager.load(server, userid, USER, true);
                User user = new User();
                if (data != null && data.length > 0)
                    user.bytesRead(data);
                if (log.isDebug)
                    Debug.Log(log.getMessage(",load,user=" + user));
                return user;
            }
            catch (Exception e)
            {
                string path = Application.persistentDataPath + "/" + ServerInfos.server.id + "/user/" + userid;
                Directory.Delete(path, true);
                return null;
            }
        }

        /// <summary> 保存用户数据 </summary>
        public static void save(User user)
        {
            if (user == null) return;
            ByteBuffer data = new ByteBuffer();
            user.bytesWrite(data);
            FileCachesManager.save(ServerInfos.server.id, user.userid, USER, true, data);
            if (log.isDebug)
                Debug.Log(log.getMessage(",save,user=" + user));
        }

        /// <summary> 注销登录数据 </summary>
        public static void logout(User user)
        {
            string path = Application.persistentDataPath + "/" + ServerInfos.server.id + "/user/" + user.userid;
            if(Directory.Exists(path))
            Directory.Delete(path, true);
        }
        /// <summary> 检查用户名 </summary>
        public static string checkUsername(string username)
        {
            if (username == null || username.Equals("")) return "请填写邮箱！";
            if (Regex.IsMatch(username, "^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$"))
                return null;
            return "无效的邮箱";
        }
        /// <summary> 检查昵称 </summary>
        public static string checkNickname(string str)
        {
            if (str == null || str.Equals("")) return "请填写昵称！";
            if (str.Length < 1 || str.Length > 12) return "昵称1-12位";
            if (str.IndexOf("游客") != -1) return "包含非法字符";
            return null;
        }
        /// <summary> 检查密码 </summary>
        public static string checkPassword(string str)
        {
            if (str == null || str.Equals("")) return "请填写密码！";
            if (str.Length < 6 || str.Length > 14) return "密码6-14位";
            return null;
        }
        /* 通行号登录 */
        /// <summary>
        /// 用户id </summary>
        public long userid;
        /// <summary>
        /// 通行号 </summary>
        public string passcheck;

        /// <summary>
        /// 昵称 </summary>
        public string nickname;
        /// <summary>
        /// 用户头像 </summary>
        public string head;
        /// <summary>
        /// 普通用户性别，1为男性，2为女性
        /// </summary>
        public int sex;
        /// <summary>
        /// 账号状态 </summary>
        public int status;
        /// <summary>
        /// 手机号 </summary>
        public string mobilenumber;
        /// <summary>
        /// 登录类型
        /// </summary>
        public int loginType;
        /// <summary>
        /// 微信临时码
        /// </summary>
        public string weixintmpcode;

        /// <summary>
        /// 推荐人
        /// </summary>
        public long recruiter;

        public User()
        {
        }
        public User(string weixintmpcode)
        {
            this.weixintmpcode = weixintmpcode;
        }

        public void bytesWrite(ByteBuffer data)
        {
            data.writeLong(this.userid);
            data.writeUTF(this.passcheck);
            data.writeUTF(this.nickname);
            data.writeUTF(this.head);
            data.writeInt(this.sex);
            data.writeInt(this.status);
            data.writeUTF(this.mobilenumber);
            data.writeLong(this.recruiter);
        }

        public void bytesRead(ByteBuffer data)
        {
            this.userid = data.readLong();
            this.passcheck = data.readUTF();
            this.nickname = data.readUTF();
            this.head = data.readUTF();
            this.sex = data.readInt();
            this.status = data.readInt();
            this.mobilenumber = data.readUTF();
            this.recruiter = data.readLong();
        }

        public override string ToString()
        {
            return "User [userid=" + this.userid + ", nickname=" + this.nickname + ", head=" + this.head +
                   ", status=" + this.status + ", passcheck=" + this.passcheck + "]";
        }
    }
}