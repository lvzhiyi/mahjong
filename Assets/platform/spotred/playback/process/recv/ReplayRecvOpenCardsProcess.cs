using platform.spotred.playback;
using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 翻牌
    /// </summary>
    public class ReplayRecvOpenCardsProcess:Process
    {
        private OpenCardOperate operate;

        public ReplayRecvOpenCardsProcess(BaseOperate operate)
        {
            this.operate = (OpenCardOperate)operate;
        }


        CPMatch match;
        /// <summary>
        /// 接收翻牌消息
        /// </summary>
        public override void execute()
        {
            match = CPMatch.match;
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();

            match.setStage(operate.stage);

            if (match.addLastPlayerCardToDisable())
            {
                panel.hideDisLastCard(match.getLastPlayerIndex());
                panel.moveShowCardToDisable(match.getLastPlayerIndex(), ab);
                match.ResetPlayerCard();
            }
            else
            {
                ab(false);
            }
          
        }

        public void ab(bool b)
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();

            if (b) match.ResetPlayerCard();

            panel.hidePlayCard(operate.index);

            panel.showFanCard(operate.card, operate.index,null);

            match.paidui=operate.paidui;

            panel.refreshCardNum();

            panel.setPMCard(operate.card);

            match.setLastPlayerCard(operate.index, operate.card);

            panel.hideAllPlayCard();

            panel.refreshClock(operate.round);

            this.operate.playOver();
        }

       
    }
}
