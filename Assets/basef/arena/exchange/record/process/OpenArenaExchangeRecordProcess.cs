using cambrian.common;

namespace basef.arena
{
    /// <summary> 打开兑换记录面板 </summary>
    public class OpenArenaExchangeRecordProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaExchangeRecordPanel panel = UnrealRoot.root.getDisplayObject<ArenaExchangeRecordPanel>();
            panel.setData(setData());
            panel.refresh();
            panel.setCVisible(true);
        }
        
        private int[] medal = new int[] { 1000,1000,2000,3000,5000,100000 };
        private int[] gold = new int[] { 100,100,200,300,500,10000 };
        private int[] status = new int[] { -1,0,0,1,1,1 };

        public ArrayList<ArenaExchangeRecordContentData> setData()
        {
            ArrayList<ArenaExchangeRecordContentData> list = new ArrayList<ArenaExchangeRecordContentData>();
            ArenaExchangeRecordContentData data = null;
            for (int i = 0; i < medal.Length; i++)
            {
                data = new ArenaExchangeRecordContentData();
                data.medal = medal[i];
                data.gold = gold[i];
                data.status = status[i];
                list.add(data);
            }
            return list;
        }
    }
}
