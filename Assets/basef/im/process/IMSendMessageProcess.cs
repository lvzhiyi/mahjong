using cambrian.common;

namespace basef.im
{
    public class IMSendMessageProcess:MouseClickProcess
    {
        public override void execute()
        {
            string str=this.getRoot<QuickIMPanel>().inputText.text;
            if (str.Length==0)
            {
                Alert.show("请输入文字!");
                return;
            }
            if (str.Length > 20)
            {
                Alert.show("只能输入16个字!");
                return;
            }

            str=MaskWord.replace(str);
            this.getRoot<QuickIMPanel>().inputText.text = "";
            CommandManager.addCommand(new IMSendTextCommand(str));
            this.root.setVisible(false);
        }
    }
}
