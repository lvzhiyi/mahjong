using cambrian.common;
using cambrian.game;
using basef.player;

namespace basef.authname
{
    public class AuthNameProcess : MouseClickProcess
    {
        AuthNamePanel panel;
        public override void execute()
        {
            this.panel = this.getRoot<AuthNamePanel>();
            string nickname = this.panel.nickName.text;
            string mobile = this.panel.mobile.text;
            string verifycode = this.panel.verifycode.text;

            if (!StringKit.isName(nickname))
            {
                Alert.show("姓名为汉字!");
                this.panel.nickName.text = "";
                return;
            }

            if (!StringKit.isIphone(mobile))
            {
                Alert.show("手机号码号码格式不正确,请重新输入!");
                this.panel.mobile.text = "";
                return;
            }

            if (verifycode.Length != 6)
            {
                Alert.show("请输入正确的验证码!");
                this.panel.verifycode.text = "";
                return;
            }

            CommandManager.addCommand(new AuthNameCommand(nickname, mobile, verifycode), callback);
        }

        public void callback(object obj)
        {
            if (obj == null) return;
            ByteBuffer data = (ByteBuffer)obj;
            bool b = data.readBoolean();
            if (b)
            {
                Alert.autoShow("认证成功");
                User.user.mobilenumber = data.readUTF();
                User.user.status = data.readInt();
                int status = data.readInt();
                if (status != -1)
                    Player.player.status = status;
                if (StatusKit.hasStatus(User.user.status, User.STATUS_LINK_MOBILE))
                {
                    UnrealRoot.root.getDisplayObject<SetPasswordPanel>().refresh();
                    UnrealRoot.root.getDisplayObject<SetPasswordPanel>().setCVisible(true);
                }
                else
                    this.root.setVisible(false);
            }
        }
    }
}
