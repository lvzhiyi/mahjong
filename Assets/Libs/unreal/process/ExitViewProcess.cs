

using UnityEngine;
using XLua;

[Hotfix]
public class ExitViewProcess:MouseClickProcess
{
    public Transform trans;

    public override void execute()
    {
        trans.gameObject.SetActive(false);
    }
}

