using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 选择飘不飘
    /// </summary>
    public class AYMJSelectPiaoProcess:MouseClickProcess
    {
        public bool piao;
        public override void execute()
        {
            AYMJMatch match =AYMJMatch.match;
            CommandManager.addCommand(new SendMJMatchCommand(SendMJMatchCommand.PIAO,match.step,piao));
        }
    }
}
