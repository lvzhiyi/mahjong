using cambrian.common;
using UnityEngine;

namespace cambrian.game
{
    /// <summary>
    /// 获取定位位置
    /// </summary>
    public class GPSLocation : MonoBehaviour
    {
//        //如果值为-1 表示玩家未开启gps
//        public float n = 0;
//        public float e = 0;

        public GPSObject gps;


        public void doStart()
        {
            lasttime = TimeKit.currentTimeMillis;
            //if (!Input.location.isEnabledByUser)
            //{
            //    if (gps == null)
            //        gps = new GPSObject();
            //    this.gps.isEnabledByUser = false;
            //    this.gameObject.GetComponent<AccessPlatformMessage>().call_back_gps("0,0");
            //}
            //else
            //{
                WXManager.getInstance().getGPSLocation("Root", "call_back_gps");
            //}
        }


        void StopGPS()
        {
            Input.location.Stop();
        }

        /// <summary>
        /// 上次访问时间
        /// </summary>
        private long lasttime;

        private long intervaltime = TimeKit.MIN_MILLS*30;
        
        void Update()
        {
            long time = TimeKit.currentTimeMillis;
            if (time < 0||lasttime==0) return;
            if (time - lasttime > intervaltime)
                doStart();
        }
    }
}