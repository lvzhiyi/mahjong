using cambrian.common;
using cambrian.game;
using platform.spotred;

namespace platform.spotred.room
{
    public class SlipCardProcess:MouseClickProcess
    {
        public override void execute()
        {
            SoundManager.playButton();

            CPMatch match = CPMatch.match;

            int cardsNum = match.paidui;

            int[][] list = match.getSelfPlayerCards<CPPlayerCards>().getSlipCards();

            
            int step = match.step;
            

            if (match.isXiaoJia() || match.getRoomRule().rule.getRuleAtribute("isshowtoupai"))
            {
                if ((this.getRoot<SpotRoomPanel>().operate & OperateView.CAN_SLIP) == OperateView.CAN_SLIP)
                {
                    if (list.Length == 1 && list[0][1] == 3)
                    {
                        CommandManager.addCommand(new SendMatchCommand(SendMatchCommand.SLIP, step, list[0][0], 3,new int[1][] {new int[] {list[0][0],list[0][1]}}));
                        this.getRoot<SpotRoomPanel>().operateView.hideAllBtn();
                    }
                    else if (list.Length == 1 && list[0][1] == 4 && !match.isstage(CPMatch.STAGE_SLIP) && match.getRoomRule().rule.sid != 1016)
                    {
                        CommandManager.addCommand(new SendMatchCommand(SendMatchCommand.SLIP, step, list[0][0], 3, new int[1][] { new int[] { list[0][0], 3 } }));
                        this.getRoot<SpotRoomPanel>().operateView.hideAllBtn();
                    }
                    else
                    {
                        if(match.getRoomRule().rule.sid==1010)
                        {
                            CommandManager.addCommand(new SendMatchCommand(SendMatchCommand.SLIP, step, list[0][0], 3, new int[1][] { new int[] { list[0][0], 3 } }));
                            this.getRoot<SpotRoomPanel>().operateView.hideAllBtn();
                        }
                        else
                        {
                            SelectSlipCardPanel slipPanel = UnrealRoot.root.getDisplayObject<SelectSlipCardPanel>();
                            slipPanel.setData(list, cardsNum);
                            slipPanel.refresh();
                            slipPanel.setVisible(true);
                        }                       
                    }
                }
                else
                {
                    if (list.Length == 1 && list[0][1] == 3)
                    {
                        CommandManager.addCommand(new SendMatchCommand(SendMatchCommand.SLIP, step, list[0][0], 3, new int[1][] { new int[] { list[0][0], list[0][1] } }));
                        this.getRoot<SpotRoomPanel>().operateView.hideAllBtn();
                    }
                    else if (list.Length == 1 && list[0][1] == 4 && !match.isstage(CPMatch.STAGE_SLIP))
                    {
                        CommandManager.addCommand(new SendMatchCommand(SendMatchCommand.SLIP, step, list[0][0], 3,
                            new int[1][] {new int[] {list[0][0], 3}}));
                        this.getRoot<SpotRoomPanel>().operateView.hideAllBtn();
                    }
                    else
                    {
                        SelectSlipCardPanel slipPanel = UnrealRoot.root.getDisplayObject<SelectSlipCardPanel>();
                        slipPanel.setData(list, cardsNum);
                        slipPanel.refresh();
                        slipPanel.setVisible(true);
                    }
                }
            }
        }
    }
}
