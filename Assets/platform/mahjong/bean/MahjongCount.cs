using cambrian.common;

namespace platform.mahjong
{
    public class MahjongCount: BytesSerializable
    {
        /// <summary> 接炮次数 </summary>
        public int huTimes;
        /// <summary> 自摸次数 </summary>
        public int huselfTimes;
        /// <summary> 点炮次数 </summary>
        public int dianpaoTimes;
        /// <summary> 明杠次数（包含补杠） </summary>
        public int kongPubTimes;
        /// <summary> 暗杠次数 </summary>
        public int kongPriTimes;
        /// <summary> 查大叫次数 </summary>
        public int checkReady;

        public override void bytesRead(ByteBuffer data)
        {
            this.huTimes = data.readInt();
            this.huselfTimes = data.readInt();
            this.dianpaoTimes = data.readInt();
            this.kongPubTimes = data.readInt();
            this.kongPriTimes = data.readInt();
            this.checkReady = data.readInt();
        }
    }
}
