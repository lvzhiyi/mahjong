using basef.server;
using cambrian.common;
using cambrian.game;
using cambrian.uui;
using Libs.extend;
using scene.loading;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using XLua;

namespace scene.game
{
    /// <summary>
    /// 点点红根节点
    /// </summary>
    [Hotfix]
    public class SpotRedRoot : UnrealRoot
    {
        /// <summary>
        /// 版本类型
        /// </summary>
        public enum VersionsType
        {
            [Header("开发版")] Develop = 0,
            [Header("测试版")] Beta = 1,
            [Header("稳定版")] Stable = 2,
        }

        [EnumLabel("版本类型")]
        /// <summary> 版本类型 </summary>
        public VersionsType versionstype = VersionsType.Develop;


        /// <summary>
        /// 加载界面
        /// </summary>
        [HideInInspector] public GameLoadingPanel loading;
        /// <summary>
        /// 数据加载界面
        /// </summary>
        public static LoadingDataPanel dataLoading;
        /// <summary>
        /// 是否加载完成
        /// </summary>
        [HideInInspector] public bool isLoadOk;
        /// <summary>
        /// GPS位置
        /// </summary>
        [HideInInspector] public GPSLocation gps;
        /// <summary>
        /// 重连次数
        /// </summary>
        [HideInInspector] public int conntimes;
        /// <summary>
        /// 最大重连次数
        /// </summary>
        public const int maxconntimes=3;
        /// <summary>
        /// 是否从头开始
        /// </summary>
        [HideInInspector] public bool isStart;

        public static SpotRedRoot roots=null;
        /// <summary>
        /// 是否是重新卸载登陆
        /// </summary>
        public static bool isFirstLogin;
        /// <summary>
        /// 地区模块
        /// </summary>
        public RegionModule regionModule { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void init()
        {
            LuaUtil.instance();
            base.init();
            roots = this;
            PropAnimationCacheManager.reset();
            Application.runInBackground = true;
            gameObject.AddComponent<SpotRedInitData>().init(roots);
           // Application.logMessageReceived += logCollect;
        }

        public void logCollect(string condition, string stackTrace, LogType type)
        {
            FileKit.writeText(CacheLocalPath.AB_RESROUCE_PATH+ "log.txt","\n "+TimeKit.getCurrTime()+" "+condition+"\n"+ stackTrace,true);
        }

        /// <summary>
        /// 选择地区
        /// </summary>
        public void selectRegions()
        {
            takeWhStatus();
        }


        public void closePanel()
        {
            foreach (Transform child in this.transform)
            {
                UnrealLuaPanel p = child.GetComponent<UnrealLuaPanel>();
                if (p == null) continue;
                if (p is GameLoadingPanel) continue;
                p.setVisible(false);
            }
        }

        public void regionExitCallBack()
        {
            takeWhStatus();
        }

        /// <summary>
        /// 获取当前服务器状态，是否是在维护
        /// </summary>
        public void takeWhStatus()
        {
            //获取地址
            Loader.getLoader().loadBytes(regionModule.region.getNoticeUrl(), noticeback);
        }

        private WhNotice notice;

        public void noticeback(byte[] bytes)
        {
            if (bytes == null)
            {
                netError();
                return;
            }

            string str = Encoding.UTF8.GetString(bytes);
            notice = JsonUtility.FromJson<WhNotice>(str);
            JsonUtility.FromJsonOverwrite(str, notice);

            if (notice.isWhing())
            {
                Alert.fixedShow(notice.statusNotic, s => this.takeWhStatus());
                return;
            }
            else
            {
                initServer(true);
            }
        }

        public void netError()
        {
            dataLoading.hidden();
            Alert.fixedShow("请在良好的网络环境下游戏，以保证良好的游戏体验", s => this.takeWhStatus());
        }

        /// <summary>
        /// 初始化服务器
        /// </summary>
        public void initServer(bool initstart)
        {
            isStart = initstart;
            isLoadOk = false;
            if (conntimes > 0 && conntimes < maxconntimes)
            {
                conntimes++;
                takeServerAddresses();
            }
            else
            {
                if (conntimes >= maxconntimes)
                {
                    dataLoading.hidden();
                    Alert.fixedShow("请在良好的网络环境下游戏，以保证良好的游戏体验", s => this.takeServerAddresses());
                }
                else
                {
                    takeServerAddresses();
                }

                conntimes = 1;
            }
        }

        public void takeServerAddresses()
        {
            initQuitWindow();
        }

        public void initQuitWindow()
        {
            this.loading.setPercent(40);
            this.loading.refresh();
            SelectServer();
        }

        public void SelectServer()
        {
            if (versionstype == VersionsType.Develop || versionstype == VersionsType.Beta)
            {
				dataLoading.hidden();
                showServersPanel(ServerInfos.addresses.servers);
            }
            else
            {
                ServerInfos.server = ServerInfos.addresses.servers[0];
                onSelectServerAddress();
            }
        }

        /// <summary>
        /// 选服务器界面
        /// </summary>
        public void showServersPanel(Server[] servers)
        {
            ServersPanel panel = this.getDisplayObject<ServersPanel>();
            panel.setServers(servers);
            panel.refresh();
            panel.relayout();
            panel.setVisible(true);
        }

        public void onSelectServerAddress()
        {
            Invoke("initSample", 0.1f);
        }

        public void initSample()
        {
            Sample.factory.clear();
            XmlLoadManager.loadXml();
            Sample.factory.initAndCheck();

            //初始化微信   appid 
            WXManager.getInstance().initWeiXin("wxd1faa7048137c047");
            this.loading.setPercent(60, true);
            this.loading.refresh();

            Invoke("initConnect", 0.1f);
        }

        public void initConnect()
        {
            if (versionstype == VersionsType.Stable)
                showUsersPanel(true);
            else
                showUsersPanel(false);
        }

        bool quick;

        public void showUsersPanel(bool quick)
        {
            this.quick = quick;

            string path = Application.persistentDataPath + "/" + ServerInfos.server.id + "/user";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string[] subs = Directory.GetDirectories(path);
            User[] users = new User[subs.Length];
            for (int i = 0; i < subs.Length; i++)
            {
                long userid = Int64.Parse(subs[i].Substring(path.Length + 1));
                users[i] = User.load(ServerInfos.server.id, userid);
            }

            if (this.quick)
            {
                if (users.Length == 0)
                {
                    loading.showWeiXinBtn();
                    loading.hideLoading();
                    onAddress(new User());
                    //if (isFirstLogin)
                    //    new OpenRegionPanelProcess().execute();
                }
                else if (users[0] != null)
                {
                    onAddress(users[0]);
                }
            }
            else
            {
                UsersPanel panel = getDisplayObject<UsersPanel>();
                panel.setUsers(users);
                panel.refresh();
                panel.setVisible(true);
            }
        }

        /// <summary>
        /// 游客登陆
        /// </summary>
        public void guestLogin()
        {
            if (notice.isWhing())
            {
                Alert.fixedShow(notice.statusNotic, s => this.takeWhStatus());
                return;
            }

            User user = new User();
            this.onAddress(user);
        }

        public void onAddress(User user)
        {
            if (notice.isWhing())
            {
                Alert.fixedShow(notice.statusNotic, s => this.takeWhStatus());
                return;
            }

            User.user = user;
            loading.setPercent(70);
            loading.refresh();

            if (Platform.getPlatFormType() != Platform.PC)
                YunVaImManager.yunva.init();


            dataLoading.setVisible(true);
            ConnectRoot connect = this.gameObject.GetComponent<ConnectRoot>();
            if (connect == null)
                gameObject.AddComponent<ConnectRoot>().initGameConnect();
            else
                connect.initGameConnect();
        }

        /// <summary>
        /// 是否有网络
        /// </summary>
        /// <returns></returns>
        public bool internetReachability()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                return false;
            return true;
        }

