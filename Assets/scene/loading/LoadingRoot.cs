using cambrian.common;
using cambrian.game;
using cambrian.uui;
using Libs.extend;
using scene.game;
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace scene.loading
{
    /// <summary> 加载场景图片 </summary>
    public class LoadingRoot : UnrealRoot
    {
        [EnumLabel("资源路径")]
        /// <summary> 地址 </summary>
        public UrlAddress url_addr = UrlAddress.Develop;

        public enum UrlAddress
        {
            [Header("正式服地址"), EnumDescription("http://134.122.136.28/mj/address/version.txt")] Online,
            [Header("开发版"), EnumDescription("http://192.168.0.112/mj/address/version.txt")] Develop,
        }


        private LoadingPanel loading;

        public bool isLogCollect;

        public static LoadingRoot roots = null;

        public static bool isDelete;

        public override void init()
        {
            roots = this;
            if (isDelete)
            {
                string path = CacheLocalPath.APPLICATION_PATH + "/ab";
                FileKit.delete(CacheLocalPath.APPLICATION_PATH + "/version.cfg");
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
                isDelete = false;
                Invoke("join", 1);
            }
            else
                join();
        }

        private void join()
        {
            //Lua虚拟机创建
            LuaUtil.instance();
            base.init();
            //添加摄像头
            add("prefab/SubCameras");
            //获取本地版本号
            VersionsCfg.load();

            ModuleManager.instance();

            //提示面板加载
            Alert.alert = getDisplayObject<Alert>();
            Alert.fixedalert = getDisplayObject<FixedAlert>();
            Alert.confirmAlert = getDisplayObject<Confirm>();
            Alert.fixedshow = getDisplayObject<FixedAlertShow>();

            //声音资源加载
            SoundManager.load();
            loadingGame();
        }


        public void loadingGame()
        {
            //加载界面
            loading = getDisplayObject<LoadingPanel>();
            loading.refresh();
            loading.setVisible(true);

            if (url_addr == UrlAddress.Online)//如果是正式服地址，需要检查是否实在模拟器上运行
            {
                if (WXManager.getInstance().isSimulator())
                    return;
            }

            //选择服务器
            Invoke("selectAddress", 0.1f);

            if (isLogCollect) Application.logMessageReceived += logCollect;
        }

        public void logCollect(string condition, string stackTrace, LogType type)
        {
            FileKit.writeText(
                string.Concat(CacheLocalPath.AB_RESROUCE_PATH, "log.txt"),
                string.Concat("\n ", TimeKit.getCurrTime(), " ", condition, "\n" + stackTrace),
                true);
        }

        public void selectAddress()
        {
            string address = "";
            switch (url_addr)
            {
                case UrlAddress.Develop:
                    address = EnumHelper.GetDescription(UrlAddress.Develop);
                    break;
                case UrlAddress.Online:
                    address = EnumHelper.GetDescription(UrlAddress.Online);
                    break;
            }

            address += "?" + MathKit.random(1, 10000);
            Loader.getLoader().loadBytes(address, localUrlBack);   //获取地址
        }

        private GameVersion version;
        public void localUrlBack(byte[] bytes)
        {
            if (bytes == null)
            {
                netError();
                return;
            }

            string str = Encoding.UTF8.GetString(bytes);
            version = JsonUtility.FromJson<GameVersion>(str);
            JsonUtility.FromJsonOverwrite(str, version);
            checkUpdate(version);
        }


        private PlatformVersion platform;

        /// <summary>
        /// 检查更新
        /// </summary>
        public void checkUpdate(GameVersion version)
        {
            int type = VersionsCfg.NO_UPDATE;
            platform = version.android;
            if (Platform.getPlatFormType() == Platform.IOS)
                platform = version.ios;

            if(VersionsCfg.cfg!=null)
                type = VersionsCfg.compareVersion(VersionsCfg.cfg.versions.getVersion(), platform.version);
            else
                type= VersionsCfg.compareVersion(versions, platform.version);

            if (type == VersionsCfg.DOWNLOAD)
            {
                type = VersionsCfg.compareVersion(versions, platform.version);
                if (type == VersionsCfg.DOWNLOAD)
                {
                    if (platform == version.android)
                    {
                        loading.downPack(downAction, version.android.downurl); //下载整包
                        return;
                    }
                    else
                    {
                        //打开网页下载
                        Application.OpenURL(platform.downurl + "?time=" + new DateTime().Millisecond);
                        return;
                    }
                }
                else
                {
                    if (VersionsCfg.cfg != null)
                        versions = VersionsCfg.cfg.versions.getVersion();
                }
            }

            ModuleManager.delete("mahjong.apk");

            if (type == VersionsCfg.NO_UPDATE)//无更新
            {
                joinGameScene();
            }
            else//资源更新
            {
                Loader.getLoader().loadBytes(platform.getUrl(), loadResBack);
            }
        }

        /// <summary>
        /// Android整包下载
        /// </summary>
        public void downAction()
        {
            WXManager.getInstance().installApk(CacheLocalPath.AB_RESROUCE_PATH+"mahjong.apk");
        }


        private ResUpdateList reslist;
        /// <summary>
        /// 资源更新
        /// </summary>
        public void loadResBack(byte[] bytes)
        {
            if (bytes == null)
            {
                netError();
                return;
            }

            string str = Encoding.UTF8.GetString(bytes);
            reslist = JsonUtility.FromJson<ResUpdateList>(str);
            JsonUtility.FromJsonOverwrite(str, reslist);
            ResData[] resData= ResUpdateModule.res.getNeedUpdateRes(reslist);

            if (resData.Length > 0)
                loading.updateRes(resData, downOver,netError);
            else
            {
                resData = ResUpdateModule.res.getResourcePackage(reslist);
                if (resData.Length > 0)
                {
                    string[] name = new string[resData.Length];
                    for (int i = 0; i < name.Length; i++)
                        name[i] = resData[i].packname;
                    ResUpdateModule.res.deleteDirectory(name);
                    loading.unCompress(resData, joinGameScene);
                }
                else
                    joinGameScene();
            }
        }

        public void netError()
        {
            Alert.fixedShow("请在良好的网络环境下游戏，以保证良好的游戏体验", s => this.selectAddress());
        }

        /// <summary>
        /// 资源更新完成
        /// </summary>
        public void downOver()
        {
            ResData[] resData = ResUpdateModule.res.getUnCompressRes(reslist);
            loading.unCompress(resData, joinGameScene);
        }

        /// <summary>
        /// 进入游戏场景
        /// </summary>
        public void joinGameScene()
        {
            //删除老文件夹
            if (reslist != null&&reslist.delteresdata!=null)
                ResUpdateModule.res.deleteDirectory(reslist.delteresdata);
            if(VersionsCfg.cfg==null)
                VersionsCfg.cfg=new VersionsCfg();
            VersionsCfg.cfg.setData(platform.version,platform.cfgurl);
            VersionsCfg.save();
            new SwtichSceneEvent(1).execute();
        }

        public override void update()
        {
            Alert.updateAlert();
            Alert.updateFixedAlert();
            Alert.updateFixedAlertShow();
            Alert.updateConfirmAlert();
            LuaUtil.update();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            dispose();
            if (ModuleManager.manager.getAbCfgModule() != null)
                disposeLuaFixed((string) ModuleManager.manager.getAbCfgModule().getPropertyValue("resetfixed"));
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
