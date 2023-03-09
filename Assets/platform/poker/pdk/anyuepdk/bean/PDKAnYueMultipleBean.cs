using cambrian.common;
using UnityEngine;

namespace platform.poker
{
    public class PDKAnYueMultipleBean : BytesSerializable
    {

        /// <summary> 炸弹倍数 </summary>
        public int boomPoint { get; private set; }

        public PDKAnYueMultipleBean()
        {
            boomPoint = Room.room.getRule().getIntAtribute("multipScore")==0 ? 1 : 0;
        }

        public void reset()
        {
            boomPoint = Room.room.getRule().getIntAtribute("multipScore")==0 ? 1 : 0;
        }

        /// <summary> 出炸弹时调用 </summary>
        public void changeBoom(int index)
        {
            addPoint(index);
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
                if (index != PDKAnYueMatch.match.mindex)
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
