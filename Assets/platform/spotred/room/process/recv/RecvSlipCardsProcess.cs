using basef.rule;
using cambrian.common;
using cambrian.game;
using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 接收滑牌
    /// </summary>
    public class RecvSlipCardsProcess : Process
    {
        private int operate;

        private SlipOperate op;

        public RecvSlipCardsProcess(SlipOperate op)
        {
            this.op = op;
            this.operate = op.operates[op.selfIndex];
        }

        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();

            panel.setOperate(operate);
            CPMatch match = CPMatch.match;
            match.step = op.step;
            match.setStage(op.stage);

            match.getPCards()[op.index].fixCards.clear();

            Rule rule = match.getRoomRule().rule;
            if (rule.sid == 1017) 
                SoundManager.playAn(Room.room.players[op.index].player.sex, rule.getIntAtribute("soundType"));// 播放暗

            if (op.index == match.mindex)
            {
                match.getPCards()[op.index].addFixCard(op.cards);
                match.getPCards()[op.index].handcards.clear();

                match.getPCards()[op.index].AddHandCards(op.draws);

                panel.refreshFixed(op.index);
                panel.allHandView.selfView.getHandView().showHandCard(op.draws, new int[0]);
            }
            else
            {
                match.getPCards()[op.index].addFixCard(op.cards);
                panel.refreshFixed(op.index);
            }

            if (operate > 0)
            {
                //自己有划牌的动作,如果是小家，另外处理

                if (panel.havOperate(OperateView.CAN_SLIP))
                {
                    if (match.isXiaoJia())
                    {
                        panel.operateView.showButtonList(OperateView.getShowOperate(operate));
                    }
                    else
                    {
                        if (Room.room.roomRule.rule.getRuleAtribute("isshowtoupai"))
                            panel.operateView.showButton(operate, op.paidui);
                        // CommandManager.addCommand(new SendMatchCommand(SendMatchCommand.SLIP, step, Card.NO_CARD, 3));
                        //3在这里其实是没用的,庄家或者闲家,后台不需要传数字
                    }
                }
                else
                {
                    if (match.isXiaoJia())
                    {
                        panel.operateView.showButtonList(OperateView.getShowOperate(operate));
                    }
                    else
                    {
                        panel.operateView.showButton(operate, op.paidui);
                    }
                }


                if (operate == OperateView.CAN_DISCARD)
                {
                    panel.allHandView.selfView.showHuaDong();
                }
                else
                {
                    panel.allHandView.selfView.hideHuaDong();
                }
            }

            ///最后一个人滑完牌后，检查自己有没有报的操作，有就显示出来。
            if (StatusKit.hasStatus(operate, OperateView.CAN_BAOPAI))
            {
                panel.operateView.showButton(operate, op.paidui);
            }

            int[] bb = panel.getSelfHandCard();

            panel.allHandView.selfView.getHandView().showHandCard(bb, op.disableCard);

            match.paidui = op.paidui;
            panel.refreshCardNum();

            panel.refreshFuShu();

            panel.refreshClock(op.round);

            panel.showTextinfo(false);

            this.op.isOver = true;
        }
    }
}
