using basef.rule;
using cambrian.common;

namespace basef.teahouse
{
    public class ClubLockRule : BytesSerializable
    {
        /** 序列号 */
        public int uuid;

        /** 包装别名 */
        public string name;

        /** 规则 */
        public Rule rule;

        ///<summary> 门票收入等级 </summary>
        public ArrayList<ScoreLevel> list;

        public ClubLockRule()
        {
            list = new ArrayList<ScoreLevel>();
        }

        public ClubLockRule(string name,Rule rule)
        {
            this.name = name;
            this.rule = rule;
            list = new ArrayList<ScoreLevel>();
        }

        public Rule getRule()
        {
            return rule;
        }

        public void setRule(Rule rule)
        {
            this.rule = rule;
        }

        /// <summary>
        /// 增加门票等级
        /// </summary>
        public void addScoreLevel()
        {
            ScoreLevel scoreLevel = new ScoreLevel();

            long minScore = 0;

            long maxScore = ScoreLevel.NO_UPPER_LIMIT;

            long score = ScoreLevel.COLLECT_TICKET_STEP;
            if (list.count > 0)
            {
                ScoreLevel lastScore = list.getLast();
                if (lastScore.maxScore == ScoreLevel.NO_UPPER_LIMIT)
                {
                    minScore = lastScore.minScore + ScoreLevel.GOLD_STEP;
                    score = lastScore.value + ScoreLevel.COLLECT_TICKET_STEP;
                    lastScore.maxScore = minScore;
                }
            }
            scoreLevel.setData(minScore, maxScore, score);
            list.add(scoreLevel);
        }

        public bool updateScoreLevel(int index, long minScroe, long maxScroe, long score)
        {
            if (list.count - 1 < index || minScroe >= maxScroe) return false;
            ScoreLevel scoreLevel = list.get(index);
            scoreLevel.setData(minScroe, maxScroe, score);
            ScoreLevel curscoreLevel = null;
            for (int i = index + 1; i < list.count; i++)
            {
                curscoreLevel = list.get(i);
                if (i != list.count - 1)
                {
                    if (curscoreLevel.maxScore <= scoreLevel.maxScore)
                        curscoreLevel.setDatas(scoreLevel.maxScore, scoreLevel.maxScore + ScoreLevel.GOLD_STEP);
                    else
                        curscoreLevel.setDatas(scoreLevel.maxScore, curscoreLevel.maxScore);
                }
                else
                    curscoreLevel.setDatas(scoreLevel.maxScore, ScoreLevel.NO_UPPER_LIMIT);

                scoreLevel = curscoreLevel;
            }

            return true;
        }

        public bool delteScoreLevel(int index)
        {
            if (list.count - 1 < index || list.count == 0) return false;
            list.removeAt(index);

            ScoreLevel curscoreLevel = null;
            ScoreLevel lastScoreLevel = null;
            for (int i = 0; i < list.count; i++)
            {
                curscoreLevel = list.get(i);
                if (i == 0 && list.count == 1)
                {
                    curscoreLevel.minScore = 0;
                    curscoreLevel.maxScore = -1;
                }
                else if (i == 0)
                {
                    curscoreLevel.minScore = 0;

                }
                else if (i == list.count - 1)
                {
                    curscoreLevel.setDatas(lastScoreLevel.maxScore, ScoreLevel.NO_UPPER_LIMIT);
                }
                else
                {
                    curscoreLevel.setDatas(lastScoreLevel.maxScore, lastScoreLevel.maxScore + ScoreLevel.GOLD_STEP);
                }

                lastScoreLevel = curscoreLevel;
            }

            return true;
        }

        public override void bytesRead(ByteBuffer data)
        {
            uuid = data.readInt();
            name = data.readUTF();
            rule = RuleManager.manager.newSample(data.readInt());
            rule.bytesRead(data);
            int len = data.readInt();
            ScoreLevel level;
            for (int i = 0; i < len; i++)
            {
                level = new ScoreLevel();
                level.bytesRead(data);
                list.add(level);
            }
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(uuid);
            data.writeUTF(name);
            rule.bytesWrite(data);
            data.writeInt(rule.getBet());
            data.writeInt(list.count);
            for (int i = 0; i < list.count; i++)
            {
                list.get(i).bytesWrite(data);
            }
        }
    }

}
