using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 胡操作
    /// </summary>
    public class AYMJSendHuProcess:MouseClickProcess
    {
        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            int type = SendMJMatchCommand.HU_SELF;
            int step = match.step;

            int card = match.getLastPlayerCard();

            if (AYMJMatch.match.isRound(match.mindex))
            {
                card = match.getSelfPlayerCards<AYMJPlayerCards>().mocard;
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
