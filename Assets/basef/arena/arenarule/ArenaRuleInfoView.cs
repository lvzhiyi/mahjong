using cambrian.common;
using System.Text;

namespace basef.arena
{
    public class ArenaRuleInfoView:UnrealLuaSpaceObject
    {
        private UnrealTextField rulename;

        private UnrealTextField wfname;

        private UnrealTextField wf;

        private UnrealTextField yaoqiu;

        private ArenaLockRule lockRule;

        protected override void xinit()
        {
            rulename = transform.Find("rulename").GetComponent<UnrealTextField>();
            wfname = transform.Find("wfname").GetComponent<UnrealTextField>();
            wf = transform.Find("wf").GetComponent<UnrealTextField>();
            yaoqiu = transform.Find("yaoqiu").GetComponent<UnrealTextField>();
        }

        public void setArenaLockRule(ArenaLockRule lockRule)
        {
            this.lockRule = lockRule;
        }

        protected override void xrefresh()
        {
            rulename.text = lockRule.rule.name;
            if (lockRule.name == null || "".Equals(lockRule.name))
                wfname.text = lockRule.rule.name;
            else
                wfname.text = lockRule.name;
            StringBuilder buidBuilder = new StringBuilder();
            buidBuilder.Append("积分≥").Append(lockRule.getLimitGold()).Append(",").Append("底分").Append(MathKit.getRound2LongStr(lockRule.rule.getBet())).Append(",低于")
                .Append(lockRule.getDisbandThreshold()).Append("积分自动解散,奖励低于").Append(lockRule.getThReshold()).Append("不返,可负积分:")
                .Append(lockRule.getOverDraft());
            yaoqiu.text = buidBuilder.ToString();
            string[] rulesinfo = lockRule.rule.getRuleInfo();
            buidBuilder = new StringBuilder();
            for (int i = 0; i < rulesinfo.Length; i++)
            {
                buidBuilder.Append(rulesinfo[i]).Append(",");
            }

            if (lockRule.rule.getTrusteeship() != -1)
            {
                buidBuilder.Append("有托管,");
                int time = (int)(lockRule.rule.getTrusteeTime() / TimeKit.SECOND_MILLS);
                if (time >= TimeKit.MIN_MILLS)
                {
                    time /= (int)TimeKit.MIN;
                    buidBuilder.Append(time + "分钟后," + "自动解散房间");
                }
                else
                {
                    buidBuilder.Append(time + "秒后," + "自动解散房间");
                }
            }
            wf.text = buidBuilder.ToString();
        }
    }
}
