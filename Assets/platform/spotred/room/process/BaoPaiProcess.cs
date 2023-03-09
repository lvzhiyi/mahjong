using cambrian.common;
using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 报牌
    /// </summary>
    public class BaoPaiProcess:MouseClickProcess
    {
        public override void execute()
        {
            this.getRoot<SpotRoomPanel>().operateView.hideAllBtn();
            CommandManager.addCommand(new SendMatchCommand(SendMatchCommand.BAOPAI, CPMatch.match.step,0,0,null));
        }
    }
}
