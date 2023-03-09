using cambrian.common;
using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 出牌消息
    /// </summary>
    public class MJMatchDiscardOperate:MJBaseOperate
    {

        public int index;

        public int card;

        public MJMatchDiscardOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex, isreplay)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            index = data.readInt();
            card = data.readInt();
        }

        public override string ToString()
        {
            return "MJMatchDiscardOperate,index=" + index+",card="+card;
        }
    }
}
