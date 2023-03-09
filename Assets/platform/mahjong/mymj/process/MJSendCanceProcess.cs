using cambrian.common;

namespace platform.mahjong
{
    public class MJSendCanceProcess : MouseClickProcess
    {
        public override void execute()
        {
            int type = SendMJMatchCommand.CANCEL;
            int step = MJMatch.match.step;
            CommandManager.addCommand(new SendMJMatchCommand(type, step));
            UnrealRoot.root.getDisplayObject<MahjongRoomPanel>().gameView.setTingView(null);
        }
    }
}
