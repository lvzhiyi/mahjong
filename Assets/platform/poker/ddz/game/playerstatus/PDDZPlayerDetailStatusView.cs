using System.Collections;
using cambrian.game;
using DragonBones;
using UnityEngine;
using UnityEngine.UI;

namespace platform.poker
{
    public class PDDZPlayerDetailStatusView : PKPlayerDetailStatusView
    {
        protected override void xinit()
        {
            if (transform.Find("cardnum"))
            {
                cardNum = transform.Find("cardnum").GetComponent<UnrealTextField>();
                cardNum.init();
            }
            if (transform.Find("banker"))
            {
                banker = transform.Find("banker").GetComponent<Image>();
            }
            if (transform.Find("jingbaodeng"))
            {
                cardwarning = transform.Find("jingbaodeng").GetComponent<UnityArmatureComponent>();
            }
        }

        protected override void xrefresh()
        {

        }

        public override void setCardNum(int cardnum)
        {
            if (cardNum)
            {
                cardNum.gameObject.SetActive(cardnum > 0);
                cardNum.text = cardnum.ToString();
            }
        }

        public override void showBanker(bool isShow)
        {
            if (banker)
            {
                banker.gameObject.SetActive(isShow);
            }
        }

        public override void showCardWarning(bool isShow)
        {
            if (cardwarning)
            {
                cardwarning.gameObject.SetActive(isShow);
            }
        }

        private IEnumerator delayHide()
        {
            yield return new WaitForSeconds(delayTime);
            cardwarning.gameObject.SetActive(false);
            yield break;
        }
    }
}