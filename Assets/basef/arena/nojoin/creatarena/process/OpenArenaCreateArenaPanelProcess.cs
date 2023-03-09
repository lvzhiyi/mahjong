using basef.player;

namespace basef.arena
{
    /// <summary> 打开创建竞赛场面板 按钮 </summary>
    public class OpenArenaCreateArenaPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            if(!PlayerToken.instance.isPromoter())
            {
                Alert.autoShow("你未开通代理，无法创建休闲场");
                return;
            }
            ArenaCreateArenaPnael panel = UnrealRoot.root.getDisplayObject<ArenaCreateArenaPnael>();
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
