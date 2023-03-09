using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 显示听牌内容
    /// </summary>
    public class MJShowTingCardInfoProcess:MouseClickProcess
    {
        private MJMatch match;

        private MJPlayerCards playerCards;
        public override void execute()
        {
            MahjongRoomPanel panel= getRoot<MahjongRoomPanel>();

            match=MJMatch.match;

            if (panel.gameView.tingCardView.getVisible())
            {
                if (match.isRound(match.mindex)&& panel.getOperate() != 0)
                    return;
                else
                {
                    panel.gameView.tingCardView.setVisible(false);
                    return;
                }
            }
            else
            {
                if (match.isRound(match.mindex)&& panel.getOperate()!=0)
                {
                    panel.gameView.tingCardView.setVisible(false);
                    return;
                }
            }

            playerCards= match.getSelfPlayerCards<MJPlayerCards>();
          
            TingCardsInfo infos = MJMatch.match.getDengTingCards(match.mindex);
            if (playerCards.hasStatus(MJPlayerCards.STATUS_TANG))
            {
                TingCardsInfo tingInfo = match.getTangTing(playerCards.getTangCards(), match.mindex);
                getTing(infos, tingInfo);
                infos = tingInfo;
            }

            if (infos == null)
            {
                transform.gameObject.SetActive(false);
                return;
            }

            panel.gameView.setTingView(infos);
        }

        private void getTing(TingCardsInfo source,TingCardsInfo dest)
        {
            ArrayList<TingCard> list= source.getTingList();

            ArrayList<TingCard> destlist = dest.getTingList();

            TingCard info;
            for (int i = 0; i < list.count; i++)
            {
                info = list.get(i);
                for (int j = 0; j < destlist.count; j++)
                {
                    if (info.card == destlist.get(j).card)
                    {
                        destlist.set(info, j);
                        break;
                    }
                }
            }

            bool isTang = playerCards.hasStatus(MJPlayerCards.STATUS_TANG);

            dest.resetCount();
            for (int j = 0; j < destlist.count; j++)
            {
                dest.addCount(destlist.get(j).num);
                if (isTang)
                    destlist.get(j).fan++;
            }
        }
    }

   
}
