using cambrian.common;
using scene.game;

namespace cambrian.game
{
    /// <summary>
    /// 订单保存(苹果的订单需要保存)
    /// </summary>
    public class PayOrderStorage
    {
        /// <summary>
        /// 点单号
        /// </summary>
        public static long orderid;

        /// <summary>
        /// 保存IOS订单
        /// </summary>
        public static void saveIOSOrder(string iosorder)
        {
            ByteBuffer data = new ByteBuffer();
            data.writeLong(orderid);
            data.writeUTF(iosorder);
            FileCachesManager.savePathData(CacheLocalPath.PAY_ORDER_STORAGE_PATH, true,data);
        }

        /// <summary>
        /// 保存IOS订单
        /// </summary>
        public static void saveAndroidOrder()
        {
            ByteBuffer data = new ByteBuffer();
            data.writeLong(orderid);
            FileCachesManager.savePathData(CacheLocalPath.PAY_ORDER_STORAGE_PATH, true, data);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        public static void deleteOrder()
        {
            FileCachesManager.deletePathData(CacheLocalPath.PAY_ORDER_STORAGE_PATH);
        }

        /// <summary>
        /// 获取订单号
        /// </summary>
        public static ByteBuffer getOrder()
        {
            ByteBuffer data= FileCachesManager.loadPathData(CacheLocalPath.PAY_ORDER_STORAGE_PATH, true);
            if (data == null) return null;
            return data;
        }

    }
}
