using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 麻将躺牌
    /// </summary>
    public class MJTangCardProcess:MouseClickProcess
    {
        public override void execute()
        {
            MJMatch match = MJMatch.match;
            MahjongRoomPanel panel= getRoot<MahjongRoomPanel>();

            int[] tangs= match.getTangCardsIndexs().toArray();

            if (tangs.Length == 0)
            {
                Alert.autoShow("请选择需要躺的牌");
                return;
            }

            MJPlayerCards playerCards = match.getSelfPlayerCards<MJPlayerCards>();
            int[] cards= playerCards.getTangCardValue(tangs);
            int[] notangCards=playerCards.getNOTangCardValue(tangs);
            TingCardsInfo tingInfo= match.getTangTing(cards,match.mindex);
            if (tingInfo == null)
            {
                match.getTangCardsIndexs().clear();
                panel.refreshTangSelectUpCard();
                return;
            }

            ArrayList<int> h = new ArrayList<int>(notangCards);
            MJCardsCounter counter=new MJCardsCounter(match.getRoomRule().rule,h ,playerCards.fixCards, match.paidui);
            if (!counter.isEliminate())
            {
                match.getTangCardsIndexs().clear();
                panel.refreshTangSelectUpCard();
                return;
            }

            if (cards.Length == 2 && cards[0] == cards[1])
            {
                match.getTangCardsIndexs().clear();
                panel.refreshTangSelectUpCard();
                return;
            }

            int[][] counts = Eliminator.getCounts(cards);
            Eliminator.simplify(counts, getTingCards(tingInfo),MJCard.ALL_BITS);
            int[] outTang = Eliminator.getTang(counts);

            if (outTang.Length != cards.Length)
            {
                match.getTangCardsIndexs().clear();
                panel.refreshTangSelectUpCard();
                return;
            }
            transform.parent.parent.gameObject.SetActive(false);
            CommandManager.addCommand(new SendMJMatchCommand(SendMJMatchCommand.LIE_CARD,match.step, playerCards.mocard, outTang));
        }

        private int[] getTingCards(TingCardsInfo tingInfo)
        {
            ArrayList<int> ting=new ArrayList<int>();
            ArrayList<TingCard> card= tingInfo.getTingList();
            for (int i = 0; i < card.count; i++)
            {
                ting.add(card.get(i).card);
            }

            return ting.toArray();
        }
    }
}
