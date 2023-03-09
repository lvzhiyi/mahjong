using basef.joinroom;
using cambrian.common;
using cambrian.game;
using platform.spotred;
using basef.player;
using basef.rule;
using System.Text.RegularExpressions;
using platform;

namespace basef.share
{
    public class ShareManager
    {
        private static ShareManager manager;

        public const string MASTER = "master";
        /// <summary>
        /// 微信1,朋友圈2，闲聊,吹牛
        /// </summary>
        public const int WEIXIN = 1, FRIEND = 2, XIAN_LIAO = 3, CHUI_NIU = 4, DING_DING = 5;

        public static ShareManager getInstance()
        {
            if (manager == null)
            {
                manager = new ShareManager();
            }
            return manager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public void shareRoom(Room room, int type)
        {
            string title = "房间号:" + room.getRoomIndex();
            string info = "规则: " + room.roomRule.getPlayRule(true);
            if (type == WEIXIN)
                WXManager.getInstance()
                    .shareLineToWeiXin(
                        ServerInfos.server.getShareUrl() + "/?id=roomid" + room.getRoomIndex() + MASTER + Player.player.userid +
                        "&time=" + TimeKit.currentTimeMillis, title, info, 1);
        }

        public void sharePromoterRoom(int roomid,string info_1)
        {
            string title = "房间号:" + roomid;
            string info = "规则: " + info_1;
            WXManager.getInstance().shareLineToWeiXin(ServerInfos.server.getShareUrl() + "/?id=roomid" + roomid + MASTER + Player.player.userid + "&time=" + TimeKit.currentTimeMillis, title, info, 1);
        }

        /// <summary>
        /// 分享到钉钉
        /// </summary>
        /// <param name="room"></param>
        public void shareRoomToDD()
        {
            string title = "房间号:" + Room.room.getRoomIndex();
            string info = "规则: " + Room.room.roomRule.getPlayRule(true);
            WXManager.getInstance()
                .shareLineToDD(
                    ServerInfos.server.getShareUrl() + "/?id=roomid" + Room.room.getRoomIndex() + MASTER + Player.player.userid +
                    "&time=" + TimeKit.currentTimeMillis, title, info);
        }

        /// <summary>
        /// 分享二人对战房间
        /// </summary>
        /// <param name="room"></param>
        /// <param name="type"></param>
        public void shareDuelRoom(Room room, int type)
        {
            string title = "房间号:" + room.getRoomIndex();
            string info = "规则: " + room.roomRule.getPlayRule(true);
            if (type == WEIXIN)
                WXManager.getInstance()
                    .shareLineToWeiXin(
                        ServerInfos.server.getShareUrl() + "/?id=duelgame" + room.getRoomIndex() + MASTER + Player.player.userid +
                        "&time=" + TimeKit.currentTimeMillis, title, info, 1);
        }

        public void sharePlayback(long id, RoomRule rule, int type)
        {
            string title = "历史战绩";
            string info = "ID:" + id + "规则: " + rule.getPlayRule(false);
            if (type == WEIXIN)
                WXManager.getInstance()
                    .shareLineToWeiXin(
                        ServerInfos.server.getShareUrl() + "/?id=playback" + id + MASTER + Player.player.userid + "&time=" +
                        TimeKit.currentTimeMillis, title, info, 1);
            else if (type == DING_DING)
            {
                WXManager.getInstance().shareLineToDD(
                    ServerInfos.server.getShareUrl() + "/?id=playback" + id + MASTER + Player.player.userid + "&time=" +
                    TimeKit.currentTimeMillis, title, info);
            }
        }

        public void shareGame(int type,string title)
        {
            string info = "欢迎来到大熊猫麻将";
            WXManager.getInstance().shareLineToWeiXin(ServerInfos.server.getShareUrl() + "/?id=game" + MASTER + Player.player.userid+"&time="+TimeKit.currentTimeMillis, title, info, type);
        }

        public void shareGames(int type,string title,string info)
        {
            WXManager.getInstance().shareLineToWeiXin(ServerInfos.server.getShareUrl() + "/?id=game" + MASTER + Player.player.userid + "&time=" + TimeKit.currentTimeMillis, title, info, type);
        }

        public void shareGame(int type,string code,string title,string info,int sharetype)
        {
            if (sharetype == WEIXIN)
                WXManager.getInstance().shareLineToWeiXin(ServerInfos.server.getShareUrl() + "/inviation.html?data=" + code + "&id=game" + MASTER + Player.player.userid + "&time=" + TimeKit.currentTimeMillis, title, info, type);
        }

        public void sharePic(byte[] obj)
        {
            WXManager.getInstance().shareIconToWeiXin(obj, WEIXIN);
        }

        public void sharePicToFriend(byte[] obj)
        {
            WXManager.getInstance().shareIconToWeiXin(obj, FRIEND);
        }


        public void sharePicToApplication(byte[] obj, int type)
        {
            if (type == WEIXIN)
                WXManager.getInstance().shareIconToWeiXin(obj, WEIXIN);
            else if (type == DING_DING)
                WXManager.getInstance().shareIconToDD(obj);
        }


        public void recvShareInfo(string str)
        {
            if (str == "" || str == null)
                return;

            //如果当前玩家已经在房间中 则不显示分享
            if (Room.room!=null) return;
            UnrealRoot.root.getDisplayObject<SpotRoomRulePanel>().setVisible(false);
            UnrealRoot.root.getDisplayObject<SpotJoinRoomPanel>().setVisible(false);
            // string[] strs = str.Split('&');
            string[] strs = Regex.Split(str, "master", RegexOptions.IgnoreCase);

            //执行对应的情况
            if (strs[0].StartsWith("roomid"))
            {
                int id = StringKit.parseInt(strs[0].Substring("roomid".Length));
                RecvShareRoomProcess process = new RecvShareRoomProcess();
                process.setRoomid(id);
                process.execute();
            }
            else if (strs[0].StartsWith("playback"))
            {
                string id = strs[0].Substring("playback".Length);
                RecvSharePlaybackProcess process = new RecvSharePlaybackProcess();
                process.setPlaybackId(id);
                process.execute();
            }
            else if (strs[0].StartsWith("game"))
            {
                //单纯分享游戏 不需要处理
            }

        }
    }
}
