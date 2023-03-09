using cambrian.common;
using cambrian.game;
using scene.game;
using UnityEngine;

namespace platform.spotred.room
{
    public class DeskTopManager
    {
        public static DeskTopManager instance = new DeskTopManager();
        /// <summary>
        /// 舒适黄,奢华绿,木纹黄,清新绿
        /// </summary>
        public const int SHU_SHI_YELLOW = 1, SHE_HUA_GREEN = 2, MU_WEN_YELLOW = 3, QING_XIN_GREEN = 4;

        public static int[] types = new int[] { SHU_SHI_YELLOW, SHE_HUA_GREEN, MU_WEN_YELLOW, QING_XIN_GREEN };

        /// <summary>
        /// 设置桌布
        /// </summary>
        /// <param name="type"></param>
        public void setDeskTopStyle(int type)
        {
            ByteBuffer data = new ByteBuffer();
            data.writeInt(type);
            FileCachesManager.savePath(CacheLocalPath.ROOM_DESKBG_PATH, true, data);
        }

        /// <summary>
        /// 获取偷牌背面牌
        /// </summary>
        /// <returns></returns>
        public Sprite getBackCardStyle()
        {
            return CacheManager.desktop[9];
        }

        /// <summary>
        /// 获取滑牌阶段背面牌
        /// </summary>
        /// <returns></returns>
        public Sprite getSlipCardStyle()
        {
            return CacheManager.desktop[11];
        }
    }
}
