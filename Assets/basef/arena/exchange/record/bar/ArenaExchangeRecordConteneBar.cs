using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    public class ArenaExchangeRecordConteneBar : ScrollBar
    {
        ArenaExchangeRecordContentData data;

        /// <summary> 兑换奖章数量 </summary>
        private UnrealTextField medal;

        /// <summary> 消耗金豆数量 </summary>
        private UnrealTextField gold;

        /// <summary> 兑换状态 </summary>
        private UnrealTextField status;

        /// <summary> 兑换时间 </summary>
        private UnrealTextField etime;

        protected override void xinit()
        {
            medal = this.transform.Find("title_1").GetComponent<UnrealTextField>();
            medal.init();

            gold = this.transform.Find("title_2").GetComponent<UnrealTextField>();
            gold.init();

            status = this.transform.Find("title_3").GetComponent<UnrealTextField>();
            status.init();

            etime = this.transform.Find("title_4").GetComponent<UnrealTextField>();
            etime.init();
        }

        protected override void xrefresh()
        {
            if (this.data == null) return;
            medal.text = data.medal.ToString();
            gold.text = data.gold.ToString();
            setStatus();
            setTime();
        }

        private void setStatus()
        {
            switch (data.status)
            {
                case ArenaExchangeRecord.Underway:
                    status.text = ArenaExchangeRecord.UnderwayText;
                    break;
                case ArenaExchangeRecord.Fail:
                    status.text = ArenaExchangeRecord.FailText;
                    break;
                case ArenaExchangeRecord.Succeed:
                    status.text = ArenaExchangeRecord.SucceedText;
                    break;
            }
        }



        private void setTime()
        {
            long time = TimeKit.currentTimeMillis;
            string t= TimeKit.format(time,"yyyy-MM-dd hh:mm:ss");
            etime.text = t;
        }

        public void setData(ArenaExchangeRecordContentData data)
        {
            this.data = data;
        }
    }
}
