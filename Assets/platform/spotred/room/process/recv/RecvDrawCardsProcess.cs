using basef.rule;
using cambrian.common;
using cambrian.game;

namespace platform.spotred.room
{
    /// <summary>
    /// 接收摸牌消息
    /// </summary>
    public class RecvDrawCardsProcess : Process
    {
        int operte; //操作

        private DrawOperate op;
        public RecvDrawCardsProcess(DrawOperate op)
        {
            this.op = op;
            operte = op.operates[op.selfIndex];
        }

        CPMatch match;
        public override void execute()
        {
            match = CPMatch.match;
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.setOperate(0);
            match.step=op.step;

            if (match.addLastPlayerCardToDisable())
            {
                panel.hideDisLastCard(match.getLastPlayerIndex());
                panel.moveShowCardToDisable(match.getLastPlayerIndex(), ab);
            }
            else
            {
                ab(false);
            }
        }

        public void ab(bool b)
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();

            if (!match.isXiaoJia(op.index))
            {
                Rule rule = CPMatch.match.getRoomRule().rule;
                if (rule.sid == 1017) SoundManager.playTou(Room.room.players[op.index].player.sex, rule.getIntAtribute("soundType"));
            }               
            if (b)
            {
                match.ResetPlayerCard();
            }

            //将其他人出的牌隐藏掉
            panel.hideAllPlayCard();

            match.paidui= op.paidui;

            match.setStage(op.stage);

            panel.refreshCardNum();

            panel.refreshClock(op.round);

            panel.hideFanCard();

            panel.showTouCardAnimation(op.cards[0], op.index, drawCardsOver);
        }


        void drawCardsOver(bool b)
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.setOperate(operte);
            if (op.index == match.mindex)
            {
                int[] handcards = match.getPCards()[op.index].handcards.toArray();
                //加进入
                match.getPCards()[op.index].AddHandCards(op.cards);

                ///如果是小家
                if (match.isXiaoJia())
                {
                    if (match.getSelfPlayerCards<CPPlayerCards>().hasStatus(CPPlayerCards.STATUS_BAOPAI))
                    {
                        match.getSelfPlayerCards<CPPlayerCards>().setCanplayCards(new int[] { op.cards[0] });
                        op.disableCard = match.getBaoDisableCards();
                        match.getSelfPlayerCards<CPPlayerCards>().setCanplayCards(null);
                    }
                    panel.operateView.showButtonList(OperateView.getShowOperate(operte));

                    panel.allHandView.selfView.getHandView().showHandCard(handcards, op.disableCard, op.cards);
                }
                else
                {
                    panel.allHandView.selfView.getHandView().showHandCard(handcards, op.disableCard, op.cards);
                    if (StatusKit.hasStatus(operte, OperateView.CAN_SLIP))
                    {
                        if (Room.room.roomRule.rule.getRuleAtribute("isshowtoupai"))
                        {
                            panel.operateView.showButton(operte, op.paidui);
                        }
                    }
                    else if (StatusKit.hasStatus(operte, OperateView.CAN_PONG))
                    {
                        if (Room.room.roomRule.rule.getRuleAtribute("isshowtoupai"))
                        {
                            panel.operateView.showButton(operte, op.paidui);
                        }
                    }
                    else
                    {
                        panel.operateView.showButton(operte, op.paidui);
                    }
                }

                if (match.isstage(CPMatch.STAGE_SLIP))// 如果处于滑牌阶段，刷新手牌
                {
                    handcards = match.getPCards()[op.index].handcards.toArray();
                    panel.allHandView.selfView.getHandView().showHandCard(handcards, new int[] { 0 }); 
                }
            }
            else
            {
                if (operte > 0)
                {
                    if (match.isstage(CPMatch.STAGE_SLIP) && operte == OperateView.CAN_DISCARD)
                    {
                        int[] handcards = match.getPCards()[op.selfIndex].handcards.toArray();
                        panel.allHandView.selfView.getHandView().showHandCard(handcards, op.disableCard);
                    }
                    panel.operateView.showButtonList(OperateView.getShowOperate(operte));
                }
            }

            if (operte == OperateView.CAN_DISCARD)
                panel.allHandView.selfView.showHuaDong();
            else
                panel.allHandView.selfView.hideHuaDong();
            panel.refreshFuShu();
            panel.showTextinfo(false);

            op.isOver = true;
        }

    }
}
