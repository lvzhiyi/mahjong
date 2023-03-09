namespace scene.game
{
    /// <summary>
    /// 通信命令
    /// </summary>
    public class CommandConst
    {

        ///<summary> GPS信息 </summary>

        /// <summary> GPS-获取玩家GPS信息 </summary>
        public const int COMMAND_GPS_INFO_GET = 821;

        /// <summary> GPS-更新玩家GPS信息 </summary>
        public const int COMMAND_GPS_INFO_UPDATE = 822;

        /// <summary> 长牌-玩家-获取玩家基本数据 </summary>
        public const int COMMAND_MAHJONG_PLAYER_INFO = 811;

        ///<summary> 玩家-实名认证信息提交 </summary>
        public const int PORT_PLAYER_SUBMIT_SMRZ = 814;

        ///<summary> 玩家身份证及姓名信息提交 </summary>
        public const int PORT_PLAYER_SUBMIT_SFRZ = 822;

        ///<summary> 玩家-获取已使用代币兑换房卡次数 </summary>
        public const int PORT_PLAYER_TOKEN_EXCHANGE_COUNT = 815;

        /// <summary>玩家-修改密码</summary>
        //public const int PORT_PLAYER_UPDATA_PASSWORD = 816;
        public const int PORT_PLAYER_UPDATA_PASSWORD = 823;

        /// <summary>玩家-设置密码</summary>
        public const int PORT_PLAYER_SET_PASSWORD = 817;

        /// <summary>玩家-请求-账号绑定验证码</summary>
        public const int PORT_PLAYER_REQUEST_VERIFYCODE_BIND = 818;

        /// <summary>玩家-领取实名认证奖励</summary>
        public const int PORT_PLAYER_TAKE_SMRZ_AWARD = 819;

        /// <summary>房间-刷新房间数据 </summary>
        public const int PORT_ROOM_REFRESH = 6105;
        ///<summary> 房间-创建房间 </summary>
        public const int COMMAND_ROOM_CTEATE = 910;

        ///<summary> 房间-玩家加入房间 </summary>
        public const int COMMAND_ROOM_ADD = 911;

        ///<summary> 房间-玩家离开房间 </summary>
        public const int COMMAND_ROOM_REMOVE = 912;

        ///<summary> 房间-玩家准备 </summary>
        public const int COMMAND_ROOM_READY = 913;

        ///<summary> 房间-发送-托管 </summary>
        public const int PORT_ROOM_TRUSTEE = 914;

        ///<summary> 房间-比赛_验证步数 </summary>
        public const int COMMAND_ROOM_MATCH_STEP = 916;

        ///<summary> 房间-比赛_操作 </summary>
        public const int COMMAND_ROOM_MATCH_OPTION = 901;

        ///<summary> 房间-发起解散房间操作 </summary>
        public const int COMMAND_ROOM_DISBAND = 918;

        ///<summary> 房间-代开房间 </summary>
        public const int PORT_ROOM_CREATE_BY_PROMOTER = 919;
        ///<summary> 房间-获得代开房间列表 </summary>
        public const int PORT_ROOM_PROMOT_LIST = 920;
        ///<summary> 房间-获得已结算的房间列表 </summary>
        public const int PORT_ROOM_PROMOT_OVER_LIST = 921;
        ///<summary> 房间-解算代开房间 </summary>
        public const int PORT_ROOM_PROMOT_DISBIND_ROOM = 922;
        ///<summary> 房间-获取分享的房间规则 </summary>
        public const int PORT_ROOM_PROMOT_RULE_ROOM = 923;

        ///<summary> 房间-选择房数-2房或者3房 </summary>
        public const int PORT_ROOM_SELECT_FS = 924;


        ///<summary> 房间-普通房间-再来一局</summary>
        public const int PORT_ROOM_NORMAL_AGAIN = 925;

        /// <summary>
        /// 同意邀请加入房间
        /// </summary>
        public const int PORT_AGREE_INVITE_JOIN_ROOM = 926;


        /// <summary> 更新玩家状态 </summary>
        public const int COMMAND_PLAYER_CONNECT_STATUS = 812;

        ///<summary> 排行-前端-获取当周排行 </summary>
        public const int PORT_RANK_LIST_NOW = 1201;
        ///<summary> 排行-前端-获取上周排行 </summary>
        public const int PORT_RANK_LIST_LAST = 1202;
        ///<summary> 排行-前端-获取双周排行 </summary>
        public const int PORT_RANK_LIST_DOUBLE_WEEK = 1203;
        ///<summary> 排行-前端-获取今日排行 </summary>
        public const int PORT_FKC_RANK_LIST_DAY = 1204;
        ///<summary> 排行-前端-获取昨日排行 </summary>
        public const int PORT_FKC_RANK_LIST_LAST_DAY = 1205;


        ///<summary> 获取活动列表 </summary>
        public const int PORT_PROMOTION_LIST = 1300;
        ///<summary> 获取活动结果信息 </summary>
        public const int PORT_PROMOTION_RESULT = 1302;
        ///<summary> 获取公告列表 </summary>
        public const int PORT_NOTICEBOARD_LIST = 1402;


        /// <summary>聊天-发送快捷聊天消息 </summary>
        public const int COMMAND_IM_QUICKMESSAGE = 6103;

        /// <summary> 聊天-发送聊天消息 </summary>
        public const int COMMAND_IM_SENDMESSAGE = 6104;

        /// <summary>聊天-发送语音消息 </summary>
        public const int COMMAND_IM_SENDAUDIO = 6102;

        /*--------------------------休闲场--------------------------*/
        /// <summary> 获取玩家休闲场列表 </summary>
        public const int PORT_CLUB_LIST = 1001;

        /// <summary> 创建休闲场 </summary>
        public const int PORT_CLUB_CREATE = 1002;

        /// <summary> 移除休闲场 </summary>
        public const int PORT_CLUB_REMOVE = 1003;

        /// <summary> 加入休闲场 </summary>
        public const int PORT_CLUB_JOIN = 1004;

        /// <summary> 退出休闲场 </summary>
        public const int PORT_CLUB_EXIT = 1005;

        /// <summary> 获取休闲场信息 </summary>
        public const int PORT_CLUB_INFO = 1006;

        /// <summary> 休闲场充值 </summary>
        public const int PORT_CLUB_ADD_TOKEN = 1008;

        /// <summary>
        /// 获取休闲场成员列表
        /// </summary>
        public const int PORT_CLUB_MEMBER_LIST = 1011;
        /// <summary>
        /// 添加休闲场成员
        /// </summary>
        public const int PORT_CLUB_MEMBER_ADD = 1012;
        /// <summary>
        /// 移除休闲场成员
        /// </summary>
        public const int PORT_CLUB_MEMBER_REMOVE = 1013;
        /// <summary>
        /// 获取休闲场消息列表
        /// </summary>
        public const int PORT_CLUB_APPLICATION_LIST = 1014;
        /// <summary>
        /// 处理休闲场消息
        /// </summary>
        public const int PORT_CLUB_APPLICATION_OPTION = 1015;
        /// <summary>
        /// 获取休闲场结算列表
        /// </summary>
        public const int PORT_CLUB_ACCOUNT_LIST = 1016;
        /// <summary>
        /// 结算休闲场账目
        /// </summary>
        public const int PORT_CLUB_ACCOUNT_OPTION = 1017;
        /// <summary>
        /// 获取休闲场邀请码
        /// </summary>
        public const int PORT_CLUB_INVITATION_CODE = 1018;

        /// <summary>
        /// 成员权限
        /// </summary>
        public const int PORT_CLUB_MEMBER_PERMIT = 1019;

        /// <summary>
        /// 创建休闲场房间
        /// </summary>
        public const int PORT_CLUB_ROOM_CREATE = 1021;
        /// <summary>
        /// 获取休闲场房间列表
        /// </summary>
        public const int PORT_CLUB_ROOM_LIST = 1022;
        ///<summary> 获取休闲场房卡场账单记录 </summary>
        public const int PORT_CLUB_ROOM_CARD_BILL = 1023;
        /// <summary>
        /// 获取休闲场单个房间信息
        /// </summary>
        public const int PORT_CLUB_ROOM_SINGLE_MSG = 1024;

        /// <summary>
        /// 10分钟内不再接收邀请
        /// </summary>
        public const int PORT_CLUB_ROOM_NO_ACCEPT_INVITE_TIME = 1030;



        ///<summary> 更新休闲场基础设置 </summary>
        public const int PORT_CLUB_SETTINGS_BASE = 1031;
        ///<summary> 更新休闲场房卡场设置 </summary>
        public const int PORT_CLUB_SETTINGS_FKC = 1032;

        /// <summary> 增加或者锁定规则 </summary>
        public const int PORT_CLUB_ADD_UPDATE_LOCKED_RULE = 1034;

        /// <summary> 删除茶馆锁定规则 </summary>
        public const int PORT_CLUB_DELETE_LOCKED_RULE = 1035;


        ///<summary> 创建休闲场金币房间 </summary>
        public const int PORT_CLUB_ROOM_JBC_CREATE = 1041;
        ///<summary> 获取休闲场金币房间列表 </summary>
        public const int PORT_CLUB_ROOM_JBC_LIST = 1042;
        ///<summary> 获取休闲场金币房间详细信息 </summary>
        public const int PORT_CLUB_ROOM_JBC_DETAIL = 1044;
        ///<summary> 获取休闲场金币账单记录 </summary>
        public const int PORT_CLUB_ROOM_JBC_BILL = 1043;

        /// <summary>
        /// 休闲场代币兑换金豆
        /// </summary>
        public const int PORT_CLUB_TOKEN_EXCHANGE = 1007;

        /// <summary>
        /// 获取休闲场活动获奖信息
        /// </summary>
        public const int PORT_CLUB_ACTIVITY_WIN_AWARD_DATA = 1061;
        /// <summary>
        /// 领取取休闲场活动奖励
        /// </summary>
        public const int PORT_CLUB_ACTIVITY_TAKE = 1062;
        /// <summary>
        /// 获取休闲场排行
        /// </summary>
        public const int PORT_CLUB_ACTIVITY_RANK_LIST = 1063;


        /** 解散指定房间 */
        public const int PORT_CLUB_ROOM_DISBAND = 1025;
        /** 快速组局 */
        public const int PORT_CLUB_ROOM_FAST_GAME = 1026;

        /** 竞赛场快速开局 */
        public const int PORT_ARENA_QUICK_GAME = 2572;
        /** 增加互斥名单 */
        public const int PORT_CLUB_MUTEX_ADD = 1036;
        /** 减少互斥名单 */
        public const int PORT_CLUB_MUTEX_DEL = 1037;
        /** 更改互斥名单 */
        public const int PORT_CLUB_MUTEX_UPDATE = 1038;
        /** 获取互斥名单 */
        public const int PORT_CLUB_MUTEX_GET = 1039;
        /** 茶馆打烊 */
        public const int PORT_CLUB_SUSPEND = 1040;

        /** 获取指定成员结算信息列表 */
        public const int PORT_CLUB_MEMBER_ACCOUNT_LIST = 1077;


        /// <summary>
        /// 获取副馆主列表
        /// </summary>
        public const int PORT_CLUB_ASSISTANT_GET = 1073;

        /// <summary>
        /// 副馆主设置
        /// </summary>
        public const int PORT_CLUB_ASSISTANT_SETTING = 1071;

        /// <summary>
        /// 副馆主权限编辑
        /// </summary>
        public const int PORT_CLUB_ASSISTANT_PERMISSION_EDIT = 1072;

        /// <summary>
        /// 获取休闲场增加房卡记录
        /// </summary>
        public const int PORT_CLUB_TOTAL_ADD_TOKEN_BILL = 1074;
        /// <summary>
        /// 获取休闲场指定玩家增加房卡记录
        /// </summary>
        public const int PORT_CLUB_PLAYER_ADD_TOKEN_BILL = 1075;
        /// <summary>
        /// 获取休闲场成员变动记录列表
        /// </summary>
        public const int PORT_CLUB_MEMBER_CHANGE_LIST = 1076;


        /// <summary>
        /// 茶馆房间再来一局
        /// </summary>
        public const int PORT_CLUB_AGAIN_ROOM = 1082;


        /// <summary>
        /// 获取是否是指定休闲场的成员
        /// </summary>
        public const int PORT_CLUB_ASSIGN_MEMBER = 2205;
        //公告
        /// <summary>
        /// 公告-获取滚动公告列表
        /// </summary>
        public const int POST_SCROLLNOTICE_LIST = 1401;
        /// <summary>
        /// 公告-获取普通公告列表
        /// </summary>
        public const int POST_GENERALNOTICE_LIST = 1402;

        /// <summary>
        /// 获取服务器版本（客户端服务器的版本信息）
        /// </summary>
        public const int PORT_VERSIONS = 0x12;

        //------------------金币场排行榜----------------------------
        /// <summary>
        /// 获取金币场排行组
        /// </summary>
        public const int PORT_GOLDRANK_GROUP_GET = 2031;
        /// <summary>
        /// 获取指定金币场排行榜
        /// </summary>
        public const int PORT_GOLDRANK_GET = 2032;

       
        //-------------------------道具---------------------------------------
        /// <summary>
        /// 道具-房间内使用道具
        /// </summary>
        public const int PORT_ROOM_PROP_USE = 917;
        ///<summary> 道具-使用道具 </summary>
        public const int PORT_PROP_USE = 1701;
       

        //--------------------------大厅-----------------------------------
        public const int PORT_PLAYER_BIND_XIANLIAO = 820;

        //---------------竞赛场------------------

        /// <summary> 获取竞赛场列表 </summary>
        public const int PORT_ARENA_LIST = 2501;

        /// <summary> 创建竞赛场 </summary>
        public const int PORT_ARENA_CREATE = 2502;

        /// <summary> 修改竞赛场基础设置 </summary>
        public const int PORT_ARENA_SETTING_BASE = 2551;

        /// <summary> 修改竞赛场同桌限制 </summary>
        public const int PORT_ARENA_LIMIT_SETTING_BASE = 2554;

        /// <summary> 玩家-查询指定玩家的简单信息 </summary>
        public const int PORR_PLAYER_BRIEFUSER_INFO = 821;

        /// <summary> 添加竞赛场成员 </summary>
        public const int PORT_ARENA_MEMBER_ADD = 2522;

        /// <summary> 增加互斥名单 </summary>
        public const int PORT_ARENA_MUTEX_ADD = 2530;

        /// <summary> 减少互斥名单 </summary>
        public const int PORT_ARENA_MUTEX_DEL = 2531;

        /// <summary> 更改互斥名单 </summary>
        public const int PORT_ARENA_MUTEX_UPDATE = 2532;

        /// <summary> 获取互斥名单 </summary>
        public const int PORT_ARENA_MUTEX_GET = 2533;

        /// <summary> 获取竞赛场成员列表 </summary>
        public const int PORT_ARENA_MEMBER_CREATE_LIST = 2525;
        /// <summary>
        /// 竞赛场成员终止比赛按钮
        /// </summary>
        public const int PORT_ARENA_STOP_GAME = 2526;

        /// <summary> 获取竞赛场成员列表 </summary>
        public const int PORT_ARENA_MEMBER_LIST_LEAD = 2527;

        /// <summary> 获取竞赛场成员列表 </summary>
        public const int PORT_ARENA_MEMBER_LIST = 2521;

        /// <summary> 获取竞赛场指定成员的信息 </summary>
        public const int PORT_ARENA_DEST_MEMBER = 2542;

        /// <summary> 修改奖励兑换设置 </summary>
        public const int PORT_ARENA_SETTING_EXCHANGE = 2552;

        /// <summary> 获取指定成员竞技场金豆明细 </summary>
        public const int PORT_GET_MEMBER_ARENAGOLD_RECORD = 2538;

        /// <summary> 获取自己的竞技场金豆明细 </summary>
        public const int PORT_GET_LOCAL_ARENAGOLD_RECORD = 2539;

        /// <summary> 获取指定成员奖章明细 </summary>
        public const int PORT_GET_MEMBER_MEDAL_RECORD = 2540;



        /// <summary> 获取自己的奖章明细 </summary>
        public const int PORT_GET_LOCAL_MEDAL_RECORD = 2541;

        /// <summary> 获取指定成员物品兑换明细 </summary>
        public const int PORT_ARENA_AWARD_EXCHANGE_RECORD = 2550;

        /// <summary> 获取赛场报表 </summary>
        public const int PORT_ARENA_GOLD_STATS = 2511;

        /// <summary> 申请报名 </summary>
        public const int PORT_ARENA_APPLY = 2509;
        /// <summary>
        /// 任务申请列表
        /// </summary>
        public const int PORT_ARENA_GET_EVENTLIST = 2504;
        /// <summary>
        /// 获取竞赛场辅分事件记录
        /// </summary>
        public const int PORT_ARENA_GET_AUXILIARY_SCORE_RECORD = 2583;

        /// <summary>
        /// 获取个人日志
        /// </summary>
        public const int PORT_ARENA_MEMBERLOG_GET = 2592;

        /// <summary> 处理竞赛场事件 </summary>
        public const int PORT_ARENA_HANDLE_EVENT = 2505;
        /// <summary> 申请减少推广任务 </summary>
        public const int PORT_ARENA_DECR_TASK = 2506;
        /// <summary> 推广任务增加 </summary>
        public const int PORT_ARENA_INCR_TASK = 2508;
        /// <summary> 设置竞赛场锁定规则 </summary>
        public const int PORT_ARENA_SETTING_RULE_LOCK = 2553;

        /// <summary> 设置竞赛场自由桌和显示规则 </summary>
        public const int PORT_ARENA_RULE_WF_SHOW = 2555;

        /// <summary>
        /// 获取竞赛场信息
        /// </summary>
        public const int PORT_ARENA_INFO = 2503;

        /// <summary>
        /// 获取推广员辅分信息
        /// </summary>
        public const int PORT_ARENA_AUXILIARYSCORE_INFO = 2581;
        /** 获取竞赛场房卡房间列表 */
        public const int PORT_ARENA_ROOM_FKC_LIST = 2562;

        //竞赛场拒绝邀请
        public const int PORT_ARENA_REFUSE_INVITE = 2568;
        /** 创建竞赛场房卡房间 */
        public const int PORT_ARENA_ROOM_FKC_CREATE = 2561;

        /** 获取推广任务明细列表 */
        public const int PORT_ARENA_TASKE_BILL_LIST_GET = 2543;

        /** 获取指定竞赛场排行榜 */
        public const int PORT_ARENA_RANK_LIST_GET = 2591;

        /// <summary>
        /// 设置成员黑名单
        /// </summary>
        public const int PORT_ARENA_MEMBER_PERMIT = 2524;

        /// <summary>
        /// 移除竞赛场成员
        /// </summary>
        public const int PORT_ARENA_MEMBER_REMOVE = 2523;

        /// <summary> 玩家申请兑换奖章 </summary>
        public const int PORT_ARENA_GET_MEDAL = 2507;

        /// <summary> 获取竞赛场物品兑换条目 </summary>
        public const int PORT_ARENA_AWARD_EXCHANGE_ITEM = 2514;

        /// <summary> 玩家申请奖章兑换物品 </summary>
        public const int PORT_ARENA_MEMBER_AWARD_EXCHANGE = 2513;

        /// <summary>
        /// 获取指定推广员的返利利率
        /// </summary>
        public const int PORT_ARENA_RADIO_BY_AGENT = 2534;
        /// <summary>
        /// 取消设置指定成员的独立利率
        /// </summary>
        public const int PORT_ARENA_RULE_RATE_CANCEL_SPECIAL = 2535;
        /// <summary>
        /// 设置全局返利利率
        /// </summary>
        public const int PORT_ARENA_SETTING_AGENT_RATE = 2536;
        /// <summary>
        /// 设置指定推广员的独立利率
        /// </summary>
        public const int PORT_ARENA_SETTING_AGENT_ALONE_RATE = 2537;


        /** 设置成员成为推广员 */
        public const int PORT_ARENA_CREATE_AGENGT = 2529;

        /// <summary>
        /// 设置推广员辅分
        /// </summary>
        public const int PORT_ARENA_AUXILIARY_SCORE_SETTING = 2582;

        /** 获取推广员结算列表 */
        public const int PORT_ARENA_AGENT_ACCOUNT_LIST = 2547;
        /** 获取推广员详细列表 */
        public const int PORT_ARENA_AGENT_DETAIL_LIST = 2546;

        /// <summary> 获取竞赛场开局统计信息 </summary>
        public const int PORT_ARENA_ROOM_STATS = 2512;

        /// <summary> 解散竞赛场 </summary>
        public const int PORT_DISBAND_ARENA = 2515;


        /// <summary> 获取我的战绩 </summary>
        public const int PORT_ARENA_GET_RECORD = 2545;

        /// <summary> 获取赛场战绩 </summary>
        public const int PORT_ARENA_GET_COMPETITION_RECORD = 2571;
        /// <summary>
        /// 获取门票列表
        /// </summary>
        public const int PORT_ARENA_TICKET_BILL_LIST = 2544;
        /// <summary>
        /// 获取成员变动详细
        /// </summary>
        public const int PORT_ARENA_MEMBER_CHANGE_DETAIL = 2548;

        /// <summary>
        /// 获取竞赛场房卡房间详细信息
        /// </summary>
        public const int PORT_ARENA_ROOM_FKC_DETAIL = 2563;

        /// <summary>
        /// 解散竞赛场房卡房间
        /// </summary>
        public const int PORT_ARENA_ROOM_DISBAND = 2564;

        /// <summary>
        /// 获取竞赛场房卡房间详细信息(解散时)
        /// </summary>
        public const int PORT_ARENA_ROOM_PLAYER_DETAIL = 2565;

        /// <summary>
        /// 竞赛场房间再来一局
        /// </summary>
        public const int PORT_ARENA_AGAIN_ROOM = 2570;

        /// <summary> 获取指定成员的奖章数量 </summary>
        public const int PORT_ARENA_MEMBER_MEDAL = 2549;

        /// <summary> 获取竞赛场可邀请玩家列表和房间列表 </summary>
        public const int PORT_ARENA_ROOM_INVITE_PLAYERS_ROOM_LIST = 2566;
        
        /// <summary> 竞赛场换桌 </summary>
        public const int PORT_ARENA_ROOM_CHANGE_ROOM = 2569;

        /// <summary>
        /// 竞赛场日志
        /// </summary>
        public const int PORT_ARENA_LOG_LIST = 2599;

        //----------------------注册代理-----------------------------
        /// <summary> 代理注册 </summary>
        public const int PORT_PROMOTER_REGISTER = 2601;
        /// <summary> 获取短信验证码 </summary>
        public const int PORT_PROMOTER_REGISTER_CORD_GET = 2602;
    }
}