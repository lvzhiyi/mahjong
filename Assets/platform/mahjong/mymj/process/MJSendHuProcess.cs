using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 胡操作
    /// </summary>
    public class MJSendHuProcess:MouseClickProcess
    {
        public override void execute()
        {
            MJMatch match = MJMatch.match;
            int type = SendMJMatchCommand.HU_SELF;
            int step = match.step;

            int card = match.getLastPlayerCard();

            if (MJMatch.match.isRound(match.mindex))
            {
                card = match.getSelfPlayerCards<MJPlayerCards>().mocard;
            }
            else
            {
                if (match.isKongSups())
                    type = SendMJMatchCommand.HU_ROB;
                else
                    type= SendMJMatchCommand.HU;
            }
            CommandManager.addCommand(new SendMJMatchCommand(type, step, card));
        }
    }
}
