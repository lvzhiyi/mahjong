using cambrian.common;
using cambrian.uui;
using UnityEngine;
using UnityEngine.UI;

namespace basef.authname
{
    /// <summary> 实名认证面板 </summary>
    public class AuthNamePanel : UnrealLuaPanel
    {
        private string initText = "获取验证码";

        public int time;

        /// <summary> 姓名 </summary>
        [HideInInspector] public UnrealInputTextField nickName;

        /// <summary> 手机号 </summary>
        [HideInInspector] public UnrealInputTextField mobile;

        /// <summary> 验证码 </summary>
        [HideInInspector] public UnrealInputTextField verifycode;

        /// <summary> 获取验证码 </summary>
        [HideInInspector] public UnrealScaleButton getVerifyCode;

        SpritesList codeBg;

        Text text;

        Text text1;

        Text award;

        string awardinfo = "绑定手机立即赠送5张房卡和1888金豆";

        protected override void xinit()
        {
            this.nickName = this.transform.Find("Canvas").Find("content").Find("name").Find("text").GetComponent<UnrealInputTextField>();
            this.mobile = this.transform.Find("Canvas").Find("content").Find("id").Find("text").GetComponent<UnrealInputTextField>();
            this.verifycode = this.transform.Find("Canvas").Find("content").Find("verifycode").Find("text").GetComponent<UnrealInputTextField>();
            this.getVerifyCode = this.transform.Find("Canvas").Find("content").Find("getverifycode").GetComponent<UnrealScaleButton>();
            this.codeBg = this.getVerifyCode.transform.Find("bg").GetComponent<SpritesList>();
            this.text = this.getVerifyCode.transform.Find("text").GetComponent<Text>();
            this.text1 = this.getVerifyCode.transform.Find("text_1").GetComponent<Text>();
            this.award = this.transform.Find("Canvas").Find("content").Find("award").GetComponent<Text>();
            this.time = 0;
        }

        protected override void xrefresh()
        {
            this.nickName.text = "";
            this.mobile.text = "";
            this.verifycode.text = "";
            this.time = 0;
            this.text.text = initText;
            this.text.gameObject.SetActive(true);
            this.text1.gameObject.SetActive(false);
            this.codeBg.ShowIndex(0,false);
            this.award.text = awardinfo;
        }

        long lasttime;
        protected override void xupdate()
        {
            if (time == 0) return;
            if (TimeKit.currentTimeMillis - this.lasttime >= 1000)
            {
                this.lasttime = TimeKit.currentTimeMillis;
                this.text1.gameObject.SetActive(true);
                this.text.gameObject.SetActive(false);
                this.show();
            }
        }

        private void show()
        {
            time--;
            this.getVerifyCode.setState(UnrealButton.DISABLE);
            this.codeBg.ShowIndex(1,false);
            this.text1.text = time + "S后重发";
            if (time == 0)
            {
                this.getVerifyCode.setState(UnrealButton.NORMAL);
                this.codeBg.ShowIndex(0,false);
                this.text.text = initText;
                this.text.gameObject.SetActive(true);
                this.text1.gameObject.SetActive(false);
            }
        }
    }
}
