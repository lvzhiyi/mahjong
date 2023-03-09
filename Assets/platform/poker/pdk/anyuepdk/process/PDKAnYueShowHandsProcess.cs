using cambrian.common;

namespace platform.poker
{
    public class PDKAnYueShowHandsProcess : PKOperateEvent
    {
        private PDKAnYueCardInfo userSelect;

        private PPDKAnYueGameView gameView;

        public override void mouseClick()
        {
            userSelect = PDKAnYueMatch.match.getSelectCard();

            PDKAnYueCardType.checkCardsType(userSelect, Room.room.roomRule.rule);

            var panel = UnrealRoot.root.getDisplayObject<PDKAnYueRoomPanel>();

            gameView = (PPDKAnYueGameView)panel.gameView;

            if (userSelect.cards.Length <= 0)
            {
                gameView.showHint("请选择想要出的牌!"); return;
            }

            if (userSelect.type == PKCardType.TYPE_ERROR)
            {
                gameView.showHint("牌型错误,请重新选择!"); return;
            }

            if (!PDKAnYueMatch.match.deskCard.compareTo(userSelect))
            {
                gameView.showHint("选择的牌不符合出牌规则!"); return;
            }

            if (PDKAnYueMatch.match.getPCardIndex((PDKAnYueMatch.match.mindex + 1) % PDKAnYueMatch.match.playerCount).handcards.count == 1
                && PDKAnYueMatch.match.rule.getRuleAtribute("baodanshowmax")
                && userSelect.type == PKCardType.TYPE_CARDS_SINGLE)
            {
                if (Poker.getValue(userSelect.cards[0]) != Poker.getValue(PDKAnYueMatch.match.getPCardIndex(PDKAnYueMatch.match.mindex).getSortHandCards()[0]))
                {
                    gameView.showHint("下家单牌,必须出最大手牌!"); return;
                }
            }

            if (PDKAnYueMatch.match.step == 1)
            {
                bool heitaosanxianchu = PDKAnYueMatch.match.rule.getRuleAtribute("heitaosanxianchu");
                int order = PDKAnYueMatch.match.rule.getIntAtribute("order");

                if (heitaosanxianchu&&(order==0||(order==1&&Room.room.roomRule.getNowsPlayNum() == 1)))
                {
                    int firstCard = PDKAnYueMatch.match.firstCard;
                    if (firstCard == 0)
                    {
                        gameView.showHint("手牌牌错误!请关闭游戏，重新进入!" + firstCard);
                        return;
                    }
                    bool heitaosan = false;
                    string[] typeNames = { "方块", "梅花", "红桃", "黑桃" };
                    foreach (var item in userSelect.cards)
                    {
                        if (item == firstCard)
                        {
                            heitaosan = true; break;
                        }
                    }
                    if (!heitaosan)
                    {
                        gameView.showHint("首次出牌请包含" + typeNames[Poker.getType(firstCard)] + Poker.getValue
                            (firstCard) + "!"); return;
                    }
                }
            }

            sendHnadCard();
        }

        private void sendHnadCard()
        {
            CommandManager.addCommand(new PDKSendMatchCommand(PKSendMatch.DISCARD, PDKAnYueMatch.match.step, userSelect), backSendHnadCard);
        }

        private void backSendHnadCard(object obj)
        {
            transform.parent.gameObject.SetActive(false);
            userSelect = null;
        }
    }
}
