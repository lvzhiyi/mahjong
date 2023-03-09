using basef.activity;
using basef.bag;
using basef.lobby;
using basef.player;
using basef.server;
using basef.setting;
using cambrian.common;
using cambrian.game;
using cambrian.uui;
using platform;
using platform.spotred.room;
using scene.loading;
using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace scene.game
{
    public class GameDataRoot : MonoBehaviour
    {
        /// <summary> 日志 </summary>
        protected static cambrian.common.Logger log = LogFactory.getLogger<ConnectRoot>(true);

        private SpotRedRoot root;

        public static GameDataRoot gameDataRoot;


        void Start()
        {
            root = (SpotRedRoot) UnrealRoot.root;
            gameDataRoot = this;
        }


        public void takeServerStatus()
        {
            CommandManager.addCommand(new ServerStatusCommmand(), this.serverStaus);
        }

        public void serverStaus(object obj)
        {
            CommandManager.addCommand(new GetClientVersionCommand(), clientVersion);
        }

        public void clientVersion(object obj)
        {
            Versions version = (Versions) obj;
            if (version.compareTo(new Versions(UnrealRoot.root.versions)) == Versions.COMP_GT)
            {
                new SwtichSceneEvent(0).execute();
                return;
            }

            onServerStatus(null);
        }

        void onServerStatus(object obj)
        {
            //推荐人
            long master = 0;

            //
            // User.user.userid = 5015897;
             //User.user.passcheck = "05sdA0$CW70mplccZS3d$6RQxvuxbLag8bFTIo";

            if (User.user.passcheck == null)
            {
                string str = WXManager.getInstance().getClipborad();
                if (UtilKit.isNullString(str) || "-1" == str) //-1指定剪贴板上没有内容。主要是针对于ios
                    master = 0;
                else
                {
                    string[] strs = Regex.Split(str, "master", RegexOptions.IgnoreCase);
                    if (strs.Length < 2) master = 0;
                    else
                    {
                        try
                        {
                            master = StringKit.parseLong(strs[1]);
                        }
                        catch (Exception)
                        {
                            master = 0;
                        }
                    }
                }

                if (log.isDebug)
                    Debug.Log(log.getMessage(" ,master=" + master));

                root.loading.setPercent(73);
                root.loading.refresh();

                if (User.user.weixintmpcode == null)
                {
                    root.closeAlert();
                    SpotRedRoot.dataLoading.hidden();
                    if (root.versionstype == SpotRedRoot.VersionsType.Develop)
                    {
                        CommandManager.addCommand(new RegistByDeviceCommand(User.user, master), onLogin);
                    }
                }
            }
            else
            {
                root.loading.setPercent(75);
                root.loading.refresh();
                CommandManager.addCommand(new CertifyByPasscheckCommand(User.user), onCertifyByPasscheck);
            }
        }

        /// <summary>
        /// 只通过 点击按钮触发 
        /// </summary>
        public void weixinLogin()
        {
            WXManager.getInstance().login("snsapi_userinfo", "zs", "Root", "WeiXinLogin");
        }

        public void onWeixinLogin(string info)
        {
            string[] strs = StringKit.parseStrings(info);
            if (strs[0] == "0")
            {
                root.loading.setPercent(74);
                root.loading.refresh();
                User.user.weixintmpcode = strs[1];
                CommandManager.addCommand(new CertifyByWeixinCommand(User.user, 0, CertifyByWeixinCommand.WEIXIN_CODE_TYPE), codeOnLogin);
            }
            else
            {
                Alert.show("请重新授权微信登录");
            }
        }

        public void codeOnLogin(object obj)
        {
            if (obj == null) return;
            root.loading.setPercent(76);
            root.loading.refresh();
            onPlayerInfo("");
        }

        /// <summary>
        /// 密码登录
        /// </summary>
        public void PasswordLogin(long userid,string password)
        {
            CommandManager.addCommand(new CertifyByPasswordCommand(userid, password), onLogin);
        }

        /// <summary>
        /// 验证码登录
        /// </summary>
        /// <param name="verifyCode"></param>
        public void verifycodeLogin(string phone,string verifyCode)
        {
            CommandManager.addCommand(new CertifyVerifyCodeCommand(phone,verifyCode), onLogin);
        }

        void onLogin(object obj)
        {
            if (obj == null)
            {
                root.closeAlert();
                SpotRedRoot.dataLoading.refresh();
                root.initServer(false);
            }
            else
            {
                root.loading.setPercent(76);
                root.loading.refresh();
                onPlayerInfo("");
            }
        }


        void onCertifyByPasscheck(object obj)
        {
            UsersPanel usersPanel = this.root.checkDisplayObject<UsersPanel>();
            if (usersPanel != null)
                usersPanel.setVisible(false);
            if (obj == null)
            {
                User.logout(User.user);
                Alert.fixedShow("授权过期,请重新授权 \n ", s => this.root.takeWhStatus());
            }
            else
            {
                root.loading.setPercent(77);
                root.loading.refresh();
                onPlayerInfo("");
            }
        }

        public void onPlayerInfo(object obj)
        {
            if (UnrealRoot.root.checkDisplayObject<OverPanel>() != null)
                UnrealRoot.root.getDisplayObject<OverPanel>().setVisible(false);
            if (UnrealRoot.root.checkDisplayObject<DisbandPanel>() != null)
                UnrealRoot.root.getDisplayObject<DisbandPanel>().setVisible(false);

            root.loading.setPercent(78);
            root.loading.refresh();
            loadOk(null);
        }

        public void loadOk(object obj)
        {
            this.root.loading.setPercent(81);
            this.root.loading.refresh();
            SpotRedRoot.dataLoading.refresh();

            CommandManager.addCommand(new PlayerInfoCommand(), infoBack);
        }

        public void infoBack(object obj)
        {
            ByteBuffer data = (ByteBuffer)obj;

            Player.player.bytesRead(data);
            Player.player.name = User.user.nickname;
            PlayerToken.instance.bytesRead(data);
            UtilKit.refreshPlayerToken();
            WXManager.getInstance().getGPSLocation("Root", "call_back_gps");
            Room.clear();
            //加载房间数据
            new LoadRoomData(data).execute();
        }


        /// <summary>
        /// 进游戏后的默认显示界面
        /// </summary>
        public void showDefaultWindow()
        {
            SpotRedRoot.dataLoading.hidden();
            foreach (Transform child in this.transform)
            {
                UnrealLuaPanel p = child.GetComponent<UnrealLuaPanel>();
                if (p == null) continue;
                if (p is GameLoadingPanel) continue;
                if (p is Alert) this.root.closeAlert();
            }

            root.loading.setPercent(90);
            root.loading.refresh();
            SpotRedRoot.dataLoading.hidden();

            if (Platform.getPlatFormType() != Platform.PC)
                YunVaImManager.yunva.onLogin(Player.player.userid,SpotRedRoot.roots.regionModule.region.id, yunWaLoginRes);

            new ReconnectRoomProcess(initOk).execute(); //执行重连

            if (root.gps == null) root.gps = gameObject.AddComponent<GPSLocation>();
            root.gps.doStart();

            root.conntimes = 1;

            MaskWord.loadMaskWord();

            SoundManager.playBackMusic();
        }

        /// <summary>
        /// 云娃登陆结果
        /// </summary>
        /// <param name="res"></param>
        public void yunWaLoginRes(bool res)
        {
            if (res)
            {
               
            }
        }

        private void initOk(object obj)
        {
            if (!(bool) obj)
            {
                ShowLobbyPanel.showLobbyPanel();
            }

            openNoticeBoardPanel();

            root.loading.setPercent(100);
            root.loading.refresh();

            root.isLoadOk = true;
            SpotRedRoot.dataLoading.hidden();
            clearPrefab();
        }

        public void clearPrefab()
        {
            UnrealRoot.root.clearGameObject<ServersPanel>();
            UnrealRoot.root.clearGameObject<UsersPanel>();
            //提交IP
            string url = ServerInfos.server.getDsUrl() + "/submitIP?" + MathKit.random(1, 10000);
            postIP(url);
        }

        public void postIP(string url)
        {
            ByteBuffer data = new ByteBuffer();
            data.writeLong(Player.player.userid);
            data.writeInt(Platform.getPlatFormType());

            Loader.getLoader().post(url, data, obj =>
            {
                byte[] b = (byte[]) obj;
                string str = "";
                if (b == null || b.Length == 0)
                {

                }
                else
                {
                    str = System.Text.Encoding.UTF8.GetString(b);
                }
            });
        }


        //打开活动面板
        public void openNoticeBoardPanel()
        {
            new AutoOpenNoticeBoardPanelProcess().execute();
        }
    }
}
