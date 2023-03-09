using basef.arena;
using basef.bag;
using basef.im;
using basef.notice;
using basef.player;
using cambrian.common;
using cambrian.game;
using platform;
using platform.spotred.room;
using System;
using System.IO;
using UnityEngine;

namespace scene.game
{
    public class ConnectRoot : MonoBehaviour
    {
        /// <summary>
        /// 日志
        /// </summary>
        protected static cambrian.common.Logger log = LogFactory.getLogger<ConnectRoot>(true);
        /// <summary>
        /// 游戏服的连接
        /// </summary>
        public static TcpConnect gameconnect;

        private SpotRedRoot root;
        void Start()
        {
            root = (SpotRedRoot)UnrealRoot.root;
            CommandManager commandManager = gameObject.AddComponent<CommandManager>();
            commandManager.onCommandException = commandExceptionHandler;
        }

        public void initGameConnect()
        {
            if(root==null)
                root = (SpotRedRoot)UnrealRoot.root;
            if (gameconnect == null)
                gameconnect = this.gameObject.AddComponent<TcpConnect>();
            gameconnect.init("game", ServerInfos.server.getHost(), ServerInfos.server.getGamePort(), true,
                connectExceptionHandler);

            gameconnect.connect(this.onConnect);
        }

        public void onConnect(TcpConnect connect, bool enable)
        {
            if (!enable)
            {
                root.closeAlert();
                SpotRedRoot.dataLoading.refresh();
                root.initServer(false);
                return;
            }
            UserCommand.connect = connect;
            initRecvCommand();
            root.loading.setPercent(71);
            root.loading.refresh();
        }


        public void initRecvCommand()
        {
            initClientCommands();
        }

        /// <summary>
        /// 初始化接收消息接口
        /// </summary>
        public void initClientCommands()
        {
            ProxyDataHandler handler = UserCommand.connect.getTransmitHandler();

            handler.setRecvCommand(new RecvAddRoomCommand());
            handler.setRecvCommand(new RecvPlayerReadyCommand());
            handler.setRecvCommand(new RecvRoomADJustCommand());

            handler.setRecvCommand(new RecvPlayerConnectStatusComamnd());
            handler.setRecvCommand(new RecvRemovePlayerCommand());

            handler.setRecvCommand(new RecvRoomPlayersGPSInfoCommand());
            handler.setRecvCommand(new RecvRoomDestoryCommand()); //接收解散房间信息
            handler.setRecvCommand(new RecvRoomDisbandCommand());
            handler.setRecvCommand(new RecvRoomScoreUpdateCommand());
            handler.setRecvCommand(new RecvUpdateRoomMasterChangeCommand());
            handler.setRecvCommand(new RecvRoomInviteCommand());
            handler.setRecvCommand(new RecvRoomPlayerAginCommand());
            
            handler.setRecvCommand(new RecvRoomDataCommand());
            handler.setRecvCommand(new RecvMatchCommand());
            handler.setRecvCommand(new RecvPlayerCardCommand());
            handler.setRecvCommand(new RecvPlayerGoldCommand());
            handler.setRecvCommand(new RecvPlayerMedalCommand());
            handler.setRecvCommand(new RecvPropUpdateCommand());
            handler.setRecvCommand(new RecvPromoterStatusUpdateCommand());

            handler.setRecvCommand(new RecvAudioCommand());
            handler.setRecvCommand(new RecvBroadcastCommand());
            handler.setRecvCommand(new RecvIMQuickMsgCommand());
            handler.setRecvCommand(new RecvIMTextCommand());
            //接收托管消息
            handler.setRecvCommand(new RecvTrusteeCommand());

            //接收增加公告消息
            handler.setRecvCommand(new RecvNoticeAddMsgCommand());
            //接收公告更新消息
            handler.setRecvCommand(new RecvNoticeUpdateMsgCommand());
            //接收删除公告消息
            handler.setRecvCommand(new RecvNoticeDeleteMsgCommand());
            //道具使用消息
            handler.setRecvCommand(new RecvRoomPropUseCommand());
            
            handler.setRecvCommand(new RecvArenaGoldCommand());

            this.root.loading.setPercent(72);
            this.root.loading.refresh();
            //获取服务器时间
            GameDataRoot dataroot=gameObject.GetComponent<GameDataRoot>();
            if (dataroot == null)
                gameObject.AddComponent<GameDataRoot>().takeServerStatus();
            else
                dataroot.takeServerStatus();
        }

        /// <summary>
        /// 命令异常处理
        /// </summary>
        /// <param name="command"></param>
        /// <param name="exception"></param>
        public void commandExceptionHandler(Port command, Exception exception)
        {
            SpotRedRoot.dataLoading.hidden();
            if (log.isDebug)
                Debug.LogWarning(log.getMessage(exception + "\n" + exception.StackTrace));
            if (command is TcpCommand)
            {
                TcpConnect connect = ((TcpCommand)command).getConnect();
                if (connect.title == "game")
                {
                    this.connectExceptionHandler(connect, exception);
                }
            }
            else
            {
                root.closeAlert();
                SpotRedRoot.dataLoading.refresh();
                root.initServer(false);
            }
        }

        public void connectExceptionHandler(TcpConnect connect, Exception exception)
        {
            Action<Transform> exceptionHandler = tran =>
            {
                root.initServer(false);
            };

            //重置连接及相关状态值
            if (gameconnect != null) gameconnect.resetStatus();
            TimeKit._remoteCurrentTimeMillisDistance = -1;

            root.closeAlert();
            SpotRedRoot.dataLoading.refresh();

            root.initServer(false);
        }
    }
}
