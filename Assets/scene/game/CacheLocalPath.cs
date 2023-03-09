using UnityEngine;

namespace scene.game
{
    /// <summary>
    /// 保存在本地文件路径
    /// </summary>
    public class CacheLocalPath
    {
        /// <summary>
        /// 本地系统根路径
        /// </summary>
        public static string APPLICATION_PATH = Application.persistentDataPath;
        /// <summary>
        /// 资源包路径
        /// </summary>
        public static string AB_RESROUCE_PATH = APPLICATION_PATH+"/ab/";
        /// <summary>
        /// 本地Version.cfg 路径
        /// </summary>
        public static string VERSION_CFG_PATH = APPLICATION_PATH + "/version.cfg";
       
        /// <summary>
        /// 本地保存-服务器相关信息地址
        /// </summary>
        public static string SERVERINFO_ADDRESS_PATH = APPLICATION_PATH + "/addresses";

        /// <summary>
        /// 本地保存-声音相关信息地址
        /// </summary>
        public static string SOUND_INFO_PATH = APPLICATION_PATH + "/sound";
        /// <summary>
        /// 本地保存-大厅滚动图片信息地址
        /// </summary>
        public static string SNOTICE_SCROLL_PATH = APPLICATION_PATH + "/snotice";

        /// <summary>
        /// 本地保存-游戏中背景缓存地址
        /// </summary>
        public static string ROOM_DESKBG_PATH = APPLICATION_PATH + "/desktop";
        
       
        /// <summary>
        /// 本地保存-茶馆是否显示游戏中房间地址
        /// </summary>
        public static string TEA_SHOW_ROOM_LIST_PATH = APPLICATION_PATH + "/tearoomshow";

        /// <summary>
        /// 本地保存-茶馆是否显示游戏中房间地址
        /// </summary>
        public static string TEA_ROOM_TYPE_PATH = APPLICATION_PATH + "/roomtype";
        /// <summary>
        /// 本地保存-选择的地区
        /// </summary>
        public static string SELECT_REGION_PATH = APPLICATION_PATH + "/regions";
        /// <summary>
        /// 本地保存-进入茶馆的方式(从金币场进，还是房卡场进)
        /// </summary>
        public static string TEA_GOLD_TYPE = APPLICATION_PATH + "/teagoldtype";
        /// <summary>
        /// 本地保存-是否是第一次进入游戏
        /// </summary>
        public static string FIRST_LOGIN_PATH = APPLICATION_PATH + "/first_login";

        //----------麻将----------------
        /// <summary>
        /// 本地保存-游戏中背景缓存地址
        /// </summary>
        public static string MJ_ROOM_DESKBG_PATH = APPLICATION_PATH + "/mjdesktop";

        //-----------------------以下分地区保存-----------------------------------------

        /// <summary>
        /// 本地保存-规则缓存地址
        /// </summary>
        public static string RULE_INFO_PATH ="ruleinfos10";
        /// <summary>
        /// 本地保存-茶馆规则缓存地址
        /// </summary>
        public static string TEA_RULE_INFO_PATH = "tea_rule_infos10";
        /// <summary>
        /// 本地保存-金币场选择规则
        /// </summary>
        public static string GOLD_RULE_PATH = "gold_rule_selects4";
        /// <summary>
        /// 本地保存-金币场档次选择
        /// </summary>
        public static string GOLD_AREA_PATH = "gold_area_selects";
        /// <summary>
        /// 本地充值-订单保存数据路径
        /// </summary>
        public static string PAY_ORDER_STORAGE_PATH = "ordersave";

        //----------------------------------
        public static string LAST_ENTER_AREN_PATH = "/last_enter_aren";

        /// <summary>
        /// 本地保存-茶馆快速开局规则缓存
        /// </summary>
        public static string TEA_FASTGAME_INFO_PATH = "tea_fastgame_infos";

        /// <summary>
        /// 本地保存-竞赛场快速开局规则缓存
        /// </summary>
        public static string ARENA_QUICK_GAME_INFO_PATH = "arena_quick_game_infos";
    }
}
