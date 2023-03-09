using cambrian.common;
using cambrian.game;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.room
{
    /// <summary>
    /// 顶部基础信息
    /// </summary>
    public class TopBaseInfoView : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 电量 
        /// </summary>
        UnrealProgressBar dian;
        /// <summary>
        /// 4G
        /// </summary>
        protected Transform xinhao;
        /// <summary>
        /// wifi信号
        /// </summary>
        Transform wifi;
        /// <summary>
        /// 版本号
        /// </summary>
        private Text versions;
        /// <summary>
        /// 当前时间
        /// </summary>
        private UnrealTextField time;

        protected override void xinit()
        {
            if (this.transform.Find("dianliang") != null)
            {
                this.dian = this.transform.Find("dianliang").GetComponent<UnrealProgressBar>();
                this.dian.init();
                this.xinhao = this.transform.Find("xinhao").GetComponent<Transform>();
                this.wifi = this.transform.Find("wifi").GetComponent<Transform>();
                this.xinhao.gameObject.SetActive(false);
                this.wifi.gameObject.SetActive(false);
            }
            this.versions = this.transform.Find("version").GetComponent<Text>();

            this.time = this.transform.Find("time").GetComponent<UnrealTextField>();
        }

        protected override void xrefresh()
        {
            this.versions.text = UnrealRoot.root.versions;
            this.versions.gameObject.SetActive(false);
        }


        /// <summary>
        /// 显示电量
        /// </summary>
        /// <param name="str"></param>
        public void showDianliang(string str)
        {
            int dian = StringKit.parseInt(str);
            this.dian.setValue((float)dian / 100);
        }

        /// <summary>
        /// 设置信号强度  1-4
        /// </summary>
        /// <param name="num"></param>
        public void showXinhao(string str)
        {
            int[] xinhaos = StringKit.parseInts(str);
            if (xinhaos[1] == -1)
                return;
            Transform xh = null;
            if (xinhaos[0] == 1)//wifi
            {
                this.wifi.gameObject.SetActive(true);
                this.xinhao.gameObject.SetActive(false);
                xh = this.wifi;
            }
            else
            {
                this.wifi.gameObject.SetActive(false);
                this.xinhao.gameObject.SetActive(true);
                xh = this.xinhao;
            }

            int num = xinhaos[1] + 1;
            if (num > 4)
                num = 4;
            for (int i = 4; i > 0; i--)
            {
                xh.Find("num" + i).gameObject.SetActive(false);
                if (i == num)
                {
                    xh.Find("num" + i).gameObject.SetActive(true);
                    break;
                }
            }
        }


        private long lasttime = 0;
        protected override void xupdate()
        {
            if (time != null)
                this.time.text = TimeKit.format(TimeKit.currentTimeMillis, "HH:mm");
            if (this.xinhao == null) return;
            if (TimeKit.currentTimeMillis - this.lasttime >= 5 * 1000)
            {
                this.lasttime = TimeKit.currentTimeMillis;

                WXManager.getInstance().getCurBattery("Root", "showDianliang");
                WXManager.getInstance().getNetWorkStrength("Root", "showXinhao");
            }
        }
    }
}
