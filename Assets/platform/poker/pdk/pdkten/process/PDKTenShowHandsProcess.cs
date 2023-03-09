using basef.rule;
using cambrian.common;

namespace platform.poker
{
    public class PDKTenShowHandsProcess : PKOperateEvent
    {
        private PDKTenCardInfo userSelect;

        private PPDKTenGameView gameView;

        public override void mouseClick()
        {
            userSelect = PDKTenMatch.match.getSelectCard();
            Rule rule= Room.room.getRule();
            PDKTenCardType.checkCardsType(userSelect, rule);

            var panel = UnrealRoot.root.getDisplayObject<PDKTenRoomPanel>();

            gameView = (PPDKTenGameView)panel.gameView;

            if (userSelect.cards.Length <= 0)
            {
                gameView.showHint("请选择想要出的牌!"); return;
            }

            if (userSelect.type == PKCardType.TYPE_ERROR)
            {
                gameView.showHint("牌型错误,请重新选择!"); return;
            }

            if (userSelect.type == PKCardType.TYPE_CARDS_BOMB&& userSelect.cards.Length<4&& !PDKTenMatch.match.rule.getRuleAtribute("aaaisboom"))
            {
                userSelect.type = PKCardType.TYPE_CARDS_THREE;
            }

            if (!PDKTenMatch.match.deskCard.compareTo(userSelect))
            {
                gameView.showHint("选择的牌不符合出牌规则!"); return;
            }


            var playerIndex = (PDKTenMatch.match.mindex + 1) % PDKTenMatch.match.playerCount;
            if (PDKTenMatch.match.getPCardIndex(playerIndex).handcards.count == 1
                && PDKTenMatch.match.rule.getRuleAtribute("baodanshowmax")
                && userSelect.type == PKCardType.TYPE_CARDS_SINGLE)
            {
                if (Poker.getValue(userSelect.cards[0]) != Poker.getValue(PDKTenMatch.match.getPCardIndex(PDKTenMatch.match.mindex).getSortHandCards()[0]))
                {
                    gameView.showHint("下家单牌,必须出最大手牌!"); return;
                }
            }

            var notlunZhuang= (PDKTenMatch.match.playerCount == 2)&& (rule.getRuleAtribute("lunZhuang")) && Room.room.roomRule.num > 0;

           

            if (PDKTenMatch.match.step == 1)
            {
                int order = PDKTenMatch.match.rule.getIntAtribute("order");

                if (!notlunZhuang&& order == 0 || (order == 1 && Room.room.roomRule.getNowsPlayNum() == 1))
                {
                    int firstCard = PDKTenMatch.match.firstCard;
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
                            heitaosan = true;
                            break;
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
            CommandManager.addCommand(new PDKSendMatchCommand(PKSendMatch.DISCARD, PDKTenMatch.match.step, userSelect), backSendHnadCard);
        }

        private void backSendHnadCard(object obj)
        {
            transform.parent.gameObject.SetActive(false);
            userSelect = null;
        }
    }
}
