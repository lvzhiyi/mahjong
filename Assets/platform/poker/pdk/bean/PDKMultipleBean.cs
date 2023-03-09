using cambrian.common;

namespace platform.poker
{
    public class PDKMultipleBean : BytesSerializable
    {

        /// <summary> 炸弹倍数 </summary>
        public int boomPoint { get; private set; }

        public PDKMultipleBean()
        {
            boomPoint = Room.room.getRule().getIntAtribute("multipScore")==0 ? 1 : 0;
        }

        public void reset()
        {
            boomPoint = Room.room.getRule().getIntAtribute("multipScore")==0 ? 1 : 0;
        }

        /// <summary> 出炸弹时调用 </summary>
        public void changeBoom(PDKCardInfo info)
        {
            if (Room.room.getRule().getRuleAtribute("Boom") && info.type == PKCardType.TYPE_CARDS_BOMB)
            {
                addPoint(info.master);
            }
            else if (Room.room.getRule().getRuleAtribute("aaaisboom") && info.cards.Length == 3)
            {
                if (info.cards[0] == info.cards[1] &&
                    info.cards[0] == info.cards[2])
                {
                    addPoint(info.master);
                }
            }
        }

        /// <summary> 倍数加分 </summary>
        private void addPoint(int index)
        {
            int boomscore = Room.room.getRule().getIntAtribute("multipScore");
            if (boomscore == 0)
            {
                boomPoint <<= 1;
            }
            else
            {
                int bet=Room.room.roomRule.rule.getBet();
                if (index != PDKMatch.match.mindex)
                {
                    boomPoint -= boomscore*bet;
                }
                else
                {
                    int len = Room.room.players.Length;
                    boomPoint += (len-1)* boomscore*bet;
                } 
            }
        }

        public override void bytesRead(ByteBuffer data)
        {
            boomPoint = data.readInt();
        }

        public void clone(int boomPoint)
        {
            this.boomPoint = boomPoint;
        }
    }
}
