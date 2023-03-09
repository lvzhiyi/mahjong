using cambrian.game;
using XLua;

/// <summary>
/// 关闭所属窗口
/// </summary>
[Hotfix]
public class ExitPanelProcess : MouseClickProcess
{
    public override void execute()
    {
        this.root.setVisible(false);

        if (this.root.getLastPanel() != null)
        {
            this.root.getLastPanel().setVisible(true);
            this.root.setLastPanel(null);
        }

        SoundManager.playButton();
       
    }
    public override string getTitle()
    {
        return "关闭";
    }
}