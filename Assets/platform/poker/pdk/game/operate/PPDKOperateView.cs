using cambrian.common;
using System.Collections;

namespace platform.poker
{
    public class PPDKOperateView : PKOperateView
    {
        protected override void xinit()
        {
            only_Pass = GetComponent<UnrealLuaSpaceObject>("only_Pass");
            show_Normal = GetComponent<UnrealLuaSpaceObject>("show_Normal");
            only_ShowCards = GetComponent<UnrealLuaSpaceObject>("only_ShowCards");
            show_Hint = GetComponent<UnrealLuaSpaceObject>("show_Hint");
            hideOperateView();
        }

        protected override void xrefresh()
        {
            hideOperateView();
        }

        public override void hideOperateView()
        {
            show_Normal.setVisible(false);
            only_Pass.setVisible(false);
            only_ShowCards.setVisible(false);
            show_Hint.setVisible(false);
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

            if (StatusKit.isStatus(operate, PKOperateStatus.CAN_DISCARD | PKOperateStatus.CAN_CANCEL | PKOperateStatus.CAN_HINT))
            {
                show_Normal.setVisible(true);
            }
            else if (StatusKit.isStatus(operate, PKOperateStatus.CAN_CANCEL))
            {
                only_Pass.setVisible(true);
            }
            else if (StatusKit.isStatus(operate, PKOperateStatus.CAN_DISCARD))
            {
                only_ShowCards.setVisible(true);
            }
            else if (StatusKit.isStatus(operate, PKOperateStatus.CAN_DISCARD | PKOperateStatus.CAN_HINT))
            {
                show_Hint.setVisible(true);
            }
        }
    }
}
