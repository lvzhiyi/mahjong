using basef.rule;
using cambrian.common;

namespace platform.spotred
{
    /// <summary>
    /// 房间规则
    /// </summary>
    public class RoomRule:BytesSerializable
    {
        /// <summary>
        /// 第几局
        /// </summary>
        public int num = -1;

        public Rule rule;

        public bool isOver()
        {
            return (this.num+1) == this.rule.matchCount;
        }

        public string getNowPalyNum()
        {
            return (this.num + 1) + "/" + this.rule.matchCount;
        }

        public void setGameNum(int num)
        {
            this.num = num;
        }

        public int getMatchCount()
        {
            return rule.matchCount;
        }

        public int getNowsPlayNum()
        {
            return num + 1;
        }

        public int getGameNum()
        {
            return this.num;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int sid=data.readInt();
            this.rule = RuleManager.manager.newSample(sid);
            rule.bytesRead(data);
        }

        public override void bytesWrite(ByteBuffer data)
        {
            this.rule.bytesWrite(data);
        }


        public void copy(RoomRule rule)
        {
            this.rule=rule.rule;
        }

        /// <summary>
        /// 获得规则文字说明
        /// </summary>
        /// <param name="isShowMatchCount"></param>
        /// <returns></returns>
        public string getPlayRule(bool isShowMatchCount)
        {
            return rule.getPlayRule(isShowMatchCount);
        }

        public override string ToString()
        {
            return "rule=" + rule;
        }
    }
}
