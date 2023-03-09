namespace platform.poker
{
    using cambrian.common;
    using platform.bean;

    public class DDZMMingPaiOperate : PKBaseOperate
    {
        public int index;

        public int[] cards;

        public DDZMMingPaiOperate(OperateData data) : base(data) { }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
            cards = new int[data.readInt()];
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = data.readInt();
            }
        }
    }
}