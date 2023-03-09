namespace platform.poker
{
    using cambrian.game;
    using scene.game;
    using System;
    using System.IO;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using XLua;

    public class MouseLuaEventHandler : MonoBehaviour,
         IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler, IPointerClickHandler
    {
        public enum Sounds { None, Button }

        public Sounds sound = Sounds.None;

        public T getRoot<T>() where T : UnrealLuaPanel { return (T)root; }


        private UnrealLuaPanel _root;
        public UnrealLuaPanel root
        {
            get
            {
                if (_root == null)
                {
                    Transform tran = transform;
                    _root = tran.GetComponent<UnrealLuaPanel>();
                    while (_root == null)
                    {
                        tran = tran.parent;
                        _root = tran.GetComponent<UnrealLuaPanel>();
                    }
                }
                return _root;
            }
            set { _root = value; }
        }

        #region init

        public string luaScriptPath;

        private LuaTable scriptEnv;

        public Injection[] injections;

        private void Awake()
        {
            if ((luaScriptPath != null && luaScriptPath.Length > 0))
            {
                luaSettingInit();
            }
        }

        /// <summary> 脚本设置初始化 </summary>
        private void luaSettingInit()
        {
            scriptEnv = LuaUtil.luaEnv.NewTable();
            LuaTable meta = LuaUtil.luaEnv.NewTable();
            meta.Set("__index", LuaUtil.luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();
            scriptEnv.Set("self", this);
            foreach (var injection in injections)
            {
                scriptEnv.Set(injection.name, injection.value);
            }

            string path = string.Concat(CacheLocalPath.AB_RESROUCE_PATH,
                          SpotRedRoot.roots.regionModule.region.module.getAbCfgModule().getPropertyValue("versionpath"),
                          luaScriptPath);

            if (File.Exists(path))
            {
                string str = File.ReadAllText(path);
                LuaUtil.luaEnv.DoString(str, this.name, scriptEnv);

                scriptEnv.Get("mouseLuaClick", out mouseLuaClick);
                scriptEnv.Get("onPointerEnter", out mouseLuaEnter);
                scriptEnv.Get("mouseLuaDown", out mouseLuaDown);
                scriptEnv.Get("mouseLuaExit", out mouseLuaExit);
                scriptEnv.Get("mouseLuaUp", out mouseLuaUp);
                scriptEnv.Get("mouseLuaDrag", out mouseLuaDrag);
                scriptEnv.Get("mouseLuaEndDrag", out mouseLuaEndDrag);

                scriptEnv.Get("onDestroy", out onLuaDestroy);
                scriptEnv.Get("onEnable", out onLuaEnable);
            }
        }

        #endregion

        #region  onAction

        private Action<PointerEventData> mouseLuaClick;
        private Action<PointerEventData> mouseLuaEnter;
        private Action<PointerEventData> mouseLuaDown;
        private Action<PointerEventData> mouseLuaExit;
        private Action<PointerEventData> mouseLuaUp;
        private Action<PointerEventData> mouseLuaDrag;
        private Action<PointerEventData> mouseLuaEndDrag;

        private Action onLuaEnable; //当物体显示出来的第一帧 执行该方法 重复执行
        private Action onLuaDestroy;

        private void OnEnable()
        {
            if (onLuaEnable != null) onLuaEnable();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.dragging) return;
            if (sound == Sounds.Button) { SoundManager.playButton(); }
            if (enabled)
            {
                mouseClick();
                if (mouseLuaClick != null) mouseLuaClick(eventData);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.dragging) return;
            if (enabled)
            {
                mouseEnter();
                if (mouseLuaEnter != null) mouseLuaEnter(eventData);

            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.dragging) return;
            if (enabled)
            {
                mouseDown();
                if (mouseLuaDown != null) mouseLuaDown(eventData);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.dragging) return;
            if (enabled)
            {
                mouseExit();
                if (mouseLuaExit != null) mouseLuaExit(eventData);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.dragging) return;
            if (enabled)
            {
                mouseUp();
                if (mouseLuaUp != null) mouseLuaUp(eventData);
            }
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (enabled)
            {
                mouseOnDrag();
                if (mouseLuaDrag != null) mouseLuaDrag(eventData);
            }
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            if (enabled)
            {
                mouseOnEndDrag();
                if (mouseLuaEndDrag != null) mouseLuaEndDrag(eventData);
            }
        }

        public virtual void mouseOnEndDrag() { }

        public virtual void mouseOnDrag() { }

        public virtual void mouseExit() { }

        public virtual void mouseClick() { }

        public virtual void mouseDown() { }

        public virtual void mouseUp() { }

        public virtual void mouseEnter() { }

        #endregion

        public virtual void setVisible(bool b) { gameObject.SetActive(b); }


        public virtual void OnDestroy()
        {
            if (onLuaDestroy != null)
                onLuaDestroy();

            if (scriptEnv != null)
            {
                envActionChange<PointerEventData>("mouseLuaClick");
                envActionChange<PointerEventData>("mouseLuaEnter");
                envActionChange<PointerEventData>("mouseLuaDown");
                envActionChange<PointerEventData>("mouseLuaExit");
                envActionChange<PointerEventData>("mouseLuaUp");

                envActionChange<PointerEventData>("mouseLuaDrag");
                envActionChange<PointerEventData>("mouseLuaEndDrag");

                envActionChange("onLuaDestroy");
                envActionChange("onLuaEnable");

                onLuaDestroy = onLuaEnable = null;
                mouseLuaClick = mouseLuaExit = mouseLuaUp = mouseLuaEndDrag = mouseLuaEnter = mouseLuaDown = mouseLuaDrag = null;

                scriptEnv.Dispose();
                scriptEnv = null;
            }
            Destroy(this);
        }

        private Action action = null;

        private void envActionChange(string funcname)
        {
            action = scriptEnv.Get<Action>(funcname);
            action = null;
            scriptEnv.Set(funcname, action);
        }

        private void envActionChange<T>(string funcname)
        {
            var action = scriptEnv.Get<Action<T>>(funcname);
            action = null;
            scriptEnv.Set(funcname, action);
        }

    }
}
