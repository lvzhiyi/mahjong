using cambrian.game;

namespace scene.game
{
    /// <summary>
    /// 微信登录按钮
    /// </summary>
    public class LoginProcess : MouseClickProcess
    {
        /// <summary>
        /// 登录类型-微信
        /// </summary>
        public const int LOGING_WEIXIN = 0, LOGIN_XL = 2, LOGIN_IPHONE = 1;

        public int type;
        public override void execute()
        {
            GameLoadingPanel panel = (GameLoadingPanel)this.root;
            if (type == LOGING_WEIXIN)
            {
                GameDataRoot.gameDataRoot.weixinLogin();
            }
            else
            {
                MobileLoginPanel loginPanel = UnrealRoot.root.getDisplayObject<MobileLoginPanel>();
                loginPanel.refresh();
                loginPanel.setCVisible(true);
            }
        }
    }
}