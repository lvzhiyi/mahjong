namespace platform.mahjong
{
    public class AYMJSelfPiaoView: MJSelfPiaoView
    {
        protected override void xinit()
        {
            base.xinit();
        }

        protected override void xrefresh()
        {
            if (piaoview == null) return;
            showPiao();
        }
    }
}
