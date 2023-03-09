namespace platform.poker
{
    using cambrian.common;
    using platform.bean;
    using UnityEngine;

    /// <summary> 出牌阶段 </summary>
    public class DDZMShowCardOperate : PKBaseOperate
    {
        public DDZCardInfo cardsinfo;

        public int index;

        public DDZMShowCardOperate(OperateData data) : base(data) { }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
            cardsinfo = new DDZCardInfo(index);
            cardsinfo.showBytesRead(data);
        }
    }
}
