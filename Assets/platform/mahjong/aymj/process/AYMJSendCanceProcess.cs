using cambrian.common;

namespace platform.mahjong
{
    public class AYMJSendCanceProcess : MouseClickProcess
    {
        public override void execute()
        {
            int type = SendMJMatchCommand.CANCEL;
            int step = AYMJMatch.match.step;
            CommandManager.addCommand(new SendMJMatchCommand(type, step));
            UnrealRoot.root.getDisplayObject<AYMJRoomPanel>().gameView.setTingView(null);
        }
    }
}
