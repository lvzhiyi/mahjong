using DG.Tweening;
using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJHszUpAndDownView:UnrealLuaSpaceObject
    {
        private Image up;

        private Image down;
        protected override void xinit()
        {
            up = transform.Find("icon1").GetComponent<Image>();
            down = transform.Find("icon2").GetComponent<Image>();
        }

        protected override void xrefresh()
        {
            up.transform.DOLocalMoveY(60,0.8f).SetLoops(-1, LoopType.Yoyo);
            down.transform.DOLocalMoveY(-60, 0.8f).SetLoops(-1, LoopType.Yoyo);
        }


    }
}
