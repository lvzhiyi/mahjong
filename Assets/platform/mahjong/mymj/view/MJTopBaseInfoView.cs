using cambrian.common;
using cambrian.game;
using cambrian.uui;
using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJTopBaseInfoView: UnrealLuaSpaceObject
    {
        /// <summary>
        /// 电量 
        /// </summary>
        UnrealProgressBar dian;
        /// <summary>
        /// 4G
        /// </summary>
        protected SpritesList xinhao;
        /// <summary>
        /// wifi信号
        /// </summary>
        SpritesList wifi;
        /// <summary>
        /// 版本号
        /// </summary>
        private Text versions;
        /// <summary>
        /// 当前时间
        /// </summary>
        private UnrealTextField time;
        /// <summary>
        /// 表情
        /// </summary>
        private Image imbtn;
        /// <summary>
        /// 录音按钮
        /// </summary>
        //private UnrealTextButton recordingBtn;

        protected override void xinit()
        {
            if (transform.Find("dianliang") != null)
            {
                dian = this.transform.Find("dianliang").GetComponent<UnrealProgressBar>();
                dian.init();
                xinhao = this.transform.Find("xinhao").GetComponent<SpritesList>();
                wifi = this.transform.Find("wifi").GetComponent<SpritesList>();
                xinhao.gameObject.SetActive(false);
                wifi.gameObject.SetActive(false);
            }

            versions = transform.Find("version").GetComponent<Text>();

            time = transform.Find("time").GetComponent<UnrealTextField>();
            time.init();

            if (transform.Find("imbtn") != null)
            {
                imbtn = transform.Find("imbtn").GetComponent<Image>();
            }
        }

        protected override void xrefresh()
        {
            this.versions.text = UnrealRoot.root.versions;

            if (Room.room != null)
            {
                bool b=Room.room.roomRule.rule.getRuleAtribute("disablechattool");
                if (imbtn == null) return;
                imbtn.gameObject.SetActive(!b);
            }
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
            SpritesList xh = null;
            if (xinhaos[0] == 1)//wifi
            {
                wifi.gameObject.SetActive(true);
                xinhao.gameObject.SetActive(false);
                xh = wifi;
            }
            else
            {
                wifi.gameObject.SetActive(false);
                xinhao.gameObject.SetActive(true);
                xh = xinhao;
            }

            int num = xinhaos[1];
            if (num > 3)
                num = 3;
            xh.ShowIndex(num);
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

                WXManager.getInstance().getCurBattery("Root", "showMJDianliang");
                WXManager.getInstance().getNetWorkStrength("Root", "showMJXinhao");
            }
        }
    }
}
