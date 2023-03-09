using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 发送碰消息
    /// </summary>
    public class MJSendPongProcess : MouseClickProcess
    {
        public override void execute()
        {
            int type = SendMJMatchCommand.PONG;
            int step = MJMatch.match.step;
            CommandManager.addCommand(new SendMJMatchCommand(type, step, MJMatch.match.getLastPlayerCard()));
        }
    }
}
