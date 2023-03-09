using cambrian.common;

namespace platform.mahjong
{
    public class AYMJKongOperateBarSendProcess:MouseClickProcess
    {
        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            int step = match.step;
            MJOperationCardBean bean = GetComponent<MJKongOperateBar>().bean;
            CommandManager.addCommand(new SendMJMatchCommand(bean.type, step, bean.card));
        }
    }
}
