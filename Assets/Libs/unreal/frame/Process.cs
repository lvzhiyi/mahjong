using UnityEngine;
using XLua;

[Hotfix]
public class Process:IProcess
{
    public virtual void execute()
    {
    }
    public virtual string getTitle()
    {
        return null;
    }
    public virtual Sprite getSprite()
    {
        return null;
    }
    public virtual void setVisible(bool b)
    {
    }
}