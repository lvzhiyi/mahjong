using cambrian.game;

namespace platform.spotred.room
{
    /// <summary>
    /// 接收游戏结束
    /// </summary>
    public class RecvOverProcess : Process
    {
        /// <summary>
        /// 正常结束
        /// </summary>
        public const int NORMAL_OVER = -1;

        private OverOperate op;

        public RecvOverProcess(OverOperate op)
        {
            this.op = op;
        }

        private int status;

        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();

            CPMatch match = CPMatch.match;

            match.setStage(op.stage);
            Room cloneRoom = Room.room.clone();

            op.scene.banker = match.banker;
            op.scene.dangjia = match.dangjia;
            op.scene.xiaojia = match.xiaojia;
            op.scene.mindex = match.mindex;
            match.step = op.step;

            Room.room.addSpotRedCount(op.scene.getSpotRedCounts());

            if (Room.room.isType(Room.JBC))
            {
                ClubGoldOverPanel overpanel = UnrealRoot.root.getDisplayObject<ClubGoldOverPanel>();
                overpanel.show(cloneRoom, op.scene, match.hu_card, op.index);
                status = 0;
            }
            else
            {
                OverPanel overpanel = UnrealRoot.root.getDisplayObject<OverPanel>();
                overpanel.show(cloneRoom, op.scene, match.hu_card, op.index);
                status = 2;
            }

            panel.refreshClock(op.round);
            panel.closeSelfClock();


            if (op.index != NORMAL_OVER)
            {
                string str = Room.room.players[op.index].player.name;
                Alert.show("由于玩家[" + str + "]剩余手牌在当前规则下都不允许被打出,因此本局自动结束。");
            }

            panel.showTextinfo(false);
            panel.allHandView.selfView.hideHuaDong();

            panel.waitSceond(execbb, 0.5f);

            WXManager.getInstance().getGPSLocation("Root", "call_back_gps");
        }


        public void execbb()
        {
            if (status == 0)
            {
                ClubGoldOverPanel overpanel = UnrealRoot.root.getDisplayObject<ClubGoldOverPanel>();
                overpanel.setVisible(true);
            }
            else
            {
                OverPanel overpanel = UnrealRoot.root.getDisplayObject<OverPanel>();
                overpanel.setVisible(true);
            }
            op.playOver();
        }
    }
}
