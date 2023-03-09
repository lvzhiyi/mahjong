using cambrian.common;

namespace basef.arena
{
    public class ArenaExchangeCustomBotton : MouseClickProcess
    {
        private UnrealInputTextField text;

        string color = "#FF0000";

        int value;

        public override void execute()
        {
            base.execute();
            text = this.transform.parent.Find("custominput").GetComponent<UnrealInputTextField>();
            if (text.text.Length==0 || text.text.Equals("0")||"".Equals(text.text)) return;
            value= int.Parse(text.text);
            string str = string.Format("是否使用<color={0}>{1}积分</color>兑换<color={0}>{2}奖章</color>", color, value, value / ArenaAddAwardPanel.MEDAL_RATIO);
            Alert.confirmShow(str, back1);
        }

        private void back1(object obj)
        {
            CommandManager.addCommand(new ArenaExchangePrizeCommand(MathKit.getLongEnlarge100(value)), back);
        }
        private void back(object obj)
        {
            if (obj == null) return;

            if (Arena.arena.getMember().isAgent() && (bool)obj)
            {
                Alert.show("奖章兑换成功!!!");
                text.text = "";
            }
            else if ((bool)obj)
            {
                Alert.show("奖章兑换中,请等待上级审批同意!!!");
                text.text = "";
            }
            else
            {
                Alert.show("兑换比赛奖品失败!!!");
                text.text = "";
            }
        }
    }
}