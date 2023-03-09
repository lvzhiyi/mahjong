using cambrian.common;
using UnityEngine;

namespace platform.poker
{
    public class DDZMultipleBean : BytesSerializable
    {
        /* ----------公共倍数---------- */

        /// <summary> 基础分数 </summary>
        public static int baseScore
        {
            get
            {
                if (DDZMatch.match != null && DDZMatch.match.callScore != 0)
                    return DDZMatch.match.callScore;
                else return 1;
            }
            private set { }
        }

        /// <summary> 基础倍数 </summary>
        public int basePoint { get; private set; }

        /// <summary> 明牌倍数 </summary>
        public int mingPoint { get; private set; }

        /// <summary> 炸弹倍数 </summary>
        public int boomPoint { get; private set; }

        /// <summary> 抢地主倍数 </summary>
        public int qiangPoint { get; private set; }

        /// <summary> 春天倍数 </summary>
        public int chunPoint { get; private set; }

        /// <summary> 公共倍数  </summary>
        public int allPoint { get { return /*basePoint **/ mingPoint * boomPoint * qiangPoint * chunPoint; } private set { } }

        /// <summary> 底分加叫分倍数  </summary>
        public int LastPoint { get { return Room.room.getRule().getBet() * baseScore; } private set { } }

        /* ----------个人倍数---------- */
        /// <summary> 地主加倍倍数 </summary>
        public int landlordPoint { get; private set; }

        /// <summary> 规则允许的最大倍数 </summary>
        public int maxPoint { get; private set; }

        /// <summary> 农民加倍倍数 </summary>
        public int[] boorPoint { get; private set; }

        /// <summary> 玩家总倍数 </summary> 个人的最终倍数
        public int[] points { get; private set; }

        public DDZMultipleBean(int playerCount, int baseP, int maxP)
        {
            boorPoint = new int[playerCount];
            points = new int[playerCount];
            basePoint = baseP;
            maxPoint = maxP;
            resetData();
        }

        /// <summary> 设置农民加倍倍数 </summary>
        public void setBoorPoint(int index, int num)
        {
            boorPoint[index] = boorPoint[index] * num;
            if (DDZMatch.match.diZhuIndex == index)
            {
                landlordPoint = boorPoint[index];
            }
        }

        /// <summary> 获取某个玩家的总倍数 </summary>
        public int getSinglePoint(int index)
        {
            int boorPoint = 0;
            if (DDZMatch.match.diZhuIndex == index)
            {
                for (int i = 0; i < DDZMatch.match.playerCount; i++)
                {
                    if (index != i) boorPoint += this.boorPoint[i];
                }
            }
            else boorPoint = this.boorPoint[index];                                                                                                                
            int point = allPoint * landlordPoint * boorPoint;
            return point > maxPoint ? maxPoint : point;
        }

        /// <summary> 出炸弹时调用 </summary>
        public void changeBoom(int[] cards)
        {
            if (DDZCardType.isZhadan(cards) != -1 || DDZCardType.isWangzha(cards) != -1)
            {
                boomPoint <<= 1;
            }
        }

        /// <summary> 明牌操作调用 </summary> num = 倍数
        public void changeMingPai(int num)
        {
            mingPoint *= num;
        }

        /// <summary> 抢地主时调用 </summary>
        public void changeGrabLandlord()
        {
            qiangPoint <<= 1;
        }

        /// <summary> 春天时调用 </summary>
        public void changeChunTian(int chuntian)
        {
            chunPoint = chuntian;
        }

        /// <summary> 重置倍数 </summary>
        public void resetData()
        {
            mingPoint = 1;
            qiangPoint = 1;
            chunPoint = 1;
            boomPoint = 1;
            landlordPoint = 1;
            points = new int[Room.room.getPlayerCount()];
            boorPoint = new int[Room.room.getPlayerCount()];
            for (int i = 0; i < points.Length; i++)
            {
                boorPoint[i] = 1;
                points[i] = 1;
            }
        }

        public override void bytesRead(ByteBuffer data)
        {
            resetData();
            basePoint = data.readInt();
            int callScore = data.readInt();
            mingPoint = data.readInt();
            qiangPoint = data.readInt();
            boomPoint = data.readInt();
            chunPoint = data.readInt();
            int len = data.readInt();
            for (int i = 0; i < len; i++)
            {
                boorPoint[i] = data.readInt();
                points[i] = data.readInt();
            }
            if (DDZMatch.match.diZhuIndex >= 0)
            {
                landlordPoint = boorPoint[DDZMatch.match.diZhuIndex];
            }
            else landlordPoint = 1;
            DDZMatch.match.SetCallScore(callScore);
        }

        public void clone(DDZMultipleBean bean)
        {
            basePoint = bean.basePoint;
            mingPoint = bean.mingPoint;
            qiangPoint = bean.qiangPoint;
            boomPoint = bean.boomPoint;
            chunPoint = bean.chunPoint;
            for (int i = 0; i < boorPoint.Length; i++)
            {
                boorPoint[i] = bean.boorPoint[i];
                points[i] = bean.points[i];
            }
            landlordPoint = bean.landlordPoint;
        }
    }
}
