using System.Collections;
using basef.getgoldeffect;
using cambrian.common;
using cambrian.game;
using basef.player;
using UnityEngine;
using UnityEngine.Networking;

namespace basef.share
{
    public class ShareToFriendProcess : MouseClickProcess
    {
        [HideInInspector] public static string[] titles = new[] { "大熊猫麻将", "大熊猫麻将", "好友邀请" };

        [HideInInspector] public static string[] content = new[] { "[大熊猫麻将]2021全新版本火热上线，给您更方便快捷的游戏体验，快来一起玩吧！", "[大熊猫麻将]新版火热上线！快拿起手机立即下载游戏，一起来休闲娱乐一下！", "嗨！我在玩[大熊猫麻将]，快来和我一起玩吧！" };

        /// <summary>
        /// 分享类型
        /// </summary>
        public int shareType;

        public override void execute()
        {
            int random = MathKit.random(0,3);
            AccessPlatformMessage.addEvent(updateShareFriend);
            if (shareType == 0)
                ShareManager.getInstance().shareGames(ShareManager.FRIEND,titles[random],content[random]);
            else
                ShareManager.getInstance().shareGames(ShareManager.WEIXIN,titles[random],content[random]);
        }

        public void updateShareFriend(string status)
        {
            AccessPlatformMessage.removeEvent(updateShareFriend);
            int type = StringKit.parseInt(status);
            if (type != 0)
            {
                Alert.show("分享失败!请重新分享");
                return;
            }

           // string url = ServerInfos.server.getSharegoldUrl() + "&userid=" + Player.player.userid;
           // share(url);
        }

        public void share(string url)
        {
            StartCoroutine(sendHttp(url));
        }

        IEnumerator sendHttp(string url)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();

                if (www.isDone && !www.isNetworkError)
                {
                    string results = www.downloadHandler.text;
                    if (StringKit.isInt(results))
                    {
                        long money = StringKit.parseLong(results);
                        if (money != 0)
                        {
                            ShowGoldEffectPanel panel = UnrealRoot.root.getDisplayObject<ShowGoldEffectPanel>();
                            panel.setNum(money);
                            panel.refresh();
                            panel.setCVisible(true);
                        }
                    }
                }
                else
                {

                }
            }
        }
    }
}
