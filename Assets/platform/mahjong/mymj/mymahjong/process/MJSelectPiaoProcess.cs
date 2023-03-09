using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 选择飘不飘
    /// </summary>
    public class MJSelectPiaoProcess:MouseClickProcess
    {

        public bool piao;

        public override void execute()
        {
            MJMatch match=MJMatch.match;
            CommandManager.addCommand(new SendMJMatchCommand(SendMJMatchCommand.PIAO,match.step,piao));
        }
    }
}
