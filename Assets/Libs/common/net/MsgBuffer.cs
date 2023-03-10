using System;

namespace cambrian.common
{

    public class MsgBuffer : ByteBuffer
    {

        private int loc;

        /** 构建一个默认容量的ByteBuffer */
        public MsgBuffer(int loc)
            : this(loc,CAPACITY)
        {
        }

        /** 构建一个指定容量的ByteBuffer */
        public MsgBuffer(int loc,int capacity):base(loc+capacity)
        {
            this.loc = loc;
            this.top = loc;
            this.offset = loc;
        }

        public override void setOffset(int offset)
        {
            if (offset < loc || offset > this.top) throw new SystemException("setOffset, invalid offset:" + offset);
            this.offset = offset;
        }

        public void setOffsetUncheckLock(int offset)
        {
            base.setOffset(offset);
        }

        public override ByteBuffer clear()
        {
            this.offset = loc;
            this.top = loc;
            return this;
        }
    }
}