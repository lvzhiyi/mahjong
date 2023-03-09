using cambrian.common;
using cambrian.game;

namespace basef.authname
{
    public class UpdatePasswordVerifyCodeProcess : MouseClickProcess
    {
        UpdatePasswordPanel panel;

        public override void execute()
        {
            this.panel = this.root.GetComponent<UpdatePasswordPanel>();
            if(panel.password.text.Equals("")||panel.check.text.Equals(""))
            {
                Alert.show("请输入密码！");
                return;
            }
            string mobile = User.user.mobilenumber;
            if (mobile==null)
            {
                Alert.show("尚未绑定手机号，请先绑定！");
                return;
            }

            CommandManager.addCommand(new GetVerifyCodeCommand(mobile),callback);
        }

        void callback(object obj)
        {
            if (obj == null) return;
            //if((bool)obj)
            //{
            //    panel.time = 60;//60秒后可重新获取
            //}
        }
    }
}
