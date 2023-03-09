using basef.rule;
using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 
    /// </summary>
    public class Settle:BytesSerializable
    {
        /** 结算类型 */
        public int type;
        /** 相关目标：分数收入或支付的玩家座次索引 */
        public int dest;
        /** 牌型：平胡，对对胡， 七对 */
        public int ptype;
        /** 额外加分类型：（如清一色，门清，中张，等额外加分类型，在具体Rule子类中定义） */
        public long atype;
        /** 番数 */
        public int point;
        /** 分数 */
        public int score;

        public virtual string getSettleName(int baseScore,Rule rule)
        {
            return "";
        }

        public virtual string getSettleName1(int baseScore)
        {
            return "";
        }

        public virtual string getSettleName2(int baseScore)
        {
            return "";
        }

        public virtual string detail(CharBuffer buffer)
        {
            return "";
        }

        public virtual string detail1(CharBuffer buffer)
        {
            return "";
        }
    }
}
