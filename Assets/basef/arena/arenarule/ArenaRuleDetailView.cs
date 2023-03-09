using basef.rule;
using cambrian.common;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 规则详细面板
    /// </summary>
    public class ArenaRuleDetailView: RulesView
    {
        /// <summary>
        /// 名称空间
        /// </summary>
        UnrealLuaSpaceObject nicknamespace;
        /// <summary>
        /// 昵称
        /// </summary>
        [HideInInspector] public InputField nickname;
        /// <summary>
        /// 底分空间
        /// </summary>
        UnrealLuaSpaceObject difenspace;
        /// <summary>
        /// 底分
        /// </summary>
        [HideInInspector] public InputField difenscore;

        [HideInInspector] public ArenaLockRule arenaLockRule;
        /// <summary>
        /// 托管局数
        /// </summary>
        UnrealInputTextField tuoguanjushu;
        /// <summary>
        /// 托管解散局
        /// </summary>
        Text tuoguanju;


        protected override void xinit()
        {
            base.xinit();
            scrollRect = transform.Find("content").GetComponent<ScrollRect>();
            container = scrollRect.transform.Find("mask").Find("container");

            nicknamespace = container.Find("name").GetComponent<UnrealLuaSpaceObject>();
            nicknamespace.init();

            nickname = nicknamespace.transform.Find("text").GetComponent<InputField>();

            difenspace = container.Find("difen").GetComponent<UnrealLuaSpaceObject>();
            difenspace.init();

            difenscore = difenspace.transform.Find("text").GetComponent<InputField>();

            tuoguanjushu = transform.Find("content/mask/container/tuoguanjiesan/jushu").GetComponent<UnrealInputTextField>();

            tuoguanjushu.endChange = tuoguanjushu_back;

            tuoguanju = transform.Find("content/mask/container/tuoguanjiesan/title/ju").GetComponent<Text>();



        }

        public void setArenRule(ArenaLockRule arenaLockRule)
        {
            this.arenaLockRule = arenaLockRule;
            rule = arenaLockRule.rule;
            setRule(rule);
        }

        protected override void xrefresh()
        {

            var jiesan = rule.getTrusteejstatus();
            if (jiesan == 2)
                tuoguanjushu.text = rule.getIntAtribute("trusteeReadyTray").ToString();
            else
                tuoguanjushu.text = "2";


            radioList.clear();
            difenscore.text = MathKit.getRound2LongStr(rule.getBet());

            radioList.add(difenspace);

            if (arenaLockRule.name == null || arenaLockRule.name.Length == 0)
                nickname.text = rule.name;
            else
                nickname.text = arenaLockRule.name;
            radioList.add(nicknamespace);


            refreshNormalSingle();
            refreshShow();
            this.moren_ytfh_rule.gameObject.SetActive(false);
            if (rule.getTips().Length > 0)
            {
                this.moren_ytfh_rule.text = "(" + rule.getTips() + ")";
                this.moren_ytfh_rule.gameObject.SetActive(true);
            }
            relayout();
        }

        public void tuoguanjushu_back(string value)
        {
            var maxcount = rule.getIntAtribute("matchCount");

            var v = 0;
            if (value == null || "".Equals(value) || value == "0" || value.Length == 0)
                tuoguanju.text = 2+"";
            else
            {
                 v = StringKit.parseInt(value);
                if (v < 2)
                {
                    v = StringKit.parseInt(tuoguanju.text);
                    if (v == 0)
                    {
                        v = 2;
                    }
                }
                else if (v>=maxcount)
                {
                    v = maxcount;
                }
                tuoguanjushu.text = v.ToString();
            }

            rule.setRuleIntAtributeMethod("trusteeReadyTray",v);
        }

        /// <summary>
        /// 名称输入结束(预制件上配)
        /// </summary>
        /// <param name="value"></param>
        public void nicknameEnd(string value)
        {
            if (value == null || "".Equals(value) || "0".Equals(value))
            {
                if (arenaLockRule.name == null || arenaLockRule.name.Length == 0)
                    nickname.text = rule.name;
                else
                {
                    nickname.text = arenaLockRule.name;
                }
            }
        }

        public void nicknamechange(string value)
        {
        }

        /// <summary>
        /// 底分输入结束(预制件上配)
        /// </summary>
        /// <param name="value"></param>
        public void difenEnd(string value)
        {
            if (value == null || "".Equals(value) || "0".Equals(value))
                difenscore.text = MathKit.getRound2LongStr(rule.getBet());
            double v=StringKit.parseDouble(value);
            if (v < 0)
            {
                Alert.autoShow("底分必须大于0");
                v = MathKit.getRound2Double(rule.getBet());
                if (v == 0)
                    v = MathKit.getRound2Double(rule.getBet());
            }

            difenscore.text = MathKit.getLongEnlarge100(v).ToString();
        }

        public void difenchange(string value)
        {
            
        }

        private void refreshShow()
        {
            refreshApplyexittime();
            refreshTiQianJieSan();
            refreshTrustessShip();
            refreshTrustessShipTime();
            refreshTrustessJiesan();
        }

    }
}
