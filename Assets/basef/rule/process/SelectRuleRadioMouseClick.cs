namespace basef.rule
{
    /// <summary>
    /// 选中单选项
    /// </summary>
    public class SelectRuleRadioMouseClick:MouseClickProcess
    {
        protected int index;

        public override void execute()
        {
            index = GetComponent<IdBar>().index_space;
            transform.parent.GetComponent<UnrealRadioList>().selectedIndex(index);
        }
    }
}
