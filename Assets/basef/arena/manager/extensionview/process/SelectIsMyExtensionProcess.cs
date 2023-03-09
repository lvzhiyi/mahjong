namespace basef.arena
{
    /// <summary>
    /// 选择是否是自己的合伙人
    /// </summary>
    public class SelectIsMyExtensionProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaExtensionNextView nextView= transform.parent.GetComponent<ArenaExtensionNextView>();
            nextView.showselfExtension(!nextView.ismyExtension);
        }
    }
}
