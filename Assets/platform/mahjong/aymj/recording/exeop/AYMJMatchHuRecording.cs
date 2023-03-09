using cambrian.common;
using cambrian.game;

namespace platform.mahjong
{
    /// <summary>
    /// 点炮胡
    /// </summary>
    public class AYMJMatchHuRecording : Process
    {
        MJMatchHuOperate operate;

        int selfOperate;

        public AYMJMatchHuRecording(MJBaseOperate operate)
        {
            this.operate = (MJMatchHuOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayAYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);
            match.ResetPlayerCard();

            AYMJPlayerCards playerCards = match.getPlayerCardIndex<AYMJPlayerCards>(operate.index);
            playerCards.huSource = operate.from;
            if (StatusKit.hasStatus(operate.huType, MJMatchHuOperate.HU_NORMAL)) //点炮胡
            {
                playerCards.hu(operate.card, operate.order, operate.huType);
                AYMJPlayerCards from = match.getPlayerCardIndex<AYMJPlayerCards>(operate.from);
                from.updateDisableLastSign(MJCard.sign(operate.card, MJCard.SIGN_HU));
                panel.refreshDisAbleView(operate.from);

                SoundManager.playMJHu(Room.room.players[operate.index].player.sex);
            }
            else if (StatusKit.hasStatus(operate.huType, MJMatchHuOperate.HU_SELF)) //自摸
            {
                playerCards.hu(operate.card, operate.order, operate.huType);
                //播放音效
                SoundManager.playMJSelfHu(Room.room.players[operate.index].player.sex);
            }
            else if (StatusKit.hasStatus(operate.huType, MJMatchHuOperate.HU_ROB)) //抢巴杠
            {
                playerCards.hu(operate.card, operate.order, operate.huType);
                AYMJPlayerCards from = match.getPlayerCardIndex<AYMJPlayerCards>(operate.from);
                MJFixedCards fixedCards = (MJFixedCards) from.getSameFixedCards(operate.card, 4);
                if (fixedCards != null)
                {
                    fixedCards.deleteCard(operate.card, 1);
                    fixedCards.type = MJFixedCards.MJPONG;
                    panel.refreshFixedCard(operate.from);
                }
                SoundManager.playMJHu(Room.room.players[operate.index].player.sex);
            }
            else if (StatusKit.hasStatus(operate.huType, MJMatchHuOperate.HU_KONG)) //杠上炮
            {
                playerCards.hu(operate.card, operate.order, operate.huType);
                AYMJPlayerCards from = match.getPlayerCardIndex<AYMJPlayerCards>(operate.from);
                from.updateDisableLastSign(MJCard.sign(operate.card, MJCard.SIGN_HU));
                panel.refreshDisAbleView(operate.from);
                SoundManager.playMJHu(Room.room.players[operate.index].player.sex);
            }
            else if (StatusKit.hasStatus(operate.huType, MJMatchHuOperate.HU_KONG_OTHER) &&
                     StatusKit.hasStatus(operate.huType, MJMatchHuOperate.HU_SELF)) //点杠花
            {
                playerCards.hu(operate.card, operate.order, operate.huType);
                SoundManager.playMJSelfHu(Room.room.players[operate.index].player.sex);
            }
            else if (StatusKit.hasStatus(operate.huType, MJMatchHuOperate.HU_KONG_SELF)) //自杠花
            {
                playerCards.hu(operate.card, operate.order, operate.huType);
                SoundManager.playMJSelfHu(Room.room.players[operate.index].player.sex);
            }
            else if (StatusKit.hasStatus(operate.huType, MJMatchHuOperate.HU_TIAN) &&
                     StatusKit.hasStatus(operate.huType, MJMatchHuOperate.HU_SELF)) //天胡
            {
                playerCards.hu(operate.card, operate.order, operate.huType);
                //播放音效
                SoundManager.playMJSelfHu(Room.room.players[operate.index].player.sex);
            }
            else if (StatusKit.hasStatus(operate.huType, MJMatchHuOperate.HU_DI)) //地胡
            {
                playerCards.hu(operate.card, operate.order, operate.huType);
                AYMJPlayerCards from = match.getPlayerCardIndex<AYMJPlayerCards>(operate.from);
                from.updateDisableLastSign(MJCard.sign(operate.card, MJCard.SIGN_HU));
                panel.refreshDisAbleView(operate.from);
                SoundManager.playMJHu(Room.room.players[operate.index].player.sex);
            }
            else if (StatusKit.isStatus(operate.huType, MJMatchHuOperate.HU_HAIDI)) //海底炮
            {
                playerCards.hu(operate.card, operate.order, operate.huType);
                AYMJPlayerCards from = match.getPlayerCardIndex<AYMJPlayerCards>(operate.from);
                from.updateDisableLastSign(MJCard.sign(operate.card, MJCard.SIGN_HU));
                panel.refreshDisAbleView(operate.from);
                SoundManager.playMJHu(Room.room.players[operate.index].player.sex);
            }
            else if (StatusKit.hasStatus(operate.huType, MJMatchHuOperate.HU_HAIDI) &&
                     StatusKit.hasStatus(operate.huType, MJMatchHuOperate.HU_SELF)) //海底胡(自摸)
            {
                playerCards.hu(operate.card, operate.order, operate.huType);
                SoundManager.playMJSelfHu(Room.room.players[operate.index].player.sex);
            }

            panel.refreshHandCard(operate.index, null, false);

            panel.refreshHuVew(operate.index, true, false);

            showOperate();
        }

        private void showOperate()
        {
            operate.playOver();
        }
    }
}
