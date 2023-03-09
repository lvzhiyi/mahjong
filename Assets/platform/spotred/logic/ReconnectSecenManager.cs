using cambrian.common;
using platform.spotred.room;
using platform.spotred.waitroom;

namespace platform.spotred
{
    public class ReconnectSecenManager : BytesSerializable
    {
        private static ReconnectSecenManager sceneManager;

        public static ReconnectSecenManager manager
        {
            get
            {
                if (sceneManager == null)
                    sceneManager = new ReconnectSecenManager();
                return sceneManager;
            }
        }

        private int card;
        private bool isfanpai;
        private int round;
        private long roundTime;
        private int operate;

        public void bytesReadInfos(ByteBuffer data)
        {
            card = data.readInt(); //翻或者打的牌
            isfanpai = data.readBoolean();
            round = data.readInt(); //(该哪个人的回合)
            roundTime = data.readLong(); //
            operate = data.readInt();
        }


        public override void bytesRead(ByteBuffer data)
        {
            SpotRoomPanel spotRoomPanel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            spotRoomPanel.clearBaseOperate();

            int mo_cards = -1;
            int[] disableCard = new int[0];
            if (StatusKit.hasStatus(operate, OperateView.CAN_DISCARD))
            {

                disableCard = new int[data.readInt()];
                for (int i = 0; i < disableCard.Length; i++)
                {
                    disableCard[i] = data.readInt();
                }

                //针对于打大，报牌，只有是小家报牌后才有用
                mo_cards = data.readInt();
            }

            CPMatch match = CPMatch.match;

            if (card != Card.NO_CARD)
            {
                match.setLastPlayerCard(round, card);
            }

            spotRoomPanel.setPMCard(card);
            spotRoomPanel.setOperate(operate);
            spotRoomPanel.refresh();
            spotRoomPanel.refreshClock(round);
            spotRoomPanel.refreshCardNum();
            spotRoomPanel.refreshBaoPai();
            spotRoomPanel.refreshPiao();
            spotRoomPanel.ShowMatchPlayerInfo();


            ///报牌后,去获取不能出的牌，只有打这些才符合要求(有报牌状态，并且可以出牌)
            if (match.getSelfPlayerCards<CPPlayerCards>().hasStatus(CPPlayerCards.STATUS_BAOPAI) &&
                StatusKit.hasStatus(operate, OperateView.CAN_DISCARD))
            {
                if (match.isXiaoJia())
                    match.getSelfPlayerCards<CPPlayerCards>().setCanplayCards(new[] {mo_cards});
                int[] disableCard_1 = match.getBaoDisableCards();
                if (disableCard_1 != null)
                    disableCard = disableCard_1;
                match.getSelfPlayerCards<CPPlayerCards>().setCanplayCards(null);
            }

            //这里要增加当自己出牌的时候，不能出的牌数组
            spotRoomPanel.allHandView.selfView.getHandView()
                .showHandCard(match.getSelfHandCard(), disableCard);

            if (StatusKit.hasStatus(operate, OperateView.CAN_SLIP))
            {
                if (match.isXiaoJia())
                {
                    spotRoomPanel.operateView.showButtonList(OperateView.getList(operate));
                }
                else
                {
                    if (Room.room.roomRule.rule.getRuleAtribute("isshowtoupai"))
                    {
                        spotRoomPanel.operateView.showButtonList(OperateView.getList(operate));
                    }
                }
            }
            else if (round == Room.room.indexOf())
            {
                if (match.isXiaoJia())
                {
                    spotRoomPanel.operateView.showButtonList(OperateView.getList(operate));
                }
                else
                {
                    if ((operate & OperateView.CAN_KONG) == OperateView.CAN_KONG)
                    {
                        spotRoomPanel.operateView.showButton(operate, match.paidui);
                    }
                    else if((operate & OperateView.CAN_SINGLE) == OperateView.CAN_SINGLE)
                    {
                        spotRoomPanel.operateView.showButton(operate, match.paidui);
                    }
                    else if (match.getPlayerCardss <CPPlayerCards>()[round].getSlipCards().Length > 0)
                    {
                        if (Room.room.roomRule.rule.getRuleAtribute("isshowtoupai"))
                        {
                            spotRoomPanel.operateView.showButtonList(OperateView.getList(operate));
                        }
                    }
                    else
                    {
                        spotRoomPanel.operateView.showButton(operate, match.paidui);
                    }

                }
            }
            else if (operate > 0)
            {
                spotRoomPanel.operateView.showButton(operate, match.paidui);
            }

            if (card != Card.NO_CARD)
            {
                if (isfanpai)
                    spotRoomPanel.showFanCard(card, round, null);
                else
                    spotRoomPanel.showPlayCard(round, card, true);
            }

            if (operate == OperateView.CAN_DISCARD)
            {
                spotRoomPanel.allHandView.selfView.showHuaDong();
            }

            for (int i = 0; i < Room.room.players.Length; i++)
            {
                spotRoomPanel.refreshFixed(i);
            }

            for (int i = 0; i < Room.room.players.Length; i++)
            {
                spotRoomPanel.refreshDisCard(i);
            }

            spotRoomPanel.setVisible(true);

            Room.room.cancelReady();
            SpotWaitRoomPanel panel = UnrealRoot.root.checkDisplayObject<SpotWaitRoomPanel>();
            if (panel != null)
                panel.setVisible(false);
        }
    }
}
