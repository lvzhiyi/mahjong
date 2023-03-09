using cambrian.common;
using scene.game;

namespace cambrian.game
{
    public class PasswordLoginProcess : MouseClickProcess
    {
        MobileLoginPanel panel;
        public override void execute()
        {
            panel = root.GetComponent<MobileLoginPanel>();
            long userid = long.Parse(panel.userid.text);
            string password = panel.password.text;
            if (userid == 0)
            {
                Alert.show("请输入用户ID！");
                panel.password.text = "";
                return;
            }
            if (userid < 100000)
            {
                Alert.show("用户ID不正确,请重新输入!");
                panel.userid.text = "";
                panel.password.text = "";
                return;
            }

            if (password == null || password == "")
            {
                Alert.show("请输入密码！");
                return;
            }

            if (password.Length < 5)
            {
                Alert.show("请输入5位以上的密码!");
                panel.password.text = "";
                return;
            }
            password = MD5.encode(password);
            root.setCVisible(false);
            GameDataRoot.gameDataRoot.PasswordLogin(userid, password);
        }
    }
}
