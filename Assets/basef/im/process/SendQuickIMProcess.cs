using cambrian.common;

namespace basef.im
{
    public class SendQuickIMProcess : MouseClickProcess
    {
        public override void execute()
        {
            IMQuickMsgBar bar = this.GetComponent<IMQuickMsgBar>();
            CommandManager.addCommand(new SendQuickMsgCommand(bar.msg.type, bar.msg.value));
            this.root.setVisible(false);
        }
    }
}
