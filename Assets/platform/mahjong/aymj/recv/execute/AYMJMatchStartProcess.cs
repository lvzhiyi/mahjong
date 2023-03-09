namespace platform.mahjong
{
    /// <summary>
    /// 游戏开始
    /// </summary>
    public class AYMJMatchStartProcess:Process
    {
        AYMJMatchStartOperate operate;

        int selfOperate;

        public AYMJMatchStartProcess(MJBaseOperate bOperate)
        {
            operate = (AYMJMatchStartOperate)bOperate;
            selfOperate = operate.getSelfOperate();
        }

        AYMJRoomPanel panel;
        public override void execute()
        {
            Room room = Room.room;
            room.roomRule.setGameNum(operate.tray);
            room.cancelReady();

            AYMJMatch.match = new AYMJMatch(room.getPlayerCount(), operate.step, operate.banker, room.getMasterIndex(), operate.paidui);

            AYMJMatch.match.setPlayers(room.players, room.indexOf());
            AYMJMatch.match.setRoomRule(room.roomRule);

            panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
            panel.setOperate(operate.getSelfOperate());
            panel.refresh();
            panel.showGameView();
            panel.refreshCardNum();
            panel.gameView.getMjIndexCenterView().setJuShu(room.roomRule.getNowsPlayNum(),room.roomRule.getMatchCount());
            panel.showCountTime(operate.round);
            panel.showIPView();

            UnrealRoot.root.showPanel<AYMJRoomPanel>();

            showOperate();
        }

        private void showOperate()
        {
           if (MJOperate.isCanPiao(selfOperate))//选择飘
            {
                panel.gameView.getMjIndexCenterView().showAllDirection();
                panel.refreshSelectPiao(AYMJMatch.match.mindex);
            }
            else
            {
                otherOperate();
            }

            operate.playOver();
        }

        public void otherOperate()
        {
            int card = AYMJMatch.match.getSelfPlayerCards<AYMJPlayerCards>().handcards.get(0);
            MJOperateInfoBean[] bean = AYMJMatch.match.getMJOperateInfos(selfOperate, card, false, AYMJMatch.match.mindex);
            panel.gameView.getOperateView().showOperate(bean);
            if (selfOperate > 0)
            {
                TingCardsInfo[] tinginfos= AYMJMatch.match.getSelfTingCards(AYMJMatch.match.mindex);
                panel.refreshHandCard(AYMJMatch.match.mindex, tinginfos, false);
                if (MJOperate.isDisCard(selfOperate))
                    panel.gameView.showDisCardRedmine(true);
            }
        }
    }
}
