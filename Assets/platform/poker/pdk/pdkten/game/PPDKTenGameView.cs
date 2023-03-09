﻿using System.Collections;
using UnityEngine;

namespace platform.poker
{
    public class PPDKTenGameView : PKGameView
    {
        protected UnrealTextField showhint;

        public float delayTime = 2f;

        protected override void xinit()
        {
            clock = GetComponent<PPDKTenClockView>("clock");

            operate = GetComponent<PPDKOperateView>("operate");

            hand = GetComponent<PPDKAllHandView>("hands");

            dealpoker = GetComponent<PPDKTenDealPokersView>("hands");

            desktop = GetComponent<PPDKTenDesktopView>("desktop");

            status = GetComponent<PPDKPlayerStatusView>("playerstatus");

            stage = GetComponent<PPDKTenStageStatusView>("stagestatus");

            recorder = GetComponent<PPDKRecorderView>("recorder");
                                                                                  
            animator = GetComponent<PPDKAnimationPlayView>("animatorview");

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
            yield return new WaitForSeconds(delayTime);
            showhint.setVisible(false);
            yield break;
        }
    }
}
