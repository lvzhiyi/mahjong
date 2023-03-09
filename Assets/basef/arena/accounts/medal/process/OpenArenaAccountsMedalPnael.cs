using basef.player;
using cambrian.common;

namespace basef.arena
{
    /// <summary> 打开奖章明细 面板 </summary>
    public class OpenArenaAccountsMedalPnael : MouseClickProcess
    {
        ArenaAccountsMedalPanel panel;

        public override void execute()
        {
            if (panel == null) panel = UnrealRoot.root.getDisplayObject<ArenaAccountsMedalPanel>();
            int type = panel.getType();
            panel.userid = Player.player.userid;
            CommandManager.addCommand(
                new GetArenaAccountsMedalListCommand(panel.userid,type,0),back);
        }

        private void back(object obj)
        {
            if (obj == null) return;
            panel.setData((ArrayList<ArenaMedalBill>)obj,Player.player.medal);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
