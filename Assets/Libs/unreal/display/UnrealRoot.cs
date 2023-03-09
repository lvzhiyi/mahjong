using System;
using cambrian.common;
using scene.game;
using UnityEngine;
using XLua;
using Object = UnityEngine.Object;
using Component = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor; // 这个文件在手机上没有，需要使用条件编译
#endif

[Hotfix]
public class UnrealRoot : UnrealContainer
{
    /// <summary> 根节点游戏对象 </summary>
    public static UnrealRoot root;
    
    /// <summary> 当前版本号 </summary>
    public string versions = "0.1.0";

    /// <summary> 相对于1136*640 增加的缩放比例 </summary>
    public float addScaleX { set; get; }

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.runInBackground = true;
        Application.targetFrameRate = 60;
        if(Screen.width>1920||Screen.width/Screen.height>2f)
            Screen.fullScreen = false;
    }

    void Start()
    {
        UnrealCamera unrealCamera = this.GetComponent<UnrealCamera>();
        unrealCamera.resize();
        addScaleX = unrealCamera.getScaleX();
        this.init();
    }

    public override void init()
    {
        root = this;
        if (log.isDebug)
            Debug.Log(log.getMessage(this, string.Concat("Screen.width = ", Screen.width, " , Screen.height = ", Screen.height)));
    }


    public Component getDisplayObject(string clazz)
    {
        UnrealLuaPanel display = (UnrealLuaPanel) this.displays[clazz];

        if (display == null)
        {
            string prefabName = "prefab/" + clazz.Replace('.', '/');
            if (log.isDebug)
                Debug.Log(log.getMessage(this, "prefabName=" + prefabName + ", clazz=" + clazz));

            string _clazz = clazz;
            int index = clazz.LastIndexOf('.');
            if (index >= 0) _clazz = clazz.Substring(index + 1);
            GameObject obj = null;
            if (SpotRedRoot.roots != null)
            {
                obj = SpotRedRoot.roots.regionModule.region.module.loadPrefab(_clazz);
            }

            if (obj == null)
                obj = ModuleManager.manager.loadPrefab(_clazz);

            if (obj == null)
            {
                GameObject loadobj = add(prefabName);

#if UNITY_EDITOR
                if (loadobj == null)
                {
                    loadobj = AssetDatabase.LoadAssetAtPath("Assets/" + prefabName + ".prefab", typeof(GameObject)) as GameObject;
                    loadobj = add(Instantiate(loadobj));
                }
#endif
                display = loadobj.GetComponent<UnrealLuaPanel>();
            }
            else
            {
                obj = Instantiate(obj);
                add(obj);
                display = obj.GetComponent<UnrealLuaPanel>();
            }

            display.gameObject.SetActive(false);
            display.visible = false;
            this.displays[clazz] = display;
            display.init();
        }

        return display;
    }


    public T getDisplayObject<T>() where T : UnrealDisplayObject
    {
        return this.getDisplayObject<T>(typeof(T).FullName);
    }

    public T getDisplayObject<T>(string clazz) where T : UnrealDisplayObject
    {
        T display = (T) this.displays[clazz];

        if (display == null)
        {
            string prefabName = "prefab/" + clazz.Replace('.', '/');
            if (log.isDebug)
                Debug.Log(log.getMessage(this, "prefabName=" + prefabName + ", clazz=" + clazz));

            string _clazz = clazz;
            int index = clazz.LastIndexOf('.');
            if (index >= 0) _clazz = clazz.Substring(index + 1);
            GameObject obj = null;
            if (SpotRedRoot.roots != null)
            {
                obj = SpotRedRoot.roots.regionModule.region.module.loadPrefab(_clazz);
            }

            if (obj == null)
                obj = ModuleManager.manager.loadPrefab(_clazz);

            if (obj == null)
            {
                GameObject loadobj = add(prefabName);

#if UNITY_EDITOR
                if (loadobj == null)
                {
                    loadobj =
                        AssetDatabase.LoadAssetAtPath("Assets/" + prefabName + ".prefab", typeof(GameObject)) as
                            GameObject;
                    loadobj = add(Instantiate(loadobj));
                }
#endif
                display = loadobj.GetComponent<T>();
            }
            else
            {
                obj = Instantiate(obj);
                add(obj);
                display = obj.GetComponent<T>();
            }

            display.gameObject.SetActive(false);
            display.visible = false;
            this.displays[clazz] = display;
            display.init();
        }

        return display;
    }

    /// <summary>
    /// 检查指定界面是否可见
    /// </summary>
    /// <param name="clazz"></param>
    /// <returns></returns>
    public T checkDisplayObject<T>() where T : UnrealDisplayObject
    {
        string clazz = typeof(T).FullName;
        T display = (T) this.displays[clazz];
        if (display == null || !display.getVisible()) return null;
        return display;
    }

    public UnrealLuaPanel CheckDisplayObject(string clazz)
    {
        UnrealLuaPanel display = (UnrealLuaPanel) this.displays[clazz];
        if (display == null || !display.getVisible()) return null;
        return display;
    }

    /// <summary>
    /// 检查指定界面是否存在(之前是打开过的)
    /// </summary>
    /// <param name="clazz"></param>
    /// <returns></returns>
    public T checkDisplayObjectEixst<T>() where T : UnrealDisplayObject
    {
        string clazz = typeof(T).FullName;
        T display = (T) this.displays[clazz];
        if (display == null) return null;
        return display;
    }

    public UnrealLuaPanel CheckDisplayObjectEixst(string clazz)
    {
        UnrealLuaPanel display = (UnrealLuaPanel) this.displays[clazz];
        if (display == null) return null;
        return display;
    }

    public void clearGameObject<T>() where T : UnrealDisplayObject
    {
        T display = (T) this.displays[typeof(T).FullName];
        if (display != null)
        {
            this.displays.Remove(typeof(T).FullName);
            Destroy(display.gameObject);
            Resources.UnloadUnusedAssets();
        }
    }

    public void showPanel<T>() where T : UnrealDisplayObject
    {
        string fullname = typeof(T).FullName;

        ArrayList<UnrealDisplayObject> list = new ArrayList<UnrealDisplayObject>();

        UnrealDisplayObject showPanel = null;

        foreach (string key in this.displays.Keys)
        {
            if (!fullname.Equals(key))
            {
                if (this.displays[key] is UnrealDisplayObject)
                {
                    if (!((UnrealDisplayObject)this.displays[key]).isShow1)
                        list.add((UnrealDisplayObject)this.displays[key]);
                }
            }
            else
            {
                showPanel=(UnrealDisplayObject)this.displays[key];
            }
        }

        if (showPanel != null)
            showPanel.setVisible(true);

        for (int i = 0; i < list.count; i++)
        {
            list.get(i).setVisible(false);
        }

        list = null;
    }

    public void showPanelName(string name)
    {
        ArrayList<UnrealDisplayObject> list = new ArrayList<UnrealDisplayObject>();

        UnrealDisplayObject showPanel = null;
        foreach (string key in this.displays.Keys)
        {
            if (!name.Equals(key))
            {
                if (this.displays[key] is UnrealDisplayObject)
                {
                    if(!((UnrealDisplayObject)this.displays[key]).isShow1)
                        list.add((UnrealDisplayObject)this.displays[key]);
                }
            }
            else
            {
                showPanel = (UnrealDisplayObject)this.displays[key];
            }
        }

        if (showPanel != null)
            showPanel.setVisible(true);

        for (int i = 0; i < list.count; i++)
        {
            list.get(i).setVisible(false);
        }

        list = null;
    }

    public void clearGameObject(string name)
    {
        name = name.Replace("(Clone)", "");
        string key_name = null;
        foreach (string key in this.displays.Keys)
        {
            string[] strs = StringKit.parseStrings(key, '.');
            if (strs.Length > 0 && strs[strs.Length - 1] == name)
            {
                key_name = key;
                break;
            }
        }

        if (key_name != null)
        {
            UnrealDisplayObject display = (UnrealDisplayObject) this.displays[key_name];
            this.displays.Remove(key_name);
            Destroy(display.gameObject);
            Resources.UnloadUnusedAssets();
        }
    }

    public void dispose()
    {
        foreach (string key in this.displays.Keys)
        {
            if (this.displays[key] is UnrealDisplayObject)
            {
                UnrealDisplayObject display = (UnrealDisplayObject) this.displays[key];
                display.OnDestroy();
                Destroy(display.gameObject);
            }
            else
            {
                Destroy((Object)this.displays[key]);
            }
        }

        displays=null;
        Resources.UnloadUnusedAssets();
        AssetBundle.UnloadAllAssetBundles(true);
        GC.Collect();
    }
}