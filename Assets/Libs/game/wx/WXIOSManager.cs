using System.Runtime.InteropServices;

namespace cambrian.game
{
    public class WXIOSManager
    {

        [DllImport("__Internal")]
        private static extern int _isInstallWX();

        [DllImport("__Internal")]
        private static extern bool _isInstallXL();

        [DllImport("__Internal")]
        private static extern bool _isInstallCN();

        [DllImport("__Internal")]
        private static extern bool _isInstallDD();

        [DllImport("__Internal")]
        private static extern void _initWeiXin(string appid);

        [DllImport("__Internal")]
        private static extern void _openWeiXin();

        [DllImport("__Internal")]
        private static extern void _exitsystem();


        [DllImport("__Internal")]
        private static extern int _checkVersion();

        [DllImport("__Internal")]
        private static extern void _login(string scope, string state, string panel, string method);
        [DllImport("__Internal")]
        private static extern void _loginXL(string state, string panel, string method);

        [DllImport("__Internal")]
        private static extern void _shareLineToWeiXin(string url, string title, string description,int type);
        [DllImport("__Internal")]
        private static extern void _shareLineToXianLiao(string url, string title, string description, int roomid,string token);

        [DllImport("__Internal")]
        private static extern void _shareRoomToChuiNiu(string url, string title, string description, int roomid, string token);

        [DllImport("__Internal")]
        private static extern void _shareIconToWeiXin(byte[] bytes, int length,int type);

        [DllImport("__Internal")]
        private static extern void _shareIconToXianLiao(byte[] bytes,int length);
        [DllImport("__Internal")]
        private static extern void _shareIconToDingDing(byte[] bytes, int length);

        [DllImport("__Internal")]
        private static extern void _shareLineToDingDing(string url, string title, string description);
        

        [DllImport("__Internal")]
        private static extern void _shareIconToChuiNiu(byte[] bytes, int length);

        [DllImport("__Internal")]
        private static extern void _vibrate();

        [DllImport("__Internal")]
        private static extern void _getCursBattery(string panel, string method);

        [DllImport("__Internal")]
        private static extern void _getNetWorksStrength(string panel, string method);

        [DllImport("__Internal")]
        private static extern string _getShareInfo();

        [DllImport("__Internal")]
        private static extern string _setClipContent(string content);

        
        [DllImport("__Internal")]
        private static extern string _getClipborad();

        [DllImport("__Internal")]
        private static extern void _payWx(string partnerId, string prepayId, string packageVal, string nonceStr, string timeStamp, string sign);

        [DllImport("__Internal")]
        private static extern void _openGPS();
        [DllImport("__Internal")]
        private static extern int _isOpenGPS();
        [DllImport("__Internal")]
        private static extern void _getGPSLocation(string panel,string method);

        [DllImport("__Internal")]
        private static extern bool _isIpv6();

        [DllImport("__Internal")]
        private static extern string _getPhoneMode();
        [DllImport("__Internal")]
        private static extern string _getSystemVersion();
        [DllImport("__Internal")]
        private static extern int _getSislePort(string id,int port);

        /// <summary>
        /// 获取超级盾端口
        /// </summary>
        /// <returns></returns>
        public int getSislePort(string id, int port)
        {
#if UNITY_IOS && !UNITY_EDITOR
          return _getSislePort(id,port);
#endif
            return -1;
        }

        public bool isInstallWX()
        {
#if UNITY_IOS && !UNITY_EDITOR
          return _isInstallWX()==1?true:false;
#endif
            return false;
        }

        public bool isInstallXL()
        {
#if UNITY_IOS && !UNITY_EDITOR
          return _isInstallXL();
#endif
            return true;
        }

        public bool isInstallDD()
        {
#if UNITY_IOS && !UNITY_EDITOR
          return _isInstallDD();
#endif
            return true;
        }

        public bool isInstallCN()
        {
#if UNITY_IOS && !UNITY_EDITOR
          return _isInstallCN();
#endif
            return true;
        }

        public void initWeiXin(string appid)
        {
#if UNITY_IOS && !UNITY_EDITOR
           _initWeiXin(appid);
#endif
        }

        /// <summary>
        /// 打开微信
        /// </summary>
        public void openWeiXin()
        {
#if UNITY_IOS && !UNITY_EDITOR
           _openWeiXin();
#endif
        }


        /// <summary>
        /// 退出系统应用
        /// </summary>
        public void exitsystem()
        {
#if UNITY_IOS && !UNITY_EDITOR
           _exitsystem();
#endif
        }


        /// <summary>
        /// 检查微信版本
        /// </summary>
        public bool checkVersion()
        {
#if UNITY_IOS && !UNITY_EDITOR
          return _checkVersion()==1?true:false;
#endif
            return false;
        }

        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="scope">作用域</param>
        /// <param name="state">状态</param>
        public void login(string scope, string state, string panel, string method)
        {
#if UNITY_IOS && !UNITY_EDITOR
			_login(scope,state,panel,method);
#endif
        }

