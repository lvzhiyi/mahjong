using scene.game;
using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.regions
{
    public class RegionsView:UnrealLuaSpaceObject
    {
        private ScrollContainer container;

        private Region[] regions;

        /// <summary>
        /// 上一次选中的sid
        /// </summary>
        [HideInInspector] public int select_id;

        protected override void xinit()
        {
            container = GetComponent<ScrollContainer>();
            container.init();
        }

        public void setRegions(Region[] regions)
        {
            this.regions = regions;
        }

        protected override void xrefresh()
        {
            select_id = SpotRedRoot.roots.regionModule.region.id;
            container.updateData(callback);
            container.resize(regions.Length);
            container.resizeDelta();
        }

        public void callback(ScrollBar bar, int index)
        {
            RegionBar regionBar = (RegionBar)bar;
            regionBar.setRegion(regions[index], select_id);
            regionBar.index_space = index;
            regionBar.refresh();
        }

        public void updateBars(int id)
        {
            this.select_id = id;
            for (int i = 0; i < this.container.bars.count; i++)
            {
                RegionBar bar = (RegionBar)this.container.bars.get(i);
                bar.setSelected(id);
                bar.refresh();
            }
        }
    }
}
