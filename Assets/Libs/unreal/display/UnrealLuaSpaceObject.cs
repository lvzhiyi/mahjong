using scene.game;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class UnrealLuaSpaceObject : UnrealIndexSpaceObject
{

    public LuaTable LuaTable;

    Transform spaceObject;
    /// <summary>
    /// 深度
    /// </summary>
    [HideInInspector] public int SiblingIndex;

    /// <summary>
    /// 加载lua脚本路径
    /// </summary>
    public string luaScriptPath;
    /// <summary>
    /// 存取数据
    /// </summary>
    private Hashtable table = new Hashtable();
    public void setSpaceData(string name, object obj)
    {
        if (table[name] != null)
            table[name] = obj;
        else
        {
            table.Add(name, obj);
        }
    }

    public object getSpaceData(string name)
    {
        return table[name];
    }

    public T getRoot<T>() where T : UnrealLuaPanel
    {
        return (T)root;
    }

    UnrealLuaPanel _root;

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

    //------------------------------------------------
    private LuaTable scriptEnv;

    private Action luaRefresh;

    private Action luaUpdate;

    private Action luaRelayout;

    private Action luaOnDestroy;

    public Injection[] injections;

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

        string path =CacheLocalPath.AB_RESROUCE_PATH + SpotRedRoot.roots.regionModule.region.module.getAbCfgModule().getPropertyValue("versionpath") + luaScriptPath;

        if (File.Exists(path))
        {
            string str = File.ReadAllText(path);
            LuaUtil.luaEnv.DoString(str, this.name, scriptEnv);
            Action luaInit = scriptEnv.Get<Action>("init");
            scriptEnv.Get("refresh", out luaRefresh);
            scriptEnv.Get("relayout", out luaRelayout);
            scriptEnv.Get("onDestroy", out luaOnDestroy);
            scriptEnv.Get("update", out luaUpdate);

            if (luaInit != null)
            {
                luaInit();
            }
        }
    }

    public sealed override void init()
    {
        this.SiblingIndex = this.transform.GetSiblingIndex();

        if (!("".Equals(luaScriptPath)) && luaScriptPath != null)
        {
            base.init();
            luaSettingInit();
        }
        else
        {
            xinit();
        }
       
    }

    protected virtual void xinit()
    {
        this.spaceObject = this.transform;
        base.init();
    }


    public sealed override void refresh()
    {
        if (luaRefresh != null)
        {
            base.refresh();
            luaRefresh();
        }
        else
        {
            xrefresh();
        }
        
    }

    protected virtual void xrefresh()
    {
        base.refresh();
    }


    public sealed override void update()
    {
        if (luaUpdate != null)
            luaUpdate();
        else
            xupdate();
    }

    protected virtual void xupdate()
    {
       base.update();
    }


    public override void relayout()
    {
        if (luaRelayout != null)
            luaRelayout();
        else
            xrelayout();
    }

    protected virtual void xrelayout()
    {
       
    }

    /// <summary>
    /// 设置深度
    /// </summary>
    public void setSlibingIndex(int index)
    {
        this.transform.SetSiblingIndex(index);
    }

    /// <summary>
    /// 重置深度
    /// </summary>
    public void resetSlibingIndex()
    {
        this.transform.SetSiblingIndex(SiblingIndex);
    }

    public override void OnDestroy()
    {
        if (luaOnDestroy != null)
            luaOnDestroy();

        if (scriptEnv != null)
        {
            Action luaInit = scriptEnv.Get<Action>("init");
            luaInit = null;
            scriptEnv.Set("init", luaInit);
            luaInit = scriptEnv.Get<Action>("refresh");
            luaInit = null;
            scriptEnv.Set("refresh", luaInit);
            luaInit = scriptEnv.Get<Action>("relayout");
            luaInit = null;
            scriptEnv.Set("relayout", luaInit);
           
            luaInit = scriptEnv.Get<Action>("onDestroy");
            luaInit = null;
            scriptEnv.Set("onDestroy", luaInit);
            luaInit = scriptEnv.Get<Action>("update");
            luaInit = null;
            scriptEnv.Set("update", luaInit);


            luaOnDestroy = null;
            luaUpdate = null;
            luaRefresh = null;
            luaRelayout = null;


            scriptEnv.Dispose();
            scriptEnv = null;
        }
    }
}
