using cambrian.common;
using cambrian.game;
using UnityEngine;

namespace basef.authname
{
    public class SetPasswordProcess : MouseClickProcess
    {
        SetPasswordPanel panel;
        public override void execute()
        {
            this.panel = this.root.GetComponent<SetPasswordPanel>();
            string password = this.panel.password.text;
            string check = this.panel.check.text;
            if (password == null || password == "")
            {
                Alert.show("请输入密码！");
                this.panel.password.text = "";
                return;
            }

            if (password == null || password == "")
            {
                Alert.show("请输入密码！");
                return;
            }

            if (password.Length < 5)
            {
                Alert.show("请输入5位以上的密码！");
                this.panel.password.text = "";
                return;
            }
            if (check == null || check == "" || !password.Equals(check)) 
            {
                Alert.show("两次输入的密码不一致，请重新输入！");
                this.panel.check.text = "";
                return;
            }
            password = MD5.encode(password);
            check = MD5.encode(check);
            CommandManager.addCommand(new SetPasswordCommand(password, check), (obj) =>
            {
                if (obj == null) return;
                User.user.status = (int)obj;
                Alert.show("设置成功！");
                //UnrealRoot.root.getDisplayObject<AuthNamePanel>().setCVisible(false);
                this.root.setCVisible(false);
            });
        }
    }
}
