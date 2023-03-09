namespace platform.poker
{
    using cambrian.common;
    using platform.bean;

    /// <summary> 取消操作 </summary>
    public class DDZMCancelOperate : PKBaseOperate
    {
        public int type;

        public int index;

        public DDZMCancelOperate(OperateData data) : base(data) { }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
            type = data.readInt();
        }
    }
}
