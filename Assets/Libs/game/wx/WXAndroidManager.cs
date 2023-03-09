using UnityEngine;

namespace cambrian.game
{
    public class WXAndroidManager
    {

#if (UNITY_ANDROID ) && !UNITY_EDITOR
    /// <summary>
    /// 安卓入口类
    /// </summary>
    AndroidJavaObject wx;
#endif

        public WXAndroidManager()
        {
#if (UNITY_ANDROID ) && !UNITY_EDITOR
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        wx = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
#endif
        }


        public void installApk(string path)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
            wx.Call("installApk",path);
#endif
        }

        /// <summary>
        /// 获取超级盾端口
        /// </summary>
        /// <returns></returns>
        public int getSislePort(string id, int port)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
           return wx.Call<int>("getQSuSislePort",id,port);
#endif
            return -1;
        }

        public void exitSystem()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
           wx.Call("exitSystem");
#endif
        }

        public bool isInstallWX()
        {
#if (UNITY_ANDROID ) && !UNITY_EDITOR
           return wx.Call<bool>("isInstallWX");
#endif
            return false;
        }


        public void initWeiXin(string appid)
        {
#if (UNITY_ANDROID ) && !UNITY_EDITOR
        wx.Call("initWeiXin",appid);
#endif
        }

        /// <summary>
        /// 打开微信
        /// </summary>
        public void openWeiXin()
        {
#if (UNITY_ANDROID ) && !UNITY_EDITOR
        wx.Call("openWeiXin");
#endif
        }

        /// <summary>
        /// 检查微信版本
        /// </summary>
        public bool checkVersion()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
        return wx.Call<bool>("checkVersion");
#endif
            return false;
        }

        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="scope">作用域</param>
        /// <param name="state">状态</param>
        /// <param name="panel">android调用unity面板名称</param>
        /// <param name="method">android调用该面板中的方法</param>
        public void login(string scope, string state, string panel, string method)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
            wx.Call("login",scope,state,panel,method);
#endif
        }

        /// <summary>
        /// 分享到朋友圈
        /// </summary>
        /// <param name="str"></param>
        public void share(string str)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
        wx.Call("share",str);
#endif
        }

        /// <summary>
        /// 分享链接给好友,群
        /// </summary>
        /// <param name="str"></param>
        public string shareLineToWeiXin(string url, string title, string description,int type)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
        return wx.Call<string>("shareLineToWeiXin",url,title,description,type);
#endif
            return "未调用";
        }

        public void shareIconToWeiXin(string url)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
        wx.Call("shareIconToWeiXin",url);
#endif
        }

        public bool isInstallDD()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
           return wx.Call<bool>("isInstallDD");
#endif
            return true;
        }

        /// <summary>
        /// 分享图片到钉钉
        /// </summary>
        /// <param name="bytes"></param>
        public void shareIconToDD(byte[] bytes)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
            wx.Call("shareIconToDD", bytes);
#endif
        }

        /// <summary>
        /// 分享链接到钉钉
        /// </summary>
        /// <param name="url"></param>
        /// <param name="description"></param>
        public void shareLineToDD(string url, string title, string description)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
            wx.Call("shareLineToDD", url,title,description);
#endif
        }


        /// <summary>
        /// 获取剩余电量
        /// </summary>
        /// <returns></returns>
        public void getCurBattery(string panel,string method)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
        wx.Call("getCurBattery",panel,method);
#endif
        }

        /// <summary>
        /// 获取网络强度
        /// </summary>
        /// <returns></returns>
        public void getNetWorkStrength(string panel,string method)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
          wx.Call("getNetWorkStrength",panel,method);
#endif
        }

        /// <summary>
        ///震动 毫秒
        /// </summary>
        /// <param name="time"></param>
        public void vibrator(long time)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
             wx.Call("vibrator",time);
#endif
        }

        public string getShareInfo()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
            return wx.Call<string>("getShareInfo");
#endif
            return "";
        }

        public string shareIconToWeiXin(byte[] bytes,int type)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
            return wx.Call<string>("shareIconToWeiXin",bytes,type);
#endif
            return "";
        }
        

        /// <summary>
        /// 开始录音
        /// </summary>
        /// <returns></returns>
        public void startRecord()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
             wx.Call("startRecord");
#endif
        }

        /// <summary>
        /// 停止录音
        /// </summary>
        /// <returns></returns>
        public void stopRecord(string panel,string method)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
            wx.Call("stopRecord",panel,method);
#endif
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <returns></returns>
        public int playerRecord(byte[] data)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
            return wx.Call<int>("playerRecord",data);
#endif
            return 0;
        }

        /// <summary>
        /// 获取剪切版内容
        /// </summary>
        /// <returns></returns>
        public string getClipborad()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
            return wx.Call<string>("getClipborad");
#endif
            return "";
        }

        /// <summary>
        /// 设置剪贴板内容
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string setClipContent(string content)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
            return wx.Call<string>("setClipBoardContent",content);
#endif
            return "0";
        }

        /// <summary>
        /// 微信支付
        /// </summary>
        /// <param name="partnerId">商户号</param>
        /// <param name="prepayId">预支付交易会话ID</param>
        /// <param name="packageVal">扩展字段</param>
        /// <param name="nonceStr">随机字符串</param>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="sign">签名</param>
        public void payWX(string partnerId, string prepayId, string packageVal, string nonceStr, string timeStamp, string sign)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
             wx.Call("wxpay",partnerId,prepayId,packageVal,nonceStr,timeStamp,sign);
#endif
        }


        public void openGPS()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
             wx.Call("openGPS");
#endif
        }

        /// <summary>
        /// 添加支付数据到友盟
        /// </summary>
        /// <param name="price"></param>
        /// <param name="count"></param>
        /// <param name="channel"></param>
        public string addPayDataToYM(int price,int count,int channel)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
             return wx.Call<string>("addYMPays",price,count,channel);
#endif
            return "error1";
        }

        /// <summary>
        /// 获取androidGPS定位
        /// </summary>
        /// <returns></returns>
        public void getGPSLocation(string panel,string method)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
              wx.Call("getGPSLocation",panel,method);
#endif
        }

        public string isOpenGPS()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
             return wx.Call<string>("isOpenGPS");
#endif
            return "0";
        }

        /// <summary>
        /// 获取手机系统版本
        /// </summary>
        public string getSystemVersion()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
             return wx.Call<string>("getSystemVersion");
#endif
            return "";
        }

        /// <summary>
        /// 获取手机型号
        /// </summary>
        public string getSystemModel()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
             return wx.Call<string>("getSystemModel");
#endif
            return "";
        }

        /// <summary>
        /// 获取手机厂商
        /// </summary>
        public string getDeviceBrand()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
             return wx.Call<string>("getDeviceBrand");
#endif
            return "";
        }


        /// <summary>
        /// 当前环境是否是模拟器
        /// </summary>
        public bool isSimulator()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
             return wx.Call<bool>("isSimulator");
#endif
            return false;
        }
    }
}
