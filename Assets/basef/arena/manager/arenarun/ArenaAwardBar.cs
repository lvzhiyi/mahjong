using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaAwardBar:ScrollBar
    {
        public ExchangeEntery entery;

        private Text medalsum;

        private Text tiaojian;

        protected override void xinit()
        {
            medalsum = transform.Find("number").GetComponent<Text>();
            tiaojian = transform.Find("tiaojian").GetComponent<Text>();
        }

        public void setData(ExchangeEntery entery)
        {
            this.entery = entery;
        }

        protected override void xrefresh()
        {
            medalsum.text = (entery.getValue() / ArenaAddAwardPanel.MEDAL_RATIO) + "奖章";
            tiaojian.text = entery.getValue() + "积分";
        }
    }
}
