using cambrian.common;

namespace platform.poker
{
    public class DDZCallScoreProcess : PKOperateEvent
    {
        public override void mouseClick()
        {
            int score = int.Parse(gameObject.name);
            CommandManager.addCommand(new DDZSendMatchCommand(PKSendMatch.JIAOFEN, DDZMatch.match.step, score));
        }
    }
}
