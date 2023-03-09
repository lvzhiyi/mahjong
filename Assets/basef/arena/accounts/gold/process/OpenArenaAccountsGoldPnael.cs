using basef.player;
using cambrian.common;

namespace basef.arena
{
    /// <summary> 打开金豆明细 面板 </summary>
    public class OpenArenaAccountsGoldPnael : MouseClickProcess
    {
        ArenaAccountsGoldPanel panel;

        public override void execute()
        {
            if (panel == null) panel = UnrealRoot.root.getDisplayObject<ArenaAccountsGoldPanel>();
            int type = panel.getType();
            bool checkGoldminus = panel.getCheckGoldminus();
            CommandManager.addCommand(
                new GetArenaAccountsGoldListCommand(Player.player.userid,type,checkGoldminus,0),back);
        }

        private void back(object obj)
        {
            if (obj == null) return;

            panel.setInitData((ArrayList<ArenaAccountsGoldContentData>)obj,Player.player.userid,Arena.arena.getMember().getArenaGold());
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
