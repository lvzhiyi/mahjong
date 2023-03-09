namespace scene.game
{
    /// <summary>
    /// 接收服务器端发送过来的消息
    /// </summary>
    public class RecvConst
    {
        /* 长牌-房间-客户端接收 */
        ///<summary>房间-客户端接收-玩家加入房间</summary>
        public const int COMMAND_CLIENT_ROOM_ADD = 1102;

        ///<summary>房间-客户端接收-玩家离开房间 </summary>
        public const int COMMAND_CLIENT_ROOM_REMOVE = 1103;

        ///<summary>房间-客户端接收-玩家托管</summary>
        public const int COMMAND_CLIENT_ROOM_Trustee = 1104;

        ///<summary>房间-客户端接收-玩家准备 </summary>
        public const int COMMAND_CLIENT_ROOM_READY = 1114;

        ///<summary>房间-客户端接收-房间销毁 </summary>
        public const int COMMAND_CLIENT_ROOM_DESTORY = 1115;

        ///<summary>房间-比赛-操作 </summary>
        public const int COMMAND_CLIENT_ROOM_MATCH_OPTION = 902;

        ///<summary>房间-客户端接收-房间解散 </summary>
        public const int COMMAND_CLIENT_ROOM_DISBAND = 1117;

        ///<summary>房间-客户端接收-玩家状态 </summary>
        public const int COMMAND_CLIENT_ROOM_CONNECT_STATUS = 1118;

        /// <summary>房间-客户端接收-玩家GPS信息</summary>
        public const int COMMAND_CLIENT_ROOM_PLAYERS_GPS = 1119;
        /// <summary>
        /// 房间-客户端接收-分数变化
        /// </summary>
        public const int COMMAND_CLIENT_ROOM_UPDATE_SCORE = 1120;
        /// <summary>
        /// 房间-客户端接收-房间快速开始后，规则，人数的变化
        /// </summary>
        public const int COMMAND_CLIENT_ROOM_FAST_START = 1121;
        /// <summary>
        /// 房间-客户端接收-房主换人
        /// </summary>
        public const int COMMAND_CLIENT_ROOM_MASTER_CHANGE = 1122;
        /// <summary>
        /// 房间-客户端接收-接收房间邀请
        /// </summary>
        public const int COMMAND_CLIENT_ROOM_INVITE = 1081;

        /// <summary>
        /// 房间-客户端接收-再来一局
        /// </summary>
        public const int COMMAND_CLIENT_ROOM_AGAIN = 1082;



        /** 房间-接收-房间数据消息 */
        public const int PORT_CLIENT_ROOM_REFRESH_MSG = 6004;

        ///<summary>房间-客户端接收-语音消息 </summary>
        public const int COMMAND_CLIENT_ROOM_IMMESSAGE = 6000;

        /// <summary>聊天-接收-发送的快捷消息 </summary>
        public const int COMMAND_IM_READYFOR_QUICKMESSAGE = 6002;

        /// <summary>聊天-接收聊天消息 </summary>
        public const int COMMAND_IM_RECV_MESSAGE = 6003;

        /// <summary>玩家-客户端接收-房卡信息 </summary>
        public const int COMMAND_CLIENT_PLAYER_ROOOMCARDS = 831;
        /// <summary>玩家金币数据更新 </summary>
        public const int PORT_CLIENT_PLAYER_GOLD_UPDATE = 833;
        /// <summary>道具更新 </summary>
        public const int PORT_CLIENT_PROP_UPDATE = 834;
        /// <summary>代理状态更新 </summary>
        public const int PORT_CLIENT_PROMOTER_STATUS_UPDATE = 836;
        /// <summary>奖章更新 </summary>
        public const int PORT_CLIENT_MEDAL_UPDATE = 837;


        /** 休闲场-客户端接收-有新申请通知 */
        public const int PORT_CLIENT_CLUB_APPLICATION_NEW = 1001;

        /** 休闲场-客户端接收-申请处理状态接口 */
        public const int PORT_CLIENT_CLUB_APPLICATION_STATUS = 1002;
        /** 休闲场-客户端接收-馆主解散房间通知消息 */
        public const int PORT_CLIENT_CLUB_ROOM_DISBAND = 1003;

        /** 公告-客户端接收-增加滚动公告 */
        public const int PORT_CLENT_SCROLL_NOTICE_ADD = 1411;

        /** 公告-客户端接收-滚动公告更新） */
        public const int PORT_CLENT_SCROLL_NOTICE_UPDATE = 1412;

        /** 公告-客户端接收-移除滚动公告 */
        public const int PORT_CLENT_SCROLL_NOTICE_DELETE = 1413;

        //--------------------金币场-----------------------
        /// <summary>
        /// 金币场-客户端接收-房间匹配成功进入房间
        /// </summary>
        public const int PORT_CLIENT_GAMEAREA_ALLOT_SUCCESS = 1201;

        /// <summary>
        /// 金币场-客户端接收-房间匹配失败
        /// </summary>
        public const int PORT_CLIENT_GAMEAREA_ALLOT_TIMEOUT = 1202;

       

        /// <summary>
        /// 金币场-客户端接收-比赛操作
        /// </summary>
        public const int PORT_CLIENT_GAMEAREA_MATCH_OPTION = 1211;

        /// <summary>
        /// 金币场房间-客户端接收-玩家离开房间
        /// </summary>
        public const int PORT_CLIENT_GOLDROOM_EIXT = 1222;
        /// <summary>
        /// 金币场房间-客户端接收-房间销毁
        /// </summary>
        public const int PORT_CLIENT_GOLDROOM_DESTORY = 1224;

        /// <summary>
        /// 金币场-聊天-接收聊天消息
        /// </summary>
        public const int PORT_CLIENT_GOLD_CHAT_MSG = 6041;


        /// <summary>
        /// 二人对战游戏-客户端接收-对战邀请
        /// </summary>
        public const int PORT_CLIENT_DUELGAME_INVITE = 2201;

        /// <summary>
        /// 五子棋-客户端接收-比赛操作接口
        /// </summary>
        public const int PORT_CLIENT_WZQ_MATCH_OPTION = 2221;

        /// <summary>
        /// 道具-客户端接收-玩家使用道具
        /// </summary>
        public const int PORT_CLIENT_ROOM_USE_PROP = 1116;

        //-----------代理绑定--------------------
        /// <summary>
        /// 接收代理要求绑定消息
        /// </summary>
        public const int PORT_RECV_PROXY_BIND = 1204;
        /// <summary>
        /// 接收代理成员绑定增加消息
        /// </summary>
        public const int PORT_RECV_PROXY_MEMBER_ADD = 1203;

        //-------------竞赛场---------------------
        /// <summary>
        /// 玩家竞赛场金币数据更新
        /// </summary>
        public const int PORT_ARENA_PLAYER_GOLD_UPDATE = 2501;
        /// <summary>
        /// 玩家竞赛场推广任务数据更新
        /// </summary>
        public const int PORT_CLIENT_PLAYER_TASK_UPDATE = 2502;
    }
}
