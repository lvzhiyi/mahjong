using UnityEngine;
using XLua;

[Hotfix]
public interface IProcess
{
    void execute();
    void setVisible(bool b);
    string getTitle();
    Sprite getSprite();
}