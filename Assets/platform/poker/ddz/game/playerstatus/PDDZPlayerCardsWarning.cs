using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace platform.poker
{
    public class PDDZPlayerCardsWarning : UnrealLuaSpaceObject
    {
        private Image lightbg;

        public Color startValue = Color.white;

        public Color endValue = Color.red;

        public float times = 2f;

        public float scale = 1.2f;

        private void OnEnable()
        {
            if (lightbg == null) lightbg = GetComponent<Image>();
            lightbg.color = startValue;
            lightbg.transform.position.Scale(Vector3.one);
            lightbg.DOColor(endValue, times);
        }

        protected override void xupdate()
        {
            if (lightbg.color == endValue)
            {
                lightbg.DOColor(startValue, times);
                lightbg.transform.DOScale(scale, times);
            }
            if (lightbg.color == startValue)
            {
                lightbg.DOColor(endValue, times);
                lightbg.transform.DOScale(1f, times);
            }
        }
    }
}
