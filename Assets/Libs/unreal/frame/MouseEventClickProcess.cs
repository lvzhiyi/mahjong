using cambrian.game;
using scene.game;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using XLua;

/// <summary>
/// 鼠标或触屏点击触发事件
/// </summary>
[LuaCallCSharp]
[CSharpCallLua]
[Hotfix]
public class MouseEventClickProcess : MonoBehaviour,IPointerClickHandler, IProcess
{
    /// <summary>
    /// 加载lua脚本路径
    /// </summary>
    public string luaScriptPath;

    private LuaTable scriptEnv;


    private Action<PointerEventData> onPointerClick;

    private Action<PointerEventData> onEndDrag;

    private Action luaOnDestroy;

    public Injection[] injections;


    void Awake()
    {
        if ((luaScriptPath != null && luaScriptPath.Length > 0))
        {
            luaSettingInit();
        }
    }


    public T getRoot<T>() where T : UnrealLuaPanel
    {
        return (T) root;
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
        if (enabled)
        {
            if (onPointerClick != null)
            {
                onPointerClick(e);
            }
            else
            {
                execute();
            }

            if (sound == Sounds.Button)
                SoundManager.playButton();
        }
    }


    /// <summary>
    /// 脚本设置初始化
    /// </summary>
    private void luaSettingInit()
    {
        scriptEnv = LuaUtil.luaEnv.NewTable();

        // 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
        LuaTable meta = LuaUtil.luaEnv.NewTable();
        meta.Set("__index", LuaUtil.luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        scriptEnv.Set("self", this);
        foreach (var injection in injections)
        {
            scriptEnv.Set(injection.name, injection.value);
        }

        string path = CacheLocalPath.AB_RESROUCE_PATH +
                      SpotRedRoot.roots.regionModule.region.module.getAbCfgModule().getPropertyValue("versionpath") +
                      luaScriptPath;
        if (File.Exists(path))
        {
            string str = File.ReadAllText(path);
            LuaUtil.luaEnv.DoString(str, this.name, scriptEnv);

            scriptEnv.Get("onPointerClick", out onPointerClick);
            scriptEnv.Get("onEndDrag", out onEndDrag);
            scriptEnv.Get("onDestroy", out luaOnDestroy);
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

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (enabled)
        {
            if (onEndDrag != null)
            {
                onEndDrag(eventData);
            }
        }
    }

    public virtual void OnDestroy()
    {
        if (luaOnDestroy != null)
            luaOnDestroy();

        if (scriptEnv != null)
        {
            Action<PointerEventData> action = scriptEnv.Get<Action<PointerEventData>>("onPointerClick");
            action = null;
            scriptEnv.Set("onPointerClick", action);
            action = scriptEnv.Get<Action<PointerEventData>>("onDestroy");
            action = null;
            scriptEnv.Set("onDestroy", action);
            action = scriptEnv.Get<Action<PointerEventData>>("onEndDrag");
            action = null;
            scriptEnv.Set("onEndDrag", action);

            luaOnDestroy = null;
            onPointerClick = null;
            luaOnDestroy = null;
            onEndDrag = null;


            scriptEnv.Dispose();
            scriptEnv = null;
        }

        Destroy(this);
    }
}