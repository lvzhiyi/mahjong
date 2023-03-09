using basef.rule;
using cambrian.common;
using cambrian.game;
using System;
using UnityEngine;

namespace scene.game
{
    /// <summary>
    /// 地区模块
    /// </summary>
    public class RegionModule:BytesSerializable
    {
        public string regionname = "region";
        /// <summary>
        /// 所有地区
        /// </summary>
        private Regions regions;
        /// <summary>
        /// 当前所在的地址
        /// </summary>
        public Region region { get; set; }

        /// <summary>
        /// 加载地区配置表
        /// </summary>
        /// <param name="name"></param>
        public void loadRegionCfg(string name)
        {
            regions = null;
            if (name == null) return;
            string text = FileKit.readText(CacheLocalPath.AB_RESROUCE_PATH + name);
            regions = JsonUtility.FromJson<Regions>(text);
            JsonUtility.FromJsonOverwrite(text, regions);
        }

        public void loadCurrentRegions()
        {
            ByteBuffer data = FileCachesManager.loadPath(CacheLocalPath.SELECT_REGION_PATH, false);
            if (data == null)
            {
                SpotRedRoot.isFirstLogin = true;
                region = regions.region[0];
                region.load();
            }
            else
            {
                SpotRedRoot.isFirstLogin = false;
                region = regions.getRegion(data.readInt());
                region.load();
            }

            RuleManager.instance(region.newrulepath);

            ServerInfos.addresses = region.server;
        }

        public Regions getRegions()
        {
            return regions;
        }

        public void save(int id)
        {
            ByteBuffer data=new ByteBuffer();
            data.writeInt(id);
            FileCachesManager.savePath(CacheLocalPath.SELECT_REGION_PATH, false, data);
        }
    }
}
