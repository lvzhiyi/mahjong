using basef.player;
using cambrian.common;
using cambrian.game;
using scene.game;
using System;
using System.Collections;
using UnityEngine.Networking;

namespace platform.spotred.playback
{
    /// <summary>
    /// 打开回放界面
    /// </summary>
    public class OpenPlaybackPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            SpotRedRoot.dataLoading.refresh();
            string url = ServerInfos.server.getReplayUrl() + "/record?id=" + Player.player.userid + "&type=1";
            StartCoroutine(sendHttp(url, onDataInit));
        }

        IEnumerator  sendHttp(string url,Action<object> obj)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();
                if (www.isDone && !www.isNetworkError)
                {
                    byte[] results = www.downloadHandler.data;
                    ByteBuffer buffer=new ByteBuffer(results);
                    obj(buffer);
                }
                else
                {
                    obj(null);
                }
               // StopCoroutine("sendHttp");
            }
        }

        public void onDataInit(object obj)
        {
            SpotRedRoot.dataLoading.hidden();

            if (obj == null)
            {
                Alert.show("数据异常");
                return;
            }

            ByteBuffer data = (ByteBuffer)obj;
            int len = data.readInt();
            Record[] records = null;
            if (len != 0)
            {
                records = new Record[len];
                for (int i = 0; i < len; i++)
                {
                    records[i] = new Record();
                    records[i].bytesRead(data);
                }
            }
            long time = TimeKit.getTodayStart();
            long[] times = new long[7];// 只显示7天
            times[0] = time;
            for (int i = 1; i < times.Length; i++) 
            {
                times[i] = time - i * TimeKit.DAY_MILLS;
            }

            PlayBackPanel panel = UnrealRoot.root.getDisplayObject<PlayBackPanel>();
            panel.dataInit(records);
            panel.refresh();
            panel.setCVisible(true);
            panel.setLastPanel(null);

        }
    }
}