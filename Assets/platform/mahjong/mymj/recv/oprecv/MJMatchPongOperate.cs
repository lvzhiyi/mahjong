using cambrian.common;
using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 麻将碰牌
    /// </summary>
    public class MJMatchPongOperate:MJBaseOperate
    {
        /// <summary>
        /// 谁碰
        /// </summary>
        public int index;

        public int card;
        /// <summary>
        /// 谁打的牌
        /// </summary>
        public int from;

        public MJMatchPongOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex, isreplay)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            index = data.readInt();
            from = data.readInt();
            card = data.readInt();
        }

        public override string ToString()
        {
            return "index="+index+",from="+from+",card="+MJCard.getName(card);
        }
    }
}
