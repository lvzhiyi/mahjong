using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 规则对应的游戏面板
    /// </summary>
    public class GamePanelData:Sample
    {
        static GamePanelData gamedata;

        public static GamePanelData getInstance()
        {
            if (gamedata == null)
            {
                gamedata = (GamePanelData)Sample.factory.newSample(2);
            }
            return gamedata;
        }


        /// <summary>
        /// 绵阳0，安岳1
        /// </summary>
        public const int MY_GAME = 0, AY_GAME = 1;

        public int[][] rulesid;

        public int getGamePanel(int rule)
        {
            for (int i=0; i<rulesid.Length;i++)
            {
                for (int j=0; j<rulesid[i].Length;j++)
                {
                    if (rulesid[i][j] == rule)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
    }
}
