namespace platform.mahjong
{
    /// <summary>
    /// 游戏开始
    /// </summary>
    public class MJMatchStartProcess:Process
    {
        MJMatchStartOperate operate;

        int selfOperate;

        public MJMatchStartProcess(MJBaseOperate bOperate)
        {
            operate = (MJMatchStartOperate)bOperate;
            selfOperate = operate.getSelfOperate();
        }

        MahjongRoomPanel panel;
        public override void execute()
        {
            Room room = Room.room;
            room.roomRule.setGameNum(operate.tray);
            room.cancelReady();

            MJMatch.match = new MJMatch(room.getPlayerCount(), operate.step, operate.banker, room.getMasterIndex(), operate.paidui);

            MJMatch.match.setPlayers(room.players, room.indexOf());
            MJMatch.match.setRoomRule(room.roomRule);

            panel = UnrealRoot.root.getDisplayObject<MahjongRoomPanel>();
            panel.setOperate(operate.getSelfOperate());
            panel.refresh();
            panel.showGameView();
            panel.refreshCardNum();
            panel.gameView.getMjIndexCenterView().setJuShu(room.roomRule.getNowsPlayNum(),room.roomRule.getMatchCount());
            panel.showCountTime(operate.round);
            panel.showIPView();

            UnrealRoot.root.showPanel<MahjongRoomPanel>();

            showOperate();
        }

        private void showOperate()
        {
           if (MJOperate.isCanPiao(selfOperate))//选择飘
            {
                panel.gameView.getMjIndexCenterView().showAllDirection();
                panel.refreshSelectPiao(MJMatch.match.mindex);
            }

            else if (MJOperate.isCanReplace(selfOperate))
            {
                panel.refreshHuanSZ(0, true);
            }
            else if (MJOperate.isDisType(selfOperate))
            {
                panel.gameView.getMjIndexCenterView().showAllDirection();
                panel.refreshDingQue(0, true);
                panel.gameView.getDQView().showDingQue(MJMatch.match.getRecommendType());
            }
            else
            {
                otherOperate();
            }

            operate.playOver();
        }

        public void otherOperate()
        {
            int card = MJMatch.match.getSelfPlayerCards<MJPlayerCards>().handcards.get(0);
            MJOperateInfoBean[] bean = MJMatch.match.getMJOperateInfos(selfOperate, card, false, MJMatch.match.mindex);
            panel.gameView.getOperateView().showOperate(bean);
            if (selfOperate > 0)
            {
                TingCardsInfo[] tinginfos= MJMatch.match.getSelfTingCards(MJMatch.match.mindex);
                panel.refreshHandCard(MJMatch.match.mindex, tinginfos, false);
                if (MJOperate.isDisCard(selfOperate))
                    panel.gameView.showDisCardRedmine(true);
            }
        }
    }
}
