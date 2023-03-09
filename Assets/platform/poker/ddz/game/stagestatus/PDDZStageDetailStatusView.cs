using cambrian.game;

namespace platform.poker
{
    /// <summary> 斗地主 玩家桌面状态 </summary>
    public class PDDZStageDetailStatusView : PKStageDetailStatusView
    {
        protected override void xinit()
        {
            base.xinit();
        }

        public override void showText(int type, int index)
        {
            switch (type)
            {
                case PKStageStatus.JIAO_DI_ZHU:
                    text.text = "叫地主";
                    type = SoundManager.call_yes;
                    break;
                case PKStageStatus.NO_JIAO_DIZHU:
                    text.text = "不叫";
                    type = SoundManager.call_no;
                    break;
                case PKStageStatus.QIANG_DI_ZHU:
                    text.text = "抢地主";
                    type = SoundManager.grab_yes;
                    break;
                case PKStageStatus.NO_QIANG_DI_ZHU:
                    text.text = "不抢";
                    type = SoundManager.grab_no;
                    break;
                case PKStageStatus.JIA_BEI:
                    text.text = "加倍";
                    type = -1;
                    break;
                case PKStageStatus.NO_JIA_BEI:
                    text.text = "不加倍";
                    type = -1;
                    break;
                case PKStageStatus.PASS:
                    text.text = "要不起";
                    type = SoundManager.buyao;
                    break;
                case PKStageStatus.MING_PAI:
                    text.text = "明牌";
                    type = -1;
                    break;
                case PKStageStatus.CALLSCORE_ONE:
                    text.text = "1分";
                    type = SoundManager.jiaofen_1;
                    DDZMatch.match.SetCallScore(1);
                    break;
                case PKStageStatus.CALLSCORE_TWO:
                    text.text = "2分";
                    type = SoundManager.jiaofen_2;
                    DDZMatch.match.SetCallScore(2);
                    break;
                case PKStageStatus.CALLSCORE_THREE:
                    text.text = "3分";
                    type = SoundManager.jiaofen_3;
                    DDZMatch.match.SetCallScore(3);
                    break;
                default:
                    gameObject.SetActive(false);
                    type = -1;
                    break;
            }
            if (type != -1) SoundManager.playPKOther(type, Room.room.players[index].player.sex);
        }
    }
}
