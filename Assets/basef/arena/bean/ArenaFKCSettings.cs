using basef.rule;
using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 房卡场设置
    /// </summary>
    public class ArenaFKCSettings : BytesSerializable
    {
        int version = 0;

        /// <summary>
        /// 满桌显示
        /// </summary>
        public int fulltable;
        /// <summary>
        /// 防暂离超时时间
        /// </summary>
        public int leavetime;

        /** 竞赛场允许使用的规则：为空不能建房 */
        ArrayList<ArenaLockRule> rules;

        /// <summary>
        /// 只按照sid来划分
        /// </summary>
        ArrayList<Rule> ruleType;

        /// <summary>
        /// 自由桌是否显示
        /// </summary>
        public bool freedomEnable;
        /// <summary>
        /// 显示的规则
        /// </summary>
        public int[] showRules;

        /** 获取锁定规则数量 */
        public int getRuleCount()
        {
            return rules.count;
        }

        public void setRues(ArrayList<ArenaLockRule> rules)
        {
            this.rules = rules;
        }

        public ArenaLockRule getRenaLockRule(int id)
        {
            for (int i = 0; i < rules.count; i++)
            {
                if (rules.get(i).uuid == id)
                {
                    return rules.get(i);
                }
            }

            return null;
        }

        public ArrayList<ArenaLockRule> getRules()
        {
            return rules;
        }

        private void splitRule()
        {
            ArrayList<Rule> list = new ArrayList<Rule>();
            for (int i = 0; i < rules.count; i++)
            {
                if (list.count == 0)
                {
                    list.add(rules.get(i).rule);
                }
                else
                {
                    bool b = false;
                    for (int j = 0; j < list.count; j++)
                    {
                        if (list.get(j).sid == rules.get(i).rule.sid)
                        {
                            b = true;
                            break;
                        }
                    }

                    if (!b)
                    {
                        list.add(rules.get(i).rule);
                    }
                }
            }
            ruleType = list;
        }

        public ArrayList<ArenaLockRule> getAssignRules(int sid)
        {
            ArrayList<ArenaLockRule> list = new ArrayList<ArenaLockRule>();
            // 自由桌功能弃用
            //if (freedomEnable && rules.count > 0)
            //{
            //    list.add(rules.get(0));
            //}
            for (int i = 0; i < rules.count; i++)
            {
                for (int j = 0; j < showRules.Length; j++)
                {
                    if (rules.get(i).uuid == showRules[j])
                        list.add(rules.get(i));
                }
            }
            return list;
        }

        /// <summary>
        /// 获取竞赛场规则的种类(按照规则sid，来划分)
        /// </summary>
        /// <returns></returns>
        public ArrayList<Rule> getArenaLockRuleType()
        {
            return ruleType;
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.version = data.readInt();
            data.readInt();
            this.fulltable = data.readInt();
            this.leavetime = data.readInt();
            this.rules = new ArrayList<ArenaLockRule>();
            int len = data.readInt();
            ArenaLockRule rule;
            for (int i = 0; i < len; i++)
            {
                rule = new ArenaLockRule();
                rule.bytesRead(data);
                this.rules.add(rule);
            }
            freedomEnable = data.readBoolean();
            len = data.readInt();
            showRules = new int[len];
            for (int i = 0; i < len; i++)
            {
                showRules[i] = data.readInt();
            }
            splitRule();
        }
    }
}
