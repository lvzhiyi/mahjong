using cambrian.common;

namespace basef.rank
{
    public class GoldRankGroup
    {
		/// <summary>
        ///  排行组类型：富豪榜
        /// </summary>
        public const int RANK_GROUP_WEALTH = 0;
        /// <summary>
        ///  排行组类型：赢
        /// </summary>
        public const int RANK_GROUP_WIN = 1;
        /// <summary>
        /// 排行组类型：输
        /// </summary>
        public const int RANK_GROUP_LOSE = 2;
        /// <summary>
        /// 排行组类型：初级场
        /// </summary>
        public const int RANK_GROUP_LV1 = 3;
        /// <summary>
        /// 排行组类型：中级场
        /// </summary>
        public const int RANK_GROUP_LV2 = 4;
        /// <summary>
        ///  排行组类型：高级场
        /// </summary>
        public const int RANK_GROUP_LV3 = 5;


        public static string getGroupValuleName(int type)
        {
            switch (type)
            {
                case RANK_GROUP_WEALTH:
                    return "拥有金豆：";
                case RANK_GROUP_WIN:
                    return "赢取金豆：";
                case RANK_GROUP_LOSE:
                    return "输掉金豆：";
                case RANK_GROUP_LV1:
                case RANK_GROUP_LV2:
                case RANK_GROUP_LV3:
                    return "玩牌局数：";
                default:
                    return "";
            }
        }

        /** ID */
        public int id;
        /** 组名 */
        public string name;
        /** 包含的排行名字 */
        public string[] rankNames;
        /** 包含的排行类型 */
        public int[] rankTypes;
        public void bytesRead(ByteBuffer data)
        {
            this.id = data.readInt();
            this.name = data.readUTF();
            int len = data.readInt();
            this.rankNames = new string[len];
            this.rankTypes = new int[len];
            for (int i = 0; i < len; i++)
            {
                rankNames[i] = data.readUTF();
                rankTypes[i] = data.readInt();
            }
        }
      

    }
}
