using System.Collections;
using cambrian.common;
using cambrian.game;
using UnityEngine;

namespace platform.poker
{
    public class PDDZDealPokersView : PKDealPokersView
    {
        protected PDDZRoomPanel panel;

        /// <summary> 发牌 </summary>
        public override void deal(int[][] cards)
        {
            panel = UnrealRoot.root.getDisplayObject<PDDZRoomPanel>();
            base.deal(cards);
            DealPokerTimeSpace = DealPokerTime / cards[0].Length;
        }

        /// <summary> 显示明牌 </summary>
        public override void dealMingPai(int pos, int[] card, bool dizhu)
        {
            cards[pos] = card;
            mingpai[pos] = true;
            switch (PKGAME.GetIndex(pos))
            {
                case PKGAME.DOWN:
                    panel.gameView.hand.refresHands(pos, downcards.toArray(), mingpai[pos], dizhu);
                    break;
                case PKGAME.RIGHT:
                    rightcards = addCardsHand(rightcards, cards[pos]);
                    panel.gameView.hand.refresHands(pos, rightcards.toArray(), mingpai[pos], dizhu);
                    break;
                case PKGAME.LEFT:
                    leftcards = addCardsHand(leftcards, cards[pos]);
                    panel.gameView.hand.refresHands(pos, leftcards.toArray(), mingpai[pos], dizhu);
                    break;
            }
        }

        /// <summary> 添加手牌 </summary>
        protected override ArrayList<int> addCardsHand(ArrayList<int> list, int[] cards)
        {
            return base.addCardsHand(list, cards);
        }

        protected override void addHandCards(int pos)
        {
            switch (PKGAME.GetIndex(pos))
            {
                case PKGAME.DOWN:
                    downcards.add(cards[pos][cardnum]);
                    panel.gameView.hand.refresHands(pos, downcards.toArray());
                    break;
                case PKGAME.RIGHT:
                    rightcards.add(cards[pos][cardnum]);
                    panel.gameView.hand.refresHands(pos, rightcards.toArray());
                    break;
                case PKGAME.LEFT:
                    leftcards.add(cards[pos][cardnum]);
                    panel.gameView.hand.refresHands(pos, leftcards.toArray());
                    break;
            }
            panel.gameView.status.setCardNum(pos, cardnum);
        }

        protected override IEnumerator dealCards()
        {
            SoundManager.playPKOther(SoundManager.fapai,0);
            while (downcards.count < cards[0].Length)
            {
                yield return new WaitForSeconds(DealPokerTimeSpace);
                for (int i = 0; i < cards.Length; i++)
                {
                    addHandCards(i);
                }
                cardnum++;
            }
            if (cards[Room.room.indexOf()].Length == cardnum)
            {
                endDealCards();
                yield break;
            }
        }

        protected override void endDealCards()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                panel.gameView.status.setCardNum(i, cards[i].Length);
                panel.gameView.hand.refresHands(i, CardSort.LTSCards(cards[i]));
            }

            if (StatusKit.isStatus(panel.operate, PKOperateStatus.CAN_MING))
            {
                CommandManager.addCommand(new DDZSendMatchCommand(PKSendMatch.CANCEL_MING, DDZMatch.match.step, (DDZCardInfo)null));
            }

            panel.gameView.clock.setVisible(true);
            panel.gameView.clock.showClock();
            panel.gameView.recorder.refresh();
            panel.gameView.operate.showOperateView(panel.operate);
        }
    }
}
