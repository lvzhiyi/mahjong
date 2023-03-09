using cambrian.common;

namespace platform.mahjong
{
    public class MJRain: BytesSerializable
    {
        // 下雨类型定义
        /** 假雨（补杠：如果启用及时雨时不产生雨分，用来记录占位用，前端无用，如果前端读出该类型，应该过滤） */
        public const int RAIN_0 = 0;
        /** 刮风（补杠：其他未胡者处） */
        public const int RAIN_SUP = 1;
        /** 下雨（暗杠：其他未胡者处） */
        public const int RAIN_PRI = 2;
        /** 下雨（明杠：点杠者处） */
        public const int RAIN_PUB = 3;
        /** 转雨（收益转移：杠上炮后转移给胡牌者） */
        public const int RAIN_TUR = 4;

        /** 假雨 */
        public static MJRain RAIN_NO =new MJRain(RAIN_0,0, Single.EMPTY_INT_ARRAY);

        /** 下雨类型名称 */
        public static string[] NAMES=new string[5]{"假雨","刮风（巴杠）","下雨（暗杠）","引雨（明杠）","转雨（杠上炮）"};

	    /** 雨类型 */
	    public int type;
        /** 杠牌者 */
        public int index;
        /** 收雨者  (发生转雨，index！=dest)*/
        public int dest;
        /** 总分值 */
        public int score;
        /** 每个玩家对应需要付得雨分值：0表示不用给 */
        public int[] scores;

        /**  */
        public MJRain()
        {

        }
        /**  */
        public MJRain(int type, int index, int[] scores)
        {
            this.type = type;
            this.index = index;
            this.dest = index;
            this.score = sum(scores);
            this.scores = scores;
        }

        private int sum(int[] scores)
        {
            int count = 0;
            for (int i=0;i<scores.Length;i++)
            {
                count += scores[i];
            }
            return count;
        }

        public string getName(int type)
        {
            return NAMES[type];
        }

        /** 设置收雨者，转雨调用 */
        public void setDest(int dest)
        {
            this.dest = dest;
        }
        public int getScore()
        {
            return score;
        }
        public int getIndexScore(int index)
        {
            return scores[index];
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(type);
            data.writeInt(index);
            data.writeInt(dest);
            data.writeInt(score);
            data.writeInts(scores);
        }

        public override void bytesRead(ByteBuffer data)
        {
            type = data.readInt();
            index = data.readInt();
            dest = data.readInt();
            score = data.readInt();
            scores = data.readInts();
        }
    }
}
