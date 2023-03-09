using cambrian.common;

namespace basef.arena
{
    public class OpenArenaExchangeOrderProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaExchangeOrderPanel panel = UnrealRoot.root.getDisplayObject<ArenaExchangeOrderPanel>();
            panel.setData(setData());
            panel.refresh();
            panel.setCVisible(true);
        }

        private string[] names = new string[] { "电风扇","电脑","空调","手机" };
        private int[] price = new int[] { 200 * 99,5000,1200,3400 };
        private int[] status = new int[] { 0,1,2,3 };
        private int[] num = new int[] { 99,1,1,1 };

        public ArrayList<ArenaExchangeOrderContentData> setData()
        {
            ArrayList<ArenaExchangeOrderContentData> list = new ArrayList<ArenaExchangeOrderContentData>();
            ArenaExchangeOrderContentData data = null;
            for (int i = 0; i < status.Length; i++)
            {
                data = new ArenaExchangeOrderContentData();
                data.name = names[i];
                data.price = price[i];
                data.status = status[i];
                data.num = num[i];
                data.imgindex = 0;
                data.time = 0;
                list.add(data);
            }
            return list;
        }
    }
}
