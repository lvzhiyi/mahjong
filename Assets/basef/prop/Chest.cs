namespace basef.prop
{
    /// <summary>
    /// 类说明：宝箱，道具箱，物品箱，一系列物品组合
    /// </summary>
    public class Chest:Prop
    {
        /** 钥匙道具：0表示无需钥匙 */
        public int key;
        /** 宝箱中包含的道具 */
        public int[][] props;
    }
}
