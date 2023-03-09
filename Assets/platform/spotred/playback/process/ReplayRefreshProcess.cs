using platform.spotred.room;

namespace platform.spotred.playback
{
    public class ReplayRefreshProcess: Process
    {
        CPMatch scene;
        public void setScene(CPMatch scene)
        {
            this.scene = scene;
        }
        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();

            CPMatch.match = this.scene;

            panel.refreshAllHandCards(0);
            panel.refreshAllDisCard(0);
            panel.refreshFixed(0);
            panel.rcview.refresh();
            panel.hideHu();
            panel.refreshCardNum();
            panel.hideFanCard();
            panel.hideAllPlayCard();
            panel.hideOperates();

            panel.headView.hideAllPiao();
            panel.allHandView.hideBaoPai();

            for (int i = 0; i < scene.getPCards().Length; i++)
            {
                if (scene.getPCards()[i].hasStatus(CPPlayerCards.STATUS_PIAO))
                {
                    panel.headView.showPiao(RoomPanel.getPlayerIndex(i),true);
                }
                if (scene.getPCards()[i].hasStatus(CPPlayerCards.STATUS_BAOPAI))
                {
                    panel.refreshBaoPaiIndex(i);
                }
            }
        }
    }
}
