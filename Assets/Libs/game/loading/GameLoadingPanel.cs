using UnityEngine.UI;

namespace cambrian.game
{
    /// <summary>
    /// 加载界面
    /// </summary>
    public class GameLoadingPanel : UnrealLuaPanel
    {
        public static GameLoadingPanel panel;
        /// <summary>
        /// 进度百分比
        /// </summary>
        public int percent;
        /// <summary>
        /// 微信登陆按钮
        /// </summary>
        private UnrealScaleButton login;

        /// <summary>
        /// 游客登陆
        /// </summary>
        //private UnrealScaleButton guestLogin;

        private UnrealTextField versions;
        /// <summary>
        /// 进度显示
        /// </summary>
        private Text num;
        /// <summary>
        /// 用户协议按钮
        /// </summary>
        //public UnrealCheckBox checkBox;

        /// <summary>
        /// 地区名
        /// </summary>
        //private Text regionname;

        protected override void xinit()
        {
            base.xinit();
            this.num = this.transform.Find("Canvas").Find("num").GetComponent<Text>();

            this.versions = this.transform.Find("Canvas").Find("versions").GetComponent<UnrealTextField>();
            this.versions.init();

            this.login = this.transform.Find("Canvas").Find("wx").GetComponent<UnrealScaleButton>();
            this.login.init();

            //this.guestLogin = this.transform.Find("Canvas").Find("guest").GetComponent<UnrealScaleButton>();
            //this.guestLogin.init();

            //this.guestLogin.setVisible(false);

            //this.login.setVisible(false);

            //this.checkBox = this.transform.Find("Canvas").Find("checkbox").GetComponent<UnrealCheckBox>();

            //this.regionname = this.content.Find("region").Find("text").GetComponent<Text>();
           


            GameLoadingPanel.panel = this;
            this.resizeDelta(new Margin());
        }


        /// <summary>
        /// 设置进度
        /// </summary>
        /// <param name="percent"></param>
        public void setPercent(int percent)
        {
            this.percent = percent;
        }

        public void setPercent(int percent, bool showlogin)
        {
            this.percent = percent;
        }

        public void showWeiXinBtn()
        {
            if (Platform.isOpenGuest())
            {
                this.login.setVisible(false);
            }
            else
            {
                this.login.setVisible(true);
            }
        }

        public void hideLoading()
        {
            
        }


        protected override void xrefresh()
        {
            base.xrefresh();

            num.text = percent + "%";
            versions.text = UnrealRoot.root.versions;
            //checkBox.setState(true);

            //regionname.text = SpotRedRoot.roots.regionModule.region.name;

            if (percent >= 100)
                setVisible(false);
        }
    }
}