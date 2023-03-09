using basef.rule;
using cambrian.common;
using scene.game;

namespace platform.spotred
{
    public class GameConfig
    {
        private static GameConfig config;

        /// <summary>
        /// 出牌区，下边每行最多9张牌
        /// </summary>
        public static int line_max_bottom=8;
        /// <summary>
        /// 出牌区，上边每行最多9张牌
        /// </summary>
        public static int line_max_top = 8;
        /// <summary>
        /// 出牌区，左边每行最多9张牌
        /// </summary>
        public static int line_max_left = 8;
        /// <summary>
        /// 出牌区，右手边玩家每行最多12张牌
        /// </summary>
        public static int line_max_right = 10;


        public string getPublicKey()
        {
            return "BgIAAACkAABSU0ExAAQAAAEAAQClVY/QDpa4q/aPTP+gcgTphScOyrOfvq4YYYM+AxOrvRr7cJbJVPmLMI2M6EgWedUDLrUQG1mZ1NbmqOzLqhmdM2cl2apT8Ynk8dAIrHWE5zpkbO80HZkFJYpFGehWUZgGR+ra268eRulr//Utq/0SQnUgABr0MDMUP8K5RxZqnw==";
        }

        public static GameConfig getInstance()
        {
            if(config==null)
                config=new GameConfig();
            return config;
        }

        public GameType[] getGameType()
        {
            return SpotRedRoot.roots.regionModule.region.gametype;
        }

        public GameType[] getCloneTypes()
        {
            GameType[] types= SpotRedRoot.roots.regionModule.region.gametype;

            for (int i = 0; i < types.Length; i++)
            {
                types[i] = types[i].clone();
            }

            return types;
        }

        public int[] getRulesSort1(Rule rule,int[] rules)
        {
            if (rule == null) return rules;
            ArrayList<int> list = new ArrayList<int>(rules);
            for (int i = list.count - 1; i >= 0; i--)
            {
                if (list.get(i) == rule.sid)
                {
                    list.removeAt(i);
                    break;
                }
            }
            if (list.count == 0)
                list.add(rule.sid);
            else
                list.addAt(rule.sid, 0);
            return list.toArray();
        }

    }
}
