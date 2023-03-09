using cambrian.game;
using UnityEngine;
using UnityEngine.EventSystems;
using XLua;

/// <summary>
/// 鼠标或触屏点击触发事件
/// </summary>

[Hotfix]
public class MouseClickProcess : MonoBehaviour, IPointerClickHandler, IProcess
{
    public T getRoot<T>() where T : UnrealLuaPanel
    {
        return (T)root;
    }

    public enum Sounds
    {
        None,
        Button
    }

    public Sounds sound = Sounds.None;

    UnrealLuaPanel _root;
    /// <summary>
    /// 所属面板
    /// </summary>
    public UnrealLuaPanel root
    {
        get
        {
            if (this._root == null)
            {
                Transform tran = this.transform;
                this._root = tran.GetComponent<UnrealLuaPanel>();
                while (this._root == null)
                {
                    tran = tran.parent;
                    this._root = tran.GetComponent<UnrealLuaPanel>();
                }
            }
            return this._root;
        }
        set { this._root = value; }
    }

    public void OnPointerClick(PointerEventData e)
    {
        if (e.dragging) return;
        if (this.enabled)
        {
            execute();
            if (sound==Sounds.Button)
                SoundManager.playButton();
        }
    }

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
        this.gameObject.SetActive(b);
    }
}