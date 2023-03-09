using cambrian.common;

namespace basef.authname
{
    public class AuthNameGetVerifyCodeProcess : MouseClickProcess
    {
        AuthNamePanel panel;
        public override void execute()
        {
            this.panel = this.root.GetComponent<AuthNamePanel>();
            string mobile = panel.mobile.text;
            if(mobile=="")
            {
                Alert.show("请输入手机号！");
                return;
            }
            if (mobile.Length != 11)
            {
                Alert.show("手机号码必须是11位,请重新输入!");
                this.panel.mobile.text = "";
                return;
            }
            if (!StringKit.isIphone(mobile))
            {
                Alert.show("手机号码号码格式不正确,请重新输入!");
                this.panel.mobile.text = "";
                return;
            }
            CommandManager.addCommand(new GetBindVerifyCodeCommand(mobile),callback);
        }

        void callback(object obj)
        {
            if (obj == null) return;
            if ((bool)obj)
            {
                panel.time = 60;//60秒后可重新获取
            }
        }

    }
}
