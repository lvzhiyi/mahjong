using UnityEngine.EventSystems;
using UnityEngine.UI;
using XLua;

namespace cambrian.unreal.scroll
{
    [Hotfix]
    public class CustomSrcollRect:ScrollRect
    {
        public delegate void IsMoveing(bool b);

        public IsMoveing callback;
        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            if(callback!=null)
            callback(true);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            if (callback != null)
                callback(false);
        }

        public void setIsMoveDelegate(IsMoveing callback)
        {
           this.callback = callback;
        }
    }
}
