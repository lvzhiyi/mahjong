using basef.setting;

namespace basef.lobby
{
    class OpenSettingPaneProcess: MouseClickProcess
    {
        public override void execute()
        {
            SettingPanel panel = UnrealRoot.root.getDisplayObject<SettingPanel>();
            panel.refresh();
            panel.setCVisible(true);   
        }
    }
}
