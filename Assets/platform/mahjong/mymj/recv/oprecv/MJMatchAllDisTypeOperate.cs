using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 所有人定缺完成后
    /// </summary>
    public class MJMatchAllDisTypeOperate:MJBaseOperate
    {
        /// <summary>
        /// 定缺的类型
        /// </summary>
        public int[] distypes;

        public MJMatchAllDisTypeOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex, isreplay)
        {
            
        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            
            distypes = new int[data.readInt()];
            for (int i = 0; i < distypes.Length; i++)
            {
                distypes[i] = data.readInt();
            }
        }
    }
}
