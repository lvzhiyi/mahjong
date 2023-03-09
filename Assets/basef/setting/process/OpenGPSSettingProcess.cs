using cambrian.game;

namespace basef.setting
{
    public class OpenGPSSettingProcess:MouseClickProcess
    {
        public override void execute()
        {
            WXManager.getInstance().openGPS();
            this.root.setVisible(false);
        }
    }
}
