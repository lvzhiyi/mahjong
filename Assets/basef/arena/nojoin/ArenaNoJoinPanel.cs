using basef.player;

namespace basef.arena
{
    public class ArenaNoJoinPanel : UnrealLuaPanel
    {
        private UnrealTextScaleButton btn;

        protected override void xinit()
        {
            base.xinit();
            btn = content.Find("creatarena").GetComponent<UnrealTextScaleButton>();
        }

        protected override void xrefresh()
        {
            btn.setVisible(PlayerToken.instance.isPromoter());
        }
    }
}