        /// <summary>
        /// 分享链接给好友,群
        /// </summary>
        /// <param name="str"></param>
        public void shareLineToWeiXin(string url, string title, string description,int type)
        {
#if UNITY_IOS && !UNITY_EDITOR
           _shareLineToWeiXin(url,title,description,type);
#endif
        }

        /// <summary>
        /// 分享房间链接到闲聊
        /// </summary>
        /// <param name="url"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="roomid"></param>
        public void shareLineToXianLiao(string url, string title, string description, int roomid,string token)
        {
#if UNITY_IOS && !UNITY_EDITOR
           _shareLineToXianLiao(url,title,description,roomid,token);
#endif
        }

        /// <summary>
        /// 手机震动
        /// </summary>
        /// <param name="time"></param>
        public void vibrate(long time)
        {
#if UNITY_IOS && !UNITY_EDITOR
			_vibrate();
#endif
        }

        /// <summary>
        /// 获取剩余电量
        /// </summary>
        /// <returns></returns>
        public void getCurBattery(string panel, string method)
        {
#if UNITY_IOS && !UNITY_EDITOR
			 _getCursBattery(panel,method);
#endif
        }

        /// <summary>
        /// 获取网络强度
        /// </summary>
        /// <returns></returns>
        public void getNetWorkStrength(string panel, string method)
        {
#if UNITY_IOS && !UNITY_EDITOR
			_getNetWorksStrength(panel,method);
#endif
        }

        public string getShareInfo()
        {
#if UNITY_IOS && !UNITY_EDITOR
			return _getShareInfo();
#endif
            return "";
        }

        public void shareIconToWeiXin(byte[] bytes ,int type)
        {
            int length = bytes.Length;
#if UNITY_IOS && !UNITY_EDITOR
           _shareIconToWeiXin(bytes,length,type);
#endif
        }
        /// <summary>
        /// 分享图片到闲聊
        /// </summary>
        /// <param name="bytes"></param>
        public void shareIconToXianLiao(byte[] bytes)
        {
            int length = bytes.Length;
#if UNITY_IOS && !UNITY_EDITOR
           _shareIconToXianLiao(bytes,length);
#endif
        }

        //吹牛
        /// <summary>
        /// 分享图片到吹牛
        /// </summary>
        /// <param name="obj"></param>
        public void shareIconToChuiNiu(byte[] bytes)
        {
            int length = bytes.Length;
#if UNITY_IOS && !UNITY_EDITOR
           _shareIconToChuiNiu(bytes,length);
#endif
        }
        /// <summary>
        /// 分享信息到吹牛
        /// </summary>
        public void shareRoomToChuiNiu(string url, string title, string description, int roomid, string token)
        {
            _shareRoomToChuiNiu(url, title, description, roomid, token);
        }

        //钉钉
        public void shareIconToDD(byte[] bytes)
        {
            int length = bytes.Length;
#if UNITY_IOS && !UNITY_EDITOR
           _shareIconToDingDing(bytes,length);
#endif
        }

        public void shareLineToDD(string url, string title, string description)
        {
            _shareLineToDingDing(url,title,description);
        }

        /// <summary>
        /// 获取剪贴板内容
        /// </summary>
        public string getClipborad()
        {
#if UNITY_IOS && !UNITY_EDITOR
           return _getClipborad();
#endif
            return "";
        }

        public string setClipContent(string content)
        {
#if UNITY_IOS && !UNITY_EDITOR
           return _setClipContent(content);
#endif
            return "0";
        }

        /// <summary>
        /// 爱贝支付
        /// </summary>
        public void pay(string transid,string panel,string method)
        {
#if UNITY_IOS && !UNITY_EDITOR
        //   _pay(transid,panel,method);
#endif
        }

        /// <summary>
        /// 微信支付
        /// </summary>
        /// <param name="transid"></param>
        /// <param name="panel"></param>
        /// <param name="method"></param>
        public void payWx(string partnerId, string prepayId, string packageVal, string nonceStr, string timeStamp, string sign)
        {
#if UNITY_IOS && !UNITY_EDITOR
            _payWx(partnerId, prepayId, packageVal, nonceStr, timeStamp, sign);
#endif
        }

        public void openGPS()
        {
#if UNITY_IOS && !UNITY_EDITOR
           _openGPS();
#endif
        }

        public string isOpenGPS()
        {
            int r = 0;
#if UNITY_IOS && !UNITY_EDITOR
             r=_isOpenGPS();
#endif
            return r==1?"1":"0";
        }

        public void getGPSLocation(string panel, string method)
        {
#if UNITY_IOS && !UNITY_EDITOR
            _getGPSLocation(panel,method);
#endif
        }

        public bool isIpv6()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return _isIpv6();
#endif
            return false;
        }

        //获取机型
        public string getPhoneMode()
        {
#if UNITY_IOS && !UNITY_EDITOR
             return _getPhoneMode();
#endif
            return "";
        }

        /// <summary>
        /// 系统版本
        /// </summary>
        /// <returns></returns>
        public string getSystemVersion()
        {
#if UNITY_IOS && !UNITY_EDITOR
             return _getSystemVersion();
#endif
            return "";
        }
    }
}
