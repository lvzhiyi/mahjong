using cambrian.common;
using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 全部换完操作
    /// </summary>
    public class MJMatchAllReplaceOperate : MJBaseOperate
    {
        /// <summary>
        /// 换牌方式
        /// </summary>
        public int replaceMode;
        /// <summary>
        /// 换的牌
        /// </summary>
        public int[][] cards;

        public MJMatchAllReplaceOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex, isreplay)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            replaceMode = data.readInt();
            int len = data.readInt();
            cards = new int[len][];
            for (int i=0;i<len;i++)
            {
                cards[i] = new int[data.readInt()];
                for (int j=0;j< cards[i].Length; j++)
                {
                    cards[i][j]= data.readInt();
                }
            }
        }
    }
}
