using UnityEngine;

namespace cambrian.game
{
    public class WXManager
    {
        private WXIOSManager ios;
        private WXAndroidManager android;

        private static WXManager manager;

        private WXManager()
        {
            ios = new WXIOSManager();
            android = new WXAndroidManager();
        }

        public static WXManager getInstance()
        {
            if (manager == null)
                manager = new WXManager();
            return manager;
        }

        public void installApk(string path)
        {
            android.installApk(path);
        }


        public void initWeiXin(string appid)
        {
#if (UNITY_ANDROID ) && !UNITY_EDITOR
        android.initWeiXin(appid);
#endif
#if UNITY_IOS && !UNITY_EDITOR
          ios.initWeiXin(appid);
#endif
        }

        /// <summary>
        /// 打开微信
        /// </summary>
        public void openWeiXin()
        {
#if (UNITY_ANDROID ) && !UNITY_EDITOR
        android.openWeiXin();
#endif
#if UNITY_IOS && !UNITY_EDITOR
          ios.openWeiXin();
#endif
        }

        /// <summary>
        /// 检查微信版本
        /// </summary>
        public bool checkVersion()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
        return android.checkVersion();
#endif
#if UNITY_IOS && !UNITY_EDITOR
          return ios.checkVersion();
#endif
            return false;
        }

        /// <summary>
        /// 发送给好友,群 ,朋友圈
        /// type 1 分享到好友或者好友群
        ///      2 分享到朋友圈
        /// </summary>
        /// <param name="str"></param>
        public string shareLineToWeiXin(string url, string title, string description, int type)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
         return android.shareLineToWeiXin( url,  title, description,type);
#endif
#if UNITY_IOS && !UNITY_EDITOR
          ios.shareLineToWeiXin( url,  title, description,type);
#endif
            return "";
        }



        public void login(string scope, string state, string panel, string method)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
        android.login( scope,  state,  panel,  method);
#endif
#if UNITY_IOS && !UNITY_EDITOR
			ios.login(scope,state,panel,method);  
#endif
        }

        /// <summary>
        /// 获取剩余电量
        /// </summary>
        /// <returns>0-100 的stirng</returns>
        public void getCurBattery(string panel, string method)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
        android.getCurBattery(panel,method);
#endif

#if UNITY_IOS && !UNITY_EDITOR
		ios.getCurBattery(panel,method);  
#endif
        }

        /// <summary>
        /// 获取网络强度
        /// </summary>
        /// <returns></returns>
        public void getNetWorkStrength(string panel, string method)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
         android.getNetWorkStrength(panel,method);
#endif
#if UNITY_IOS && !UNITY_EDITOR
		ios.getNetWorkStrength(panel,method);  
#endif
        }



        /// <summary>
        ///震动 毫秒
        /// </summary>
        /// <param name="time"></param>
        public void vibrator(long time)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
          android.vibrator(time);
#endif
#if UNITY_IOS && !UNITY_EDITOR
			ios.vibrate(time);  
#endif
        }

        public string getShareInfo()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
          return android.getShareInfo();
#endif
#if UNITY_IOS && !UNITY_EDITOR
			return ios.getShareInfo();  
#endif
            return "";
        }

        /// <summary>
        /// 发送给好友,群 ,朋友圈 
        /// type 1 分享到好友或者好友群
        ///      2 分享到朋友圈
        /// </summary>
        /// <param name="bytes"></param>
        public void shareIconToWeiXin(byte[] bytes, int type)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR 
             android.shareIconToWeiXin(bytes,type);
#endif
#if UNITY_IOS && !UNITY_EDITOR
			 ios.shareIconToWeiXin(bytes,type);
#endif
        }


        public string getClipborad()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR 
            return android.getClipborad();
#endif
#if UNITY_IOS && !UNITY_EDITOR
            return ios.getClipborad();
#endif
            return "";
        }

        /// <summary>
        /// 微信支付,商户号,预支付交易会话ID,扩展字段,随机字符串,时间戳,签名
        /// </summary>
        /// <param name="partnerId">商户号</param>
        /// <param name="prepayId">预支付交易会话ID</param>
        /// <param name="packageVal">扩展字段</param>
        /// <param name="nonceStr">随机字符串</param>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="sign">签名</param>
        public void payWX(string partnerId, string prepayId, string packageVal, string nonceStr, string timeStamp,
            string sign)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
            android.payWX(partnerId,prepayId,packageVal,nonceStr,timeStamp,sign);
#endif
#if UNITY_IOS && !UNITY_EDITOR
            ios.payWx(partnerId,prepayId,packageVal,nonceStr,timeStamp,sign);
#endif
        }


        public void openGPS()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
            android.openGPS();
