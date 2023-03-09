using UnityEngine;
using UnityEngine.UI;

namespace scene
{
    public class ShowPing:UnrealLuaSpaceObject
    {
        public string IP = "www.baidu.com";
        Ping ping;
        float delayTime;

        public Text pingtext;

        void Start()
        {
            xinit();
        }

        protected override void xinit()
        {
            SendPing();
        }

        protected override void xupdate()
        {
            this.pingtext.text = delayTime.ToString() + "ms";
            if (null != ping && ping.isDone)
            {
                delayTime = ping.time;
                ping.DestroyPing();
                ping = null;
                Invoke("SendPing", 1.0F);//每秒Ping一次
            }
        }

        void SendPing()
        {
            ping = new Ping(IP);
        }
    }
}
