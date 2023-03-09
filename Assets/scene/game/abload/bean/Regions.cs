using System;

namespace scene.game
{
    /// <summary>
    /// 地区集合
    /// </summary>
    [Serializable]
    public class Regions
    {
        public Region[] region;

        public Region getRegion(int id)
        {
            for (int i = 0; i < region.Length; i++)
            {
                if (region[i].id == id)
                    return region[i];
            }

            return null;
        }
    }
}
