using platform.spotred.room;
using platform.spotred;
using UnityEngine;

namespace platform.spotred.playback
{
    /// <summary>
    /// 接收摸牌消息
    /// </summary>
    public class ReplayRecvDrawCardsProcess : Process
    {
        private DrawOperate operate;
        public ReplayRecvDrawCardsProcess(BaseOperate operate)
        {
            this.operate = (DrawOperate)operate;
        }

        CPMatch match;
        public override void execute()
        {
            match = CPMatch.match;
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();

            match.setStage(operate.stage);

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
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();

            if (b)
            {
                match.ResetPlayerCard();
            }

            //后面更新要将其他人出的牌隐藏掉
            panel.hidePlayCard(this.operate.index);

            match.paidui=this.operate.paidui;

            panel.refreshCardNum();

            panel.refreshClock(this.operate.round);
            panel.hideFanCard();
            
            panel.showTouCardAnimation(this.operate.cards[0], this.operate.index, drawCardsOver);
        }

        void drawCardsOver(bool b)
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            //加进入
            match.getPCards()[this.operate.index].AddHandCards(this.operate.cards);
            int[] handcards =match.getPCards()[this.operate.index].handcards.toArray();
           
            panel.refreshHandCards(this.operate.index, handcards);


            panel.refreshFuShu();

            this.operate.playOver();
        }
    }
}
