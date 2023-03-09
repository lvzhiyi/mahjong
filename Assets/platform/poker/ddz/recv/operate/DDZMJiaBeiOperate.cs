namespace platform.poker
{
    using cambrian.common;
    using platform.bean;

    public class DDZMJiaBeiOperate : PKBaseOperate
    {
        public int index;

        public DDZMJiaBeiOperate(OperateData data) : base(data) { }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
        }
    }
}
