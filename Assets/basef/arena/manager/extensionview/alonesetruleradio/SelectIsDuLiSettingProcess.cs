using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 选择是否是独立设置
    /// </summary>
    public class SelectIsDuLiSettingProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaAloneSetRuleRadioPanel panel= getRoot<ArenaAloneSetRuleRadioPanel>();
            panel.list.special = !panel.list.special;
            panel.refreshImage();
            panel.refreshSpeicial();
        }
    }
}
