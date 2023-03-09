using cambrian.common;
using UnityEngine;

namespace scene.game
{
    /// <summary>
    /// 模块管理
    /// </summary>
    public class ModuleManager:BytesSerializable
    {
        public static ModuleManager manager;
        /// <summary>
        /// 脚本修复模块
        /// </summary>
        private LuaLoadModule luaLoadModule;
        /// <summary>
        /// 配置表模块
        /// </summary>
        private ABCfgModule abCfgModule;
        /// <summary>
        /// 资源模块
        /// </summary>
        private ResLoadModule resModule;

        public static void instance()
        {
            if (VersionsCfg.cfg != null && VersionsCfg.cfg.versions.getVer(0) != 13)
                manager = new ModuleManager(VersionsCfg.cfg.resoucePath);
            else
                manager = new ModuleManager();
        }

        public ModuleManager()
        {
            luaLoadModule = new LuaLoadModule();
            resModule = new ResLoadModule();
        }

        public ModuleManager(string cfgPath)
        {
            abCfgModule = new ABCfgModule(cfgPath);
            luaLoadModule = new LuaLoadModule();
            resModule=new ResLoadModule();
            load();
        }

        private void load()
        {
            luaModuleLoad();
            resModuleLoad();
        }

        public ABCfgModule getAbCfgModule()
        {
            return abCfgModule;
        }

        /// <summary>
        /// lua脚本加载,修复bug
        /// </summary>
        private void luaModuleLoad()
        {
            abCfgModule.getPropertyValues(luaLoadModule.getPropertyName(), luaLoadModule.loadBugFix);
        }

        /// <summary>
        /// 必要资源加载
        /// </summary>
        private void resModuleLoad()
        {
            abCfgModule.getPropertyValue(resModule.soundname, resModule.loadSound);

            abCfgModule.getPropertyValues(resModule.imgname, resModule.loadImgRes);

            abCfgModule.getPropertyValue(resModule.prefabPointPath, resModule.loadPrefabPoints);
        }

        /// <summary>
        /// 加载预制件
        /// </summary>
        /// <param name="name">预制件名称</param>
        /// <returns></returns>
        public GameObject loadPrefab(string name)
        {
            return resModule.load(name);
        }

        public AudioClip playAudio(string path)
        {
            return resModule.playSound(path);
        }


        /// <summary>
        /// 保存资源包
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bytes"></param>
        public static void save(string name, byte[] bytes)
        {
            FileKit.write(CacheLocalPath.AB_RESROUCE_PATH + name, bytes);
        }

        public static void delete(string name)
        {
            FileKit.delete(CacheLocalPath.AB_RESROUCE_PATH + name);
        }
    }
}
