using System.Collections;
using DragonBones;
using UnityEngine;

namespace platform.poker
{
    public class PPDKPlayerDetailStatusView : PKPlayerDetailStatusView
    {
        protected override void xinit()
        {
            if (transform.Find("cardnum"))
            {
                cardNum = transform.Find("cardnum").GetComponent<UnrealTextField>();
                cardNum.init();
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
