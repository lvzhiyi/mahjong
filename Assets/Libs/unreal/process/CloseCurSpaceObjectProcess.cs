using XLua;

[Hotfix]
public class CloseCurSpaceObjectProcess : MouseClickProcess
{
    public override void execute()
    {
        this.transform.gameObject.SetActive(false);
    }
}

