using System.Collections;
using cambrian.common;

namespace platform.poker
{
    public class PDDZOperateView : PKOperateView
    {
        protected override void xinit()
        {
            call = GetComponent<UnrealLuaSpaceObject>("call");
            grab = GetComponent<UnrealLuaSpaceObject>("grab");
            jiabei = GetComponent<UnrealLuaSpaceObject>("jiabei");
            only_call = GetComponent<UnrealLuaSpaceObject>("only_call");
            only_Pass = GetComponent<UnrealLuaSpaceObject>("only_Pass");
            show_Hint = GetComponent<UnrealLuaSpaceObject>("show_Hint");
            show_Normal = GetComponent<UnrealLuaSpaceObject>("show_Normal");
            show_Mingpai = GetComponent<UnrealLuaSpaceObject>("show_Mingpai");
            only_Mingpai = GetComponent<UnrealLuaSpaceObject>("only_Mingpai");
            only_ShowCards = GetComponent<UnrealLuaSpaceObject>("only_ShowCards");
            call_Score = GetComponent<DDZProcessCallScoreManager>("call_Score");
            hideOperateView();
        }

        protected override void xrefresh()
        {
            hideOperateView();
        }

        public override void hideOperateView()
        {
            call.setVisible(false);
            grab.setVisible(false);
            jiabei.setVisible(false);
            only_call.setVisible(false);
            only_Pass.setVisible(false);
            show_Hint.setVisible(false);
            show_Normal.setVisible(false);
            call_Score.setVisible(false);
            only_Mingpai.setVisible(false);
            show_Mingpai.setVisible(false);
            only_ShowCards.setVisible(false);
        }

        public override void showOperateView(int operate, bool isDelay = false)
        {
            if (isDelay)
            {
                StartCoroutine("delayShowOperate", operate);
            }
            else showOperateView(operate);
        }

        protected override IEnumerator delayShowOperate(int operate)
        {
            yield return new UnityEngine.WaitForSeconds(0.1f);
            showOperateView(operate);
            yield break;
        }

        protected override void showOperateView(int operate)
        {
            hideOperateView();
            if (StatusKit.isStatus(operate, PKOperateStatus.CAN_JIABEI | PKOperateStatus.CAN_CANCEL))
            {//加倍 不加倍
                jiabei.setVisible(true);
            }
            else if (StatusKit.isStatus(operate, PKOperateStatus.CAN_DISCARD | PKOperateStatus.CAN_MING))
            {//出牌 明牌
                show_Mingpai.setVisible(true);
            }
            else if (StatusKit.isStatus(operate, PKOperateStatus.CAN_DISCARD | PKOperateStatus.CAN_CANCEL))
            {//出牌 要不起
                show_Normal.setVisible(true);
            }
            else if (StatusKit.isStatus(operate, PKOperateStatus.CAN_MING))
            {//明牌
                only_Mingpai.setVisible(true);
            }
            else if (StatusKit.isStatus(operate, PKOperateStatus.CAN_JIAODIZHU | PKOperateStatus.CAN_CANCEL))
            {//叫地主 不叫
                call.setVisible(true);
            }
            else if (StatusKit.isStatus(operate, PKOperateStatus.CAN_JIAODIZHU))
            {//叫地主
                only_call.setVisible(true);
            }
            else if (StatusKit.isStatus(operate, PKOperateStatus.CAN_QIANGDIZHU | PKOperateStatus.CAN_CANCEL))
            {//抢地主 不抢
                grab.setVisible(true);
            }
            else if (StatusKit.isStatus(operate, PKOperateStatus.CAN_CANCEL))
            {//要不起
                only_Pass.setVisible(true);
            }
            else if (StatusKit.isStatus(operate, PKOperateStatus.CAN_DISCARD))
            {//出牌
                only_ShowCards.setVisible(true);
            }
            else if (StatusKit.isStatus(operate, PKOperateStatus.CAN_CALLSCORE | PKOperateStatus.CAN_HINT))
            {//出牌 提示
                show_Hint.setVisible(true);
            }
            else if (StatusKit.isStatus(operate, PKOperateStatus.CAN_CALLSCORE | PKOperateStatus.CAN_CANCEL))
            {//叫分
                call_Score.setData(DDZMatch.match.callScore, true);
                call_Score.setVisible(true);
            }
            else if (StatusKit.isStatus(operate, PKOperateStatus.CAN_CALLSCORE))
            {//叫分 不能取消
                DDZMatch.match.setCallScore(2);
                call_Score.setData(DDZMatch.match.callScore, false);
                call_Score.setVisible(true);
            }
        }
    }
}
