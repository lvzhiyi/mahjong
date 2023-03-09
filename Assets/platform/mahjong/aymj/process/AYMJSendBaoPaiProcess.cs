using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 报牌
    /// </summary>
    public class AYMJSendBaoPaiProcess : MouseClickProcess
    {
        public override void execute()
        {
            int type = SendMJMatchCommand.BAO;
            int step = AYMJMatch.match.step;
            CommandManager.addCommand(new SendMJMatchCommand(type, step,0));//目前报的牌传的是0，后端需要，避免以后打出牌后，报牌
            UnrealRoot.root.getDisplayObject<AYMJRoomPanel>().gameView.setTingView(null);
        }
    }
}
