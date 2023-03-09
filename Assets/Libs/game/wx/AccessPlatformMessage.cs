using basef.player;
using cambrian.common;
using cambrian.uui;
using platform.mahjong;
using platform.poker;
using platform.spotred.room;
using platform.spotred.waitroom;
using scene.game;
using System.Text.RegularExpressions;
using UnityEngine;
using XLua;

namespace cambrian.game
{
    /// <summary>
    /// 接收android或者ios发送过来的消息
    /// </summary>
    [Hotfix]
    public class AccessPlatformMessage:MonoBehaviour
    {
        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="info"></param>
        public void WeiXinLogin(string info)
        {
            GameDataRoot.gameDataRoot.onWeixinLogin(info);
        }

        /// <summary>
        /// 显示电量
        /// </summary>
        /// <param name="info"></param>
        public void showDianliang(string info)
        {
            UnrealRoot.root.getDisplayObject<SpotRoomPanel>().showDianliang(info);
        }

        /// <summary>
        /// 显示信号
        /// </summary>
        /// <param name="info"></param>
        public void showXinhao(string info)
        {
            UnrealRoot.root.getDisplayObject<SpotRoomPanel>().showXinhao(info);
        }

        /// <summary>
        /// 显示电量 等待房间
        /// </summary>
        /// <param name="info"></param>
        public void showDianliangR(string info)
        {
            UnrealRoot.root.getDisplayObject<SpotWaitRoomPanel>().showDianliang(info);
        }

        /// <summary>
        /// 显示麻将的信号
        /// </summary>
        /// <param name="info"></param>
        public void showMJXinhao(string info)
        {
           MahJongPanel.getPanel().topView.showXinhao(info);
        }


        /// <summary>
        /// 显示电量 等待房间
        /// </summary>
        /// <param name="info"></param>
        public void showMJDianliang(string info)
        {
            MahJongPanel.getPanel().topView.showDianliang(info);
        }

        /// <summary> 显示扑克的信号 </summary>
        public void showPKXinhao(string info)
        {
            PKRoomPanel.Panel.topView.showXinhao(info);
        }

        /// <summary> 显示电量 等待房间 </summary> 扑克
        public void showPKDianliang(string info)
        {
            PKRoomPanel.Panel.topView.showDianliang(info);
        }

        /// <summary>
        /// 显示信号 等待房间
        /// </summary>
        /// <param name="info"></param>
        public void showXinhaoR(string info)
        {
            UnrealRoot.root.getDisplayObject<SpotWaitRoomPanel>().showXinhao(info);
        }

        public void call_back_room_record_error(string info)
        {
            UnrealRoot.root.getDisplayObject<SpotWaitRoomPanel>().call_back_room_record_error(info);
        }

        public void call_back_room_record(string info)
        {
            UnrealRoot.root.getDisplayObject<SpotWaitRoomPanel>().call_back_room_record(info);
        }

        public void call_back_record_error(string info)
        {
            UnrealRoot.root.getDisplayObject<SpotRoomPanel>().call_back_record_error(info);
        }

        public void call_back_record(string info)
        {
            UnrealRoot.root.getDisplayObject<SpotRoomPanel>().call_back_record(info);
        }

        /// <summary>
        /// 微信支付回掉
        /// </summary>
        /// <param name="info"></param>
        public void pay_call_back(string info)
        {
            int error=StringKit.parseInt(info);

            if (error == 0)
                Alert.show("支付成功!");
            else
                Alert.show("支付失败");
        }

        public void back(object obj)
        {
            //bool b = (bool)obj;
            PayOrderStorage.deleteOrder();
        }

        public delegate void updateShare(string info);

        private static updateShare updateShareEvent;
       

        public static void addEvent(updateShare action)
        {
            updateShareEvent += action;
        }

        public static void removeEvent(updateShare action)
        {
            updateShareEvent -= action;
        }

        /// <summary>
        /// 分享回掉(android已实现)
        /// </summary>
        /// <param name="info"></param>
        public void share_call_back(string info)
        {
            if (updateShareEvent != null)
                updateShareEvent(info);
        }

        private int lastn = 0;
        private int laste = 0;
        /// <summary>
        /// 位置变化相差20米
        /// </summary>
        private int dis = 20;

        public static int n = 0;

        public static int e = 0;
        /// <summary>
        /// 回调GPS信息
        /// </summary>
        /// <param name="info"></param>
        public void call_back_gps(string info)
        {
            string[] loc = Regex.Split(info, ",", RegexOptions.IgnoreCase);

            if (loc == null || loc.Length == 0) return;
            //经纬度
             n = (int) (double.Parse(loc[0])*1000000);
             e = (int) (double.Parse(loc[1])*1000000);

            if (n != 0 && e != 0)
            {
                double distance = MathKit.GetGPSDistance(e, n, laste, lastn);
                laste = e;
                lastn = n;
                if (distance < dis)
                    return;
            }
            string url = ServerInfos.server.getDsUrl() + "/submitGPS?" + MathKit.random(1,10000);
           
            send_gps_pos(n, e, url);
        }

        //发送gps
        public void send_gps_pos(int n,int e,string url)
        {
            ByteBuffer data = new ByteBuffer();
            data.writeLong(Player.player.userid);
            data.writeInt(n);
            data.writeInt(e);
            data.writeInt(Platform.getPlatFormType());

            Loader.getLoader().post(url, data, obj =>
            {
                byte[] b = (byte[])obj;
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
    }
}
