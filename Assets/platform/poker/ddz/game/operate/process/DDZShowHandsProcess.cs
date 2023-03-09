using cambrian.common;

namespace platform.poker
{
    public class DDZShowHandsProcess : PKOperateEvent
    {
        private DDZCardInfo userSelect;

        private PDDZGameView gameView;

        public override void mouseClick()
        {
            userSelect = DDZMatch.match.getSelectCard();

            DDZCardType.checkTypeAndLevel(userSelect);

            var panel = UnrealRoot.root.getDisplayObject<PDDZRoomPanel>();

            gameView = (PDDZGameView)panel.gameView;

            userSelect = special(userSelect, DDZMatch.match.deskCard.lastCards);

            if (userSelect.cards.Length <= 0)
            {
                gameView.showHint("请选择想要出的牌!"); return;
            }

            if (userSelect.type == PKCardType.TYPE_ERROR)
            {
                gameView.showHint("牌型错误!"); return;
            }

            if (!DDZMatch.match.deskCard.compareTo(userSelect))
            {
                gameView.showHint("选择的牌不符合出牌规则!"); return;
            }

            sendHnadCard();
        }

        private DDZCardInfo special(DDZCardInfo usercards, DDZCardInfo deskcards)
        {
            if (deskcards == null && usercards.type != PKCardType.TYPE_ERROR) return usercards;
            int[] cards = usercards.cards;
            if (usercards.type == PKCardType.TYPE_4_2_2
                && deskcards.type == PKCardType.TYPE_FEIJI_1
                && cards.Length == deskcards.cards.Length)
            {
                int level = DDZCardType.isFeiji_1(cards);
                if (level != -1)
                {
                    usercards.level = level;
                    usercards.type = PKCardType.TYPE_FEIJI_1;
                }
            }
            return usercards;
        }

        private void sendHnadCard()
        {
            CommandManager.addCommand(new DDZSendMatchCommand(PKSendMatch.DISCARD, DDZMatch.match.step, userSelect), backSendHnadCard);
        }

        private void backSendHnadCard(object obj)
        {
            transform.parent.gameObject.SetActive(false);
            userSelect = null;
        }
    }
}
