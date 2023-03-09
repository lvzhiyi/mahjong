using scene.game;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using XLua;

[Serializable]
    public class Injection
    {
        public string name;
        public GameObject value;
    }

/// <summary>
/// lua面板
/// </summary>
[LuaCallCSharp]
[Hotfix]
public class UnrealLuaPanel : UnrealPanel
{
    LuaTable luaTable_1;

    /// <summary>
    /// 加载lua脚本路径
    /// </summary>
    public string luaScriptPath;

    /// <summary>
    /// 存取数据
    /// </summary>
    private Hashtable table;

    public LuaTable luaTable
    {
        get { return luaTable_1; }
        set { this.luaTable_1 = value; }
    }

    public void initTable()
    {
        if (table == null)
            table = new Hashtable();
    }

    public void setPanelData(object name, object obj)
    {
        if (this.table == null)
            this.table = new Hashtable();
        if (this.table.ContainsKey(name))
            this.table[name] = obj;
        else
            this.table.Add(name, obj);
    }

    private bool isExistKey(object key)
    {
        if (this.table == null || !this.table.ContainsKey(key))
            return false;
        return true;
    }

    public void removePanelData(object name)
    {
        if (isExistKey(name))
            table.Remove(name);
    }

    public object getPanelData(object name)
    {
        if (isExistKey(name))
            return table[name];
        return null;
    }

    public sealed override void init()
    {
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

    //------------------------------------------------
    private LuaTable scriptEnv;

    private Action luaRefresh;

    private Action luaUpdate;


    private Action luaRelayout;

    private Action luaResizeDelta;

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

        string path = CacheLocalPath.AB_RESROUCE_PATH +
                      SpotRedRoot.roots.regionModule.region.module.getAbCfgModule().getPropertyValue("versionpath") +
                      luaScriptPath;
        if (File.Exists(path))
        {
            string str = File.ReadAllText(path);
            LuaUtil.luaEnv.DoString(str, this.name, scriptEnv);
            Action luaInit = scriptEnv.Get<Action>("init");
            scriptEnv.Get("refresh", out luaRefresh);
            scriptEnv.Get("relayout", out luaRelayout);
            scriptEnv.Get("resizeDelta", out luaResizeDelta);
            scriptEnv.Get("onDestroy", out luaOnDestroy);
            scriptEnv.Get("update", out luaUpdate);

            if (luaInit != null)
            {
                luaInit();
            }
        }
    }


    protected virtual void xinit()
    {
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
            xrefresh();
    }

    protected virtual void xrefresh()
    {
        base.refresh();
    }

    public sealed override void update()
    {
        if (luaUpdate != null)
        {
            base.update();
            luaUpdate();
        }
        else
            xupdate();
    }

    protected virtual void xupdate()
    {
        base.update();

    }

    public sealed override void relayout()
    {
        if (luaRelayout != null)
        {
            base.relayout();
            luaRelayout();
        }
        else
            xrelayout();

    }

    protected virtual void xrelayout()
    {
        base.relayout();
    }

    protected sealed override void resizeDelta()
    {
        if (luaResizeDelta != null)
        {
            base.resizeDelta();
            luaResizeDelta();
        }
        else
            xresizeDelta();
    }


    protected virtual void xresizeDelta()
    {
        base.resizeDelta();
    }


    public override void OnDestroy()
    {
        if (luaOnDestroy != null)
            luaOnDestroy();

        UnrealLuaSpaceObject[] spaces = GetComponentsInChildren<UnrealLuaSpaceObject>();
        if (spaces != null)
            for (int i = 0; i < spaces.Length; i++)
            {
                spaces[i].OnDestroy();
            }

        MouseEventProcess[] mouse = GetComponentsInChildren<MouseEventProcess>();
        if (mouse != null)
            for (int i = 0; i < mouse.Length; i++)
            {
                mouse[i].OnDestroy();
            }

        MouseEventClickProcess[] mouseClick = GetComponentsInChildren<MouseEventClickProcess>();
        if (mouseClick != null)
            for (int i = 0; i < mouseClick.Length; i++)
            {
                mouseClick[i].OnDestroy();
            }



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
            luaInit = scriptEnv.Get<Action>("resizeDelta");
            luaInit = null;
            scriptEnv.Set("resizeDelta", luaInit);
            luaInit = scriptEnv.Get<Action>("onDestroy");
            luaInit = null;
            scriptEnv.Set("onDestroy", luaInit);
            luaInit = scriptEnv.Get<Action>("update");
            luaInit = null;
            scriptEnv.Set("update", luaInit);


            luaOnDestroy = null;
            luaUpdate = null;
            luaRefresh = null;
            luaResizeDelta = null;
            luaRelayout = null;


            scriptEnv.Dispose();
            scriptEnv = null;
        }
        Destroy(this);
    }
}

