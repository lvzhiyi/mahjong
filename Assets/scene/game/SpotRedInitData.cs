using cambrian.common;
using cambrian.game;
using UnityEngine;

namespace scene.game
{
    /// <summary>
    /// 长牌初始化数据
    /// </summary>
    public class SpotRedInitData : MonoBehaviour
    {
        private SpotRedRoot root;

        /// <summary>
        /// 初始化
        /// </summary>
        public void init(SpotRedRoot root)
        {
            this.root = root;
            this.gameObject.AddComponent<Coroutine_Runner>();
            //加载本地版本配置
            VersionsCfg.load();
            ModuleManager.instance();
            loadRegions();

            gameObject.AddComponent<AccessPlatformMessage>(); //接收ios或者Android平台传过来的值
            root.add("prefab/SubCameras");
            //屏幕适配
            if (AdapterManager.isAdapterx())
            {
                UnrealCamera.adaptersX = AdapterManager.getAdapterx();
                GetComponent<UnrealCamera>().resize();
            }

            root.versions = VersionsCfg.cfg.versions.getVersion();
            startLoad();
        }

        private void loadRegions()
        {
            if (root.regionModule == null)
                root.regionModule = new RegionModule();
            ModuleManager.manager.getAbCfgModule().getPropertyValue(root.regionModule.regionname, root.regionModule.loadRegionCfg);
            root.regionModule.loadCurrentRegions();
        }

        private void startLoad()
        {
            Alert.alert = root.getDisplayObject<Alert>();
            Alert.fixedalert = root.getDisplayObject<FixedAlert>();
            Alert.confirmAlert = root.getDisplayObject<Confirm>();
            Alert.fixedshow = root.getDisplayObject<FixedAlertShow>();
            this.gameObject.AddComponent<HttpDataAccessHandler>();
            initLoading();
        }

        private void initLoading()
        {
            SpotRedRoot.dataLoading = root.getDisplayObject<LoadingDataPanel>();
            root.loading = root.getDisplayObject<GameLoadingPanel>();
            root.loading.setPercent(0);
            root.loading.refresh();
            root.loading.setVisible(true);
            SoundManager.load();
            Invoke("loadingPrefab", 0.1f);
        }

        private void loadingPrefab()
        {
            GameObject widget = root.addABbundle("prefab/CardsWidget");
            if (widget != null)
                widget.name = "CardsWidget";
            widget = root.addABbundle("prefab/CardsShowWidget");
            if (widget != null)
                widget.name = "CardsShowWidget";
            widget = root.addABbundle("prefab/Emoji");
            if (widget != null)
                widget.name = "Emoji";
            widget = root.addABbundle("prefab/common");
            if (widget != null)
                widget.name = "common";
            widget = root.addABbundle("prefab/jiesuancard");
            if (widget != null)
                widget.name = "jiesuancard";
            widget = root.addABbundle("prefab/handcards");
            if (widget != null)
                widget.name = "handcards";
            widget = root.addABbundle("prefab/desktop");
            if (widget != null)
                widget.name = "desktop";
            widget = root.addABbundle("prefab/robothead");
            if (widget != null)
                widget.name = "robothead";
            widget = root.addABbundle("prefab/robotheadgirl");
            if (widget != null)
                widget.name = "robotheadgirl";
            widget = root.addABbundle("prefab/mjbg");
            if (widget != null)
                widget.name = "mjbg";
            widget = root.addABbundle("prefab/mjcard1");
            if (widget != null)
                widget.name = "mjcard1";
            widget = root.addABbundle("prefab/pokercard");
            if (widget != null)
                widget.name = "pokercard";
            if (widget != null)
                CacheManager.init();
            root.loading.setPercent(10);
            root.loading.refresh();
            root.selectRegions();
        }
    }
}