        /// <summary>
        /// 当程序启动和切换程序时调用
        /// </summary>
        /// <param name="focusStatus"></param>
        void OnApplicationFocus(bool focusStatus)
        {
            if (!isLoadOk) return;
            
            if (!focusStatus) //失去焦点,切出去
            {
                //CommandManager.addCommand(new UpdatePlayerStateCommand(false));
            }
            else
            {
                //if (focusStatus)
                //{
                //    if (CommandManager.time1>0&&TimeKit.currentTimeMillis-CommandManager.time1>5*TimeKit.MIN_MILLS)
                //    {
                //        takeWhStatus();
                //    }
                //}
            }
        }

        public override void update()
        {
            base.update();
            if (Application.platform == RuntimePlatform.Android && (Input.GetKeyDown(KeyCode.Escape)))
            {
                //提示退出 缺提示窗口
                ExitPanel panel = root.getDisplayObject<ExitPanel>();
                if (panel.enabled) panel.setVisible(true);
            }
            Alert.updateAlert();
            Alert.updateFixedAlert();
            Alert.updateFixedAlertShow();
            Alert.updateConfirmAlert();

            LuaUtil.update();

            if (!internetReachability())
            {
                Alert.fixedShow("网络错误,请重新连接 \n 请在良好的网络环境下游戏，以保证良好的游戏体验", s => initServer(false));
                return;
            }
        }
        public void closeAlert()
        {
            Alert.alert.setVisible(false);
            Alert.confirmAlert.setVisible(false);
            Alert.fixedalert.setVisible(false);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            if (ConnectRoot.gameconnect != null)
                ConnectRoot.gameconnect.close();
            root.dispose();
            disposeLuaFixed((string)regionModule.region.module.getAbCfgModule().getPropertyValue("resetfixed"));
            disposeLuaFixed((string)ModuleManager.manager.getAbCfgModule().getPropertyValue("resetfixed"));
            roots = null;
            LuaUtil.onDestroy();
        }

       


        public void disposeLuaFixed(string name)
        {
            string path = CacheLocalPath.AB_RESROUCE_PATH + name;

            if (File.Exists(path))
            {
                string luapath = path;
                LuaUtil.luaEnv.AddLoader((ref string filepath) =>
                {
                    if (File.Exists(filepath))
                    {
                        byte[] data = File.ReadAllBytes(filepath);
                        return data;
                    }
                    else
                    {
                        return null;
                    }
                });
                LuaUtil.luaEnv.DoString(@"
                require '" + luapath + "'");
            }
        }
    }
}
