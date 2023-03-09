using cambrian.common;
using cambrian.game;
using scene.game;

namespace platform.mahjong
{
    public class MJSettingManager
    {
        public static int getDeskTop()
        {
            ByteBuffer data = FileCachesManager.loadPath(CacheLocalPath.MJ_ROOM_DESKBG_PATH, true);
            if (data == null)
                return 0;
            int type = data.readInt();
            return type;
        }

        /// <summary>
        /// 设置桌布
        /// </summary>
        /// <param name="type"></param>
        public static void setDeskTopStyle(int type)
        {
            ByteBuffer data = new ByteBuffer();
            data.writeInt(type);
            FileCachesManager.savePath(CacheLocalPath.MJ_ROOM_DESKBG_PATH, true, data);
        }

        public static int getcardImg()
        {
            ByteBuffer data = FileCachesManager.loadPath(CacheLocalPath.MJ_ROOM_DESKBG_PATH+"card1", true);
            if (data == null)
                return 1;
            int type = data.readInt();
            return type;
        }

        /// <summary>
        /// 设置桌布
        /// </summary>
        /// <param name="type"></param>
        public static void setcardImg(int type)
        {
            ByteBuffer data = new ByteBuffer();
            data.writeInt(type);
            FileCachesManager.savePath(CacheLocalPath.MJ_ROOM_DESKBG_PATH+"card1", true, data);
        }

        /// <summary>
        /// 获取麻将牌背景
        /// </summary>
        /// <returns></returns>
        public static int getcardbgImg()
        {
            ByteBuffer data = FileCachesManager.loadPath(CacheLocalPath.MJ_ROOM_DESKBG_PATH + "mjcardbg", true);
            if (data == null)
                return 1;
            return data.readInt();
        }

        /// <summary>
        /// 设置麻将牌背景
        /// </summary>
        /// <param name="type"></param>
        public static void setcardbgImg(int type)
        {
            ByteBuffer data = new ByteBuffer();
            data.writeInt(type);
            FileCachesManager.savePath(CacheLocalPath.MJ_ROOM_DESKBG_PATH + "mjcardbg", true, data);
        }
    }
}
