using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace cambrian.unreal
{
    public class ScrollRectContainer:ScrollRect
    {
        public override void StopMovement()
        {
            base.StopMovement();
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
        }

        public override void OnScroll(PointerEventData data)
        {
            base.OnScroll(data);
        }
    }
}