#endif
#if UNITY_IOS && !UNITY_EDITOR
            ios.openGPS();
#endif
        }

        //退出系统
        public void exitSystem()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
            android.exitSystem();
#endif
#if UNITY_IOS && !UNITY_EDITOR
            ios.exitsystem();
#endif
        }

        public string addPayDataToYM(int price, int count, int channel)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
            return android.addPayDataToYM(price,count,channel);
#endif
            return "error";
        }

        /// <summary>
        /// 获取GPS位置
        /// </summary>
        /// <returns></returns>
        public void getGPSLocation(string panel, string method)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
             android.getGPSLocation(panel,method);
#endif
#if UNITY_IOS && !UNITY_EDITOR
             ios.getGPSLocation(panel,method);
#endif
        }

        public string setClipContent(string content)
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
             return android.setClipContent(content);
#endif
#if UNITY_IOS && !UNITY_EDITOR
             return  ios.setClipContent(content);
#endif
            return "0";
        }


        /// <summary>
        /// 是否开启GPS
        /// </summary>
        /// <returns></returns>
        public bool isOpenGPS()
        {
            string result = "0";
#if (UNITY_ANDROID) && !UNITY_EDITOR
            result= android.isOpenGPS();
#endif
#if UNITY_IOS && !UNITY_EDITOR
            result= ios.isOpenGPS();
#endif
            return result == "1" ? true : false;
        }

        public bool isIpv6()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return ios.isIpv6();
#endif
            return false;
        }

        public bool isInstallWX()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return ios.isInstallWX();
#endif
            return true;
        }

        /// <summary>
        /// 分享链接到钉钉
        /// </summary>
        /// <param name="url"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="roomid"></param>
        /// <param name="token"></param>
        public void shareLineToDD(string url, string title, string description)
        {
            if (!isInstallDD())
            {
                return;
            }
#if (UNITY_ANDROID) && !UNITY_EDITOR
               android.shareLineToDD(url, title, description);
#endif
#if UNITY_IOS && !UNITY_EDITOR
              ios.shareLineToDD(url, title, description);
#endif
        }

        /// <summary>
        /// 分享图片到钉钉
        /// </summary>
        /// <param name="obj"></param>
        public void shareIconToDD(byte[] obj)
        {
            if (!isInstallDD())
            {
                return;
            }
#if (UNITY_ANDROID) && !UNITY_EDITOR
              android.shareIconToDD(obj);
#endif
#if UNITY_IOS && !UNITY_EDITOR
             ios.shareIconToDD(obj);
#endif


        }



        /// <summary>
        /// 是否安装钉钉
        /// </summary>
        /// <returns></returns>
        public bool isInstallDD()
        {
            bool b = true;
#if (UNITY_ANDROID) && !UNITY_EDITOR
              b=android.isInstallDD();
#endif
#if UNITY_IOS && !UNITY_EDITOR
              b=ios.isInstallDD();
#endif

            installDD(b);
            return b;
        }


        /// <summary>
        /// 钉钉
        /// </summary>
        /// <param name="b"></param>
        private void installDD(bool b)
        {
            if (!b)
            {
                Alert.confirmShow("未安装钉钉客户端\n 确认后，是否下载安装!", openDD);
            }
        }

        private void openDD(Transform trans)
        {
            Application.OpenURL("https://www.dingtalk.com/download");
        }

        /// <summary>
        /// 获取手机系统版本
        /// </summary>
        public string getSystemVersion()
        {
            string str = "";
#if (UNITY_ANDROID) && !UNITY_EDITOR
              str=android.getSystemVersion();
#endif
#if UNITY_IOS && !UNITY_EDITOR
             str=ios.getSystemVersion();
#endif
            return str;
        }

        /// <summary>
        /// 获取手机机型
        /// </summary>
        public string getSystemModel()
        {
            string str = "";
#if (UNITY_ANDROID) && !UNITY_EDITOR
              str=android.getSystemModel();
#endif
#if UNITY_IOS && !UNITY_EDITOR
             str=ios.getPhoneMode();
#endif
            return str;
        }

        /// <summary>
        /// 获取手机厂商
        /// </summary>
        public string getDeviceBrand()
        {
            string str = "";
#if (UNITY_ANDROID) && !UNITY_EDITOR
              str=android.getDeviceBrand();
#endif
#if UNITY_IOS && !UNITY_EDITOR
             str="apple";
#endif
            return str;
        }

        /// <summary>
        /// 当前环境是否是模拟器
        /// </summary>
        public bool isSimulator()
        {
#if (UNITY_ANDROID) && !UNITY_EDITOR
             return android.isSimulator();
#endif
            return false;
        }

    }
}
