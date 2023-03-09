using cambrian.common;

namespace basef.bin
{
    public class BinAccountSubmitProcess : MouseClickProcess
    {
        public override void execute()
        {
            string str = this.getRoot<BinAccountPanel>().text.text;
            if (StringKit.isNullString(str))
                return;
            if (str.Length < 5 || str.Length > 12)
            {
                this.getRoot<BinAccountPanel>().text.text = "";
                Alert.show("请重新输入!");
                return;
            }

            long b = StringKit.parseLong(str);
            CommandManager.addCommand(new BinAccountCommand(b),callBack);
        }

        public void callBack(object obj)
        {
            if (obj == null) return;
            bool b = (bool)obj;
            if (b)//成功!
            {
                Alert.show("绑定成功!");
                this.root.setVisible(false);
            }
            else
            {
                this.getRoot<BinAccountPanel>().text.text = "";
                Alert.show("绑定失败!");
            }
        }
    }
}
