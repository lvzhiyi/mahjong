using cambrian.common;
using cambrian.game;

namespace basef.authname
{
    public class UpdatePasswordProcess : MouseClickProcess
    {
        UpdatePasswordPanel panel;
        public override void execute()
        {
            this.panel = this.root.GetComponent<UpdatePasswordPanel>();
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
            check= MD5.encode(check);

            CommandManager.addCommand(new UpdatePasswordCommand(password,check),callback);
        }

        public void callback(object obj)
        {
            if (obj == null) return;
            User.user.status = (int)obj;
            Alert.show("修改成功！");
            this.root.setVisible(false);
        }
    }
}
