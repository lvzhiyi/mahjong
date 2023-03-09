using cambrian.common;

namespace basef.arena
{
    public class ArenaNextExtension : BytesSerializable
    {
        /// <summary>
        /// 玩家id
        /// </summary>
        public long userid;

        /// <summary>
        /// 玩家姓名
        /// </summary>
        public string name;

        /// <summary>
        /// 玩家头像
        /// </summary>
        public string head;

        /// <summary>
        /// 所属的上级名称
        /// </summary>
        public string mastername;

        /// <summary>
        /// 所属的上级
        /// </summary>
        public long master;

        /// <summary>
        /// 积分值
        /// </summary>
        private long score;

        /// <summary>
        /// 下级积分总值
        /// </summary>
        private long total_score;

        /// <summary>
        /// 今日游戏次数
        /// </summary>
        public int today_games;

        /// <summary>
        /// 昨日游戏次数
        /// </summary>
        public int yesterday_games;

        /// <summary>
        /// 本周游戏次数
        /// </summary>
        public int week_games;

        /// <summary>
        /// 上周游戏次数
        /// </summary>
        public int lastweek_games;

        /// <summary>
        /// 今日金豆数(门票)
        /// </summary>
        int today_golds;
        /// <summary>
        /// 状态
        /// </summary>
        public int status;

        public double getTodayGolds()
        {
            return MathKit.getRound2Long(today_golds);
        }

        /// <summary>
        /// 昨日金豆数(门票)
        /// </summary>
        int yesterday_golds;

        public double getYesterDayGolds()
        {
            return MathKit.getRound2Long(yesterday_golds);
        }

        /// <summary>
        /// 本周金豆数(门票)
        /// </summary>
        int week_golds;

        public double getWeekGolds()
        {
            return MathKit.getRound2Long(week_golds);
        }

        /// <summary>
        /// 上周金豆数(门票)
        /// </summary>
        int lastweek_golds;

        public double getLastWeekGolds()
        {
            return MathKit.getRound2Long(lastweek_golds);
        }

        public double getTask()
        {
            return MathKit.getRound2Long(score);
        }

        public void setTask(long task)
        {
            this.score = task;
        }

        /// <summary>
        /// 获取总积分
        /// </summary>
        /// <returns></returns>
        public double getTotalScore()
        {
            return MathKit.getRound2Long(score+total_score);
        }

        public void setTotalScore(long total)
        {
            this.total_score = total;
        }

        /// <summary>
        /// 是否有指定状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool hasStatus(int status)
        {
            return StatusKit.hasStatus(this.status, status);
        }

        public override void bytesRead(ByteBuffer data)
        {
            userid = data.readLong();
            name = data.readUTF();
            head = data.readUTF();
            master = data.readLong();
            mastername = data.readUTF();
            score = data.readLong();
            total_score = data.readLong();
            today_golds = data.readInt();
            today_games = data.readInt();
            yesterday_golds = data.readInt();
            yesterday_games = data.readInt();
            week_golds = data.readInt();
            week_games = data.readInt();
            lastweek_golds = data.readInt();
            lastweek_games = data.readInt();
            status = data.readInt();
        }
    }
}
