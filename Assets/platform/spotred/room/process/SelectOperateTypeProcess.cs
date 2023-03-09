using basef.rule;
using cambrian.common;
using cambrian.game;
using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 选择操作类型(碰,过,胡.....)
    /// </summary>
    public class SelectOperateTypeProcess : MouseClickProcess
    {

        public static int[] disableCard;

        public int type;

        public override void execute()
        {
            SoundManager.playButton();
            SpotRoomPanel panel = this.getRoot<SpotRoomPanel>();
            panel.operateView.hideAllBtn();
            Port command = null;
            CPMatch match = CPMatch.match;
            if (match == null) return;
            int step = match.step;
            int sid = Room.room.roomRule.rule.sid;
            if (type == SendMatchCommand.CHOW)
            {
                panel.stauts = PlayCardProcess_1.CHOW;

                if (sid == 1009) //广元市区
                {
                    gysqChowOperate(panel);
                }
                else
                {
                    Rule rule = match.getRoomRule().rule;
                    ArrayList<int> list = match.getPlayerCardss<CPPlayerCards>()[match.mindex].getCanChowCard(panel.getPMCard(), sid, rule.getRuleAtribute("canChaiDouble7"));
                    if (list.count == 1)
                    {
                        command = new SendMatchCommand(SendMatchCommand.CHOW, step, list.get(0), 0, null);
                        panel.showTextinfo(true);
                        panel.stauts = -1;
                        panel.setPMCard(Card.NO_CARD);
                    }
                    else
                    {
                        bool b = rule.getRuleAtribute("isshowtoupai");
                        int[] disableCard = match.getPlayerCardss<CPPlayerCards>()[match.mindex].getDisAbleChowCard(panel.getPMCard(), b, sid).toArray();

                        panel.allHandView.selfView.getHandView().showHandCard(panel.getSelfHandCard(), disableCard);
                        panel.operateView.hideAllBtn();
                        panel.allHandView.selfView.showHuaDong();
                        return;
                    }
                }
            }
            else if (type == SendMatchCommand.PONG)
            {
                if (panel.getPMCard() != Card.NO_CARD)
                {
                    command = new SendMatchCommand(SendMatchCommand.PONG, step, panel.getPMCard(), 2, null);
                    panel.setPMCard(Card.NO_CARD);
                    panel.showTextinfo(true);
                }
            }
            else if (type == SendMatchCommand.KONG)
            {
                if (sid != 1017)
                {
                    if (match.isXiaoJia())
                    {
                        int[] cards = match.getSelfPlayerCards<CPPlayerCards>().getCanKongCards();
                        if (cards.Length == 1)
                        {
                            command = new SendMatchCommand(SendMatchCommand.KONG, step, cards[0], 0, null);
                            panel.setPMCard(Card.NO_CARD);
                        }
                        else
                        {
                            SelectKongCardPanel kc = UnrealRoot.root.getDisplayObject<SelectKongCardPanel>();
                            kc.setData(cards);
                            kc.refresh();
                            kc.setVisible(true);
                        }
                    }
                    else
                    {
                        int card = match.getSelfPlayerCards<CPPlayerCards>().getCanKongCard();
                        command = new SendMatchCommand(SendMatchCommand.KONG, step, card, 0, null);
                        panel.setPMCard(Card.NO_CARD);
                    }
                }
                else // 金堂考考
                {
                    int[] cards = match.getSelfPlayerCards<CPPlayerCards>().getCanKongCards();
                    if (cards == null || cards.Length == 0)  // 翻开牌的扒操作 后端控制
                    {
                        panel.showTextinfo(true);
                        command = new SendMatchCommand(SendMatchCommand.KONG, step, Card.NO_CARD, 0, null);
                        panel.setPMCard(Card.NO_CARD);
                    }
                    else if (cards.Length == 1)
                    {
                        panel.showTextinfo(true);
                        command = new SendMatchCommand(SendMatchCommand.KONG, step, cards[0], 0, null);
                        panel.setPMCard(Card.NO_CARD);
                    }
                    else
                    {
                        SelectKongCardPanel kc = UnrealRoot.root.getDisplayObject<SelectKongCardPanel>();
                        kc.setData(cards);
                        kc.refresh();
                        kc.setVisible(true);
                    }
                }
            }
            else
            {
                if (type == SendMatchCommand.HU)
                    panel.showTextinfo(true);
                else if (type == SendMatchCommand.CANCEL && sid == 1009)
                {
                    panel.allHandView.selfView.getHandView().showHandCard(panel.getSelfHandCard(), new int[0]);
                    panel.allHandView.selfView.hideHuaDong();
                }

                command = new SendMatchCommand(type, step, Card.NO_CARD, 0, null);
            }

            if (command != null)
            {
                CommandManager.addCommand(command);
                panel.allHandView.selfView.getHandView().UpColumnCard(-1);
                if (type != SendMatchCommand.CANCEL)
                    panel.hideAllPlayCard();
            }

        }

        /// <summary>
        /// 针对于广元市区，吃的情况
        /// </summary>
        private void gysqChowOperate(SpotRoomPanel panel)
        {
            CPMatch match = CPMatch.match;
            int operate = panel.operate - OperateView.CAN_CHOW;
            panel.operateView.showButton(operate, 0);

            bool b = Room.room.roomRule.rule.getRuleAtribute("isshowtoupai");
            ArrayList<int> disableCard =
                match.getPlayerCardss<CPPlayerCards>()[match.mindex].getDisAbleChowCard(panel.getPMCard(), b, Room.room.roomRule.rule.sid);

            ArrayList<int> cards = match.getCanPdCards();

            for (int i = 0; i < cards.count; i++)
            {
                int card = cards.get(i);
                for (int j = disableCard.count - 1; j >= 0; j--)
                {
                    if (card == disableCard.get(j))
                        disableCard.removeAt(j);
                }
            }

            cards = match.getNoCanPlayCards();

            if (cards.count > 0)
            {
                for (int i = 0; i < cards.count; i++)
                {
                    disableCard.add(cards.get(i));
                }
            }

            panel.allHandView.selfView.getHandView().showHandCard(panel.getSelfHandCard(), disableCard.toArray());
            panel.allHandView.selfView.showHuaDong();
        }
    }
}
