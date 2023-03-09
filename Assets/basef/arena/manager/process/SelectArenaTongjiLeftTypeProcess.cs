using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 选择管理需要显示的内容
    /// </summary>
    public class SelectArenaTongjiLeftTypeProcess : MouseClickProcess
    {
        public override void execute()
        {
            int type = this.transform.parent.GetComponent<ArenaLeftTypeBar>().index_space;
            ArenaTongjiPanel panel = getRoot<ArenaTongjiPanel>();
            panel.updateSelect(type);
        }
    }
}
