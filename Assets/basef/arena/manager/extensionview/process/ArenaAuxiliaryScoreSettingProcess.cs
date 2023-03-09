using cambrian.common;
namespace basef.arena
{
    /// <summary> 辅分设置确认按钮 </summary>
    public class ArenaAuxiliaryScoreSettingProcess : MouseClickProcess
    {
        ArenaAuxiliaryScorePanel panel;
        public override void execute()
        {
            panel = UnrealRoot.root.getDisplayObject<ArenaAuxiliaryScorePanel>();
            long fufeng = panel.getsettingtext();
            long warning = panel.getearlywarningtext();
            if (fufeng < 0 || warning < 0 || warning < fufeng ||(warning>0 && fufeng==0))
            {
                Alert.show("输入不正确。");
            }
            else
            {
                CommandManager.addCommand(new GetAuxiliaryScoreSettingCommand(panel.userid, fufeng, warning), back);
            }
        }

        private void back(object o)
        {
            if (o == null) return;
            panel.refresh();
            panel.setCVisible(false);
            Alert.show("设置成功!");
        }
    }
}
