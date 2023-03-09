namespace platform.poker
{
    using cambrian.common;
    using platform.bean;

    public class PKBaseOperate : BytesSerializable
    {
        public OperateData operateData;

        public int selfIndex;

        public PKBaseOperate(OperateData operateData)
        {
            this.operateData = operateData;
        }

        public override void bytesRead(ByteBuffer data) { }

        public virtual long getWaittime()
        {
            return operateData.waittime;
        }

        public int getSelfOperate()
        {
            return operateData.operates[selfIndex];
        }

        public void playOver()
        {
            operateData.isOver = true;
        }

        public bool isType(int type)
        {
            return operateData.type == type;
        }
    }
}
