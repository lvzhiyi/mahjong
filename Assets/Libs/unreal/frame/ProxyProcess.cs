using System;
using UnityEngine;
using XLua;

/// <summary>
/// 代理处理器
/// </summary>
[Hotfix]
public class ProxyProcess : MouseClickProcess
{
    public IProcess process;

    public Action<Transform> callback;

    public void setProcess(IProcess process)
    {
        this.process = process;
    }
    public override void execute()
    {
        if (this.process != null)
            this.process.execute();
        if (this.callback != null)
            this.callback(this.transform);
    }
}