using cambrian.common;

namespace platform.mahjong
{
    public class TingCard: BytesSerializable
    {
        public int fan; //番数
        public int num; //张数
        public int card; //牌

        /// <summary>
        /// 根据不同游戏类型 单独计算倍数
        /// </summary>
        /// <returns></returns>
        public int getMultiple()
        {
            return this.fan;
        }

        public void setFan(int fan)
        {
            this.fan = fan;
        }
    }
}
