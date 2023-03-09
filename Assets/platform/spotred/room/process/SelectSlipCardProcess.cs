using cambrian.common;
using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 选者用哪张牌滑
    /// </summary>
    public class SelectSlipCardProcess : MouseClickProcess
    {
        public override void execute()
        {
            int[][] cards = this.getRoot<SelectSlipCardPanel>().selectCards.toArray();

            if (cards.Length == 0)
            {
                Alert.show("请选择需要偷的牌");
                return;
            }
            
            UnrealRoot.root.getDisplayObject<SpotRoomPanel>().operateView.hideAllBtn();

            CommandManager.addCommand(new SendMatchCommand(SendMatchCommand.SLIP, CPMatch.match.step, 0, 0, cards));

            this.root.setVisible(false);

        }
    }
}
