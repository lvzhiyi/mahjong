using cambrian.common;

namespace platform.mahjong
{
    public class MJKongOperateBarSendProcess:MouseClickProcess
    {
        public override void execute()
        {
            MJMatch match = MJMatch.match;
            int step = match.step;
            MJOperationCardBean bean = GetComponent<MJKongOperateBar>().bean;
            CommandManager.addCommand(new SendMJMatchCommand(bean.type, step, bean.card));
        }
    }
}
