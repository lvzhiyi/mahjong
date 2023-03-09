using basef.rule;
using cambrian.common;
using cambrian.game;
using UnityEngine;

namespace platform.spotred.room
{
    /// <summary>
    /// 巴杠
    /// </summary>
    public class RecvKongProcess:Process
    {

        private KongOperate op;

        private int operate;
        public RecvKongProcess(KongOperate op)
        {
            this.op = op;
            this.operate = op.operates[op.selfIndex];
        }


        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.setOperate(operate);
            CPMatch match = CPMatch.match;
            match.step=op.step;
            match.setStage(op.stage);
            match.ResetPlayerCard();// 兼容金堂考考翻牌扒
            Rule rule = CPMatch.match.getRoomRule().rule;
            SoundManager.playBa(Room.room.players[op.index].player.sex, rule.getIntAtribute("soundType"));

            if (op.index == match.mindex)
            {
                ArrayList<int> handcards = match.getPCards()[op.index].delHandCard(op.card, 1);
                panel.allHandView.selfView.getHandView().showHandCard(handcards.toArray(), op.disableCard);//刷新手牌

                if(match.isXiaoJia())
                    panel.operateView.showButtonList(OperateView.getShowOperate(operate));
                else
                    panel.operateView.showButton(operate, op.paidui);
            }

            if (rule.sid == 1017 && op.operates[op.selfIndex] != 0) 
            {
                if (match.isXiaoJia())
                    panel.operateView.showButtonList(OperateView.getShowOperate(operate));
                else
                    panel.operateView.showButton(operate, op.paidui);
            }

            match.getPCards()[op.index].getSameFixedCards(op.card,3).addSameCards(op.card);
            panel.refreshFixed(op.index);

            panel.showCard(op.index, op.card,SendView.KONG);

            this.op.isOver = true;
        }
    }
}
