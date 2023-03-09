using basef.im;
using cambrian.common;

namespace platform.poker
{
    public class PKIMSendMessageProcess : MouseClickProcess
    {
        public override void execute()
        {
            string str = getRoot<PKQuickIMPanel>().inputText.text;
            if (str.Length == 0)
            {
                Alert.show("请输入文字!");
                return;
            }
            if (str.Length > 20)
            {
                Alert.show("只能输入16个字!");
                return;
            }

            str = MaskWord.replace(str);
            getRoot<PKQuickIMPanel>().inputText.text = "";
            CommandManager.addCommand(new IMSendTextCommand(str));
            root.setVisible(false);
        }
    }
}
