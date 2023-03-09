using cambrian.game;
using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 接收碰牌消息
    /// </summary>
    public class RecvPongCardProcess:Process
    {
        int operate;//操作类型

        private PongOperate op;
        public RecvPongCardProcess(PongOperate op)
        {
            this.op = op;
            operate = op.operates[op.selfIndex];
        }

        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.setOperate(operate);
            CPMatch match = CPMatch.match;
            match.step= op.step;
            match.setStage(op.stage);
            ///重置上一家人打的牌
            match.ResetPlayerCard();

            int[] cards = new int[0];
            ///自己回合
            if (op.index == match.mindex)
            {
                if (op.count >2)//划牌
                {
                    cards=new int[op.count];
                    for (int i = 0; i < cards.Length; i++)
                    {
                        cards[i] = op.card;
                    }
                    //移除手牌区的牌
                    if(!match.isXiaoJia())
                        match.getPCards()[op.index].delAllSameCard(op.card, op.count);
                    else
                    {
                        match.getPCards()[op.index].delAllSameCard(op.card, op.count);
                    }

                    panel.addFixed(op.index, FixedCards.PONG, cards);//添加到固定牌区
                    
                }
                else
                {
                    cards = new int[3];
                    for (int i = 0; i < cards.Length; i++)
                    {
                        cards[i] = op.card;
                    }

                    //移除手牌区的牌
                    match.getPCards()[op.index].delAllSameCard(op.card, op.count);//从手里拿出来的牌数量

                    panel.addFixed(op.index, FixedCards.PONG, cards);//添加到固定牌区

                    panel.refreshDisCard(op.destindex);
                }

               // panel.refreshFixed(index);//刷新固定区的牌
                panel.allHandView.selfView.getHandView().showHandCard(panel.getSelfHandCard(), new int[0]);
            }
            else
            {
                if (op.count > 2)
                {
                    cards=new int[op.count];
                    for (int i = 0; i < op.count; i++)
                    {
                        cards[i] = op.card;
                    }
                    panel.addFixed(op.index, FixedCards.PONG, cards);//添加到固定牌区
                }
                else
                {
                    cards=new int[3];
                    for (int i = 0; i < 3; i++)
                    {
                        cards[i] = op.card;
                    }

                    panel.addFixed(op.index, FixedCards.PONG, cards);
                   
                    panel.refreshDisCard(op.destindex);
                }
                panel.allHandView.selfView.getHandView().showHandCard(panel.getSelfHandCard(), new int[0]);
            }

            panel.operateView.hideAllBtn();
            panel.refreshClock(op.round);
            panel.hideFanCard();


            panel.refreshFuShu();
            panel.showTextinfo(false);
            panel.allHandView.selfView.hideHuaDong();

            panel.hideAllPlayCard();
            SoundManager.playPeng(Room.room.players[op.index].player.sex, CPMatch.match.getRoomRule().rule.getIntAtribute("soundType"));

            if (match.getRoomRule().rule.sid != 1017) // 金堂考考显示碰
                panel.playAnimation(op.index, MovieClipView.MC_TYPE_CHE);
            else
                panel.playAnimation(op.index, MovieClipView.MC_TYPE_PENG);

            panel.refreshMoveFixed(op.index, animationOver);
        }


        public void animationOver()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.refreshFixed(op.index);
            this.op.isOver = true;
        }
    }
}
