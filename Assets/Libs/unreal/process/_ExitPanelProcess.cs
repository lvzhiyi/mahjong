using XLua;
/// <summary>
/// 关闭所属窗口
/// </summary>
[Hotfix]
public class _ExitPanelProcess : Process
{
    protected UnrealLuaPanel root;

    public _ExitPanelProcess(UnrealLuaPanel root)
    {
        this.root = root;
    }

    public override void execute()
    {
        this.root.setVisible(false);
    }
    public override string getTitle()
    {
        return "关闭";
    }
}