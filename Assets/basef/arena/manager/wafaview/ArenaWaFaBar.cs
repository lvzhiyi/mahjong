using System.Text;
using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 玩法bar
    /// </summary>
    public class ArenaWaFaBar : ScrollBar
    {
        /// <summary>
        /// 玩法名称
        /// </summary>
        private UnrealTextField wanfaName;

        /// <summary>
        /// 玩法名称
        /// </summary>
        //private UnrealTextField rulename;

        /// <summary>
        /// 防成谜设置
        /// </summary>
        //private UnrealTextField indulgence;

        /// <summary>
        /// 玩法
        /// </summary>
        private UnrealTextField wafa;

        public ArenaLockRule lockRule;


        protected override void xinit()
        {
            wanfaName = transform.Find("name").GetComponent<UnrealTextField>();
            wanfaName.init();
            //rulename = transform.Find("rulename").GetComponent<UnrealTextField>();
            //rulename.init();
            //indulgence = transform.Find("indulgence").GetComponent<UnrealTextField>();
            //indulgence.init();
            wafa = transform.Find("wafa").GetComponent<UnrealTextField>();
            wafa.init();
        }

        public void setData(ArenaLockRule lockRule)
        {
            this.lockRule = lockRule;
        }


        protected override void xrefresh()
        {
            wanfaName.text = lockRule.name+"("+lockRule.rule.name+")";
            System.Text.StringBuilder buidBuilder = new StringBuilder();
            string[] rulesinfo= lockRule.rule.getRuleInfo();
            for (int i = 0; i < rulesinfo.Length; i++)
            {
                buidBuilder.Append(rulesinfo[i]).Append(",");
            }

            if (lockRule.rule.getTrusteeship() != -1)
            {
                buidBuilder.Append("有托管,");
                int time=(int)(lockRule.rule.getTrusteeTime()/TimeKit.SECOND_MILLS);
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

            buidBuilder.Append("金豆≥").Append(lockRule.getLimitGold()).Append(",").Append("底分").Append(MathKit.getRound2LongStr(lockRule.rule.getBet())).Append(",低于")
                .Append(lockRule.getDisbandThreshold()).Append("金豆自动解散,门票低于").Append(lockRule.getThReshold()).Append("不返,可负金豆:")
                .Append(lockRule.getOverDraft());
            //indulgence.text = buidBuilder.ToString();

            wafa.text = buidBuilder.ToString();
        }
    }
}
