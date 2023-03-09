using System.Collections;
using UnityEngine;

namespace platform.poker
{
    public class PDDZGameView : PKGameView
    {
        protected UnrealTextField showhint;

        protected override void xinit()
        {
            clock = GetComponent<PDDZClockView>("clock");

            operate = GetComponent<PDDZOperateView>("operate");

            hand = GetComponent<PDDZAllHandView>("hands");

            dealpoker = GetComponent<PDDZDealPokersView>("hands");

            desktop = GetComponent<PDDZDesktopView>("desktop");

            status = GetComponent<PDDZPlayerStatusView>("playerstatus");

            stage = GetComponent<PDDZStageStatusView>("stagestatus");

            recorder = GetComponent<PDDZRecorderView>("recorder");

            landlordcards = GetComponent<PKLandlordPokerView>("landcard");

            animator = GetComponent<PDDZAnimationPlayView>("animatorview");

            showhint = GetComponent<UnrealTextField>("showhint");
        }

        protected override void xrefresh()
        {
            clock.refresh();
            operate.refresh();
            hand.refresh();
            desktop.refresh();
            status.refresh();
            stage.refresh();
            recorder.refresh();
            landlordcards.refresh();
            animator.refresh();
        }

        public void showHint(string str)
        {
            showhint.text = str;
            showhint.setVisible(true);
            StartCoroutine("delayhide");
        }

        private IEnumerator delayhide()
        {
            yield return new WaitForSeconds(2);
            showhint.setVisible(false);
            yield break;
        }
    }
}
