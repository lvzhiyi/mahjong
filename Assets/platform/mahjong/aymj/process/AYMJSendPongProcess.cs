using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 发送碰消息
    /// </summary>
    public class AYMJSendPongProcess : MouseClickProcess
    {
        public override void execute()
        {
            int type = SendMJMatchCommand.PONG;
            int step = AYMJMatch.match.step;
            CommandManager.addCommand(new SendMJMatchCommand(type, step, AYMJMatch.match.getLastPlayerCard()));
        }
    }
}
