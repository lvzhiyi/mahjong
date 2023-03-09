using cambrian.unreal.scroll;
using cambrian.uui;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{

    public class ChangUIRuleBar : ScrollBar
    {
        /// <summary>
        /// 桌子
        /// </summary>
        SpritesList table;
        /// <summary>
        /// 选中图标
        /// </summary>
        Transform selected;
        /// <summary>
        /// 玩法名称
        /// </summary>
        UnrealTextField nameText;
        /// <summary>
        /// 序号
        /// </summary>
        Text number;

        /// <summary>
        /// 板凳
        /// </summary>
        Image bd1;
        Image bd2;
        Image bd3;
        Image bd4;

        /// <summary>
        /// 是否被选中
        /// </summary>
        bool isSelect;
        /// <summary>
        /// 桌布
        /// </summary>
        int deskbg;
        /// <summary>
        /// 赛场规则
        /// </summary>
        [HideInInspector]public ArenaLockRule rule;

        protected override void xinit()
        {
            table = this.transform.Find("table").GetComponent<SpritesList>();
            selected = this.transform.Find("selected");
            nameText = this.transform.Find("name").GetComponent<UnrealTextField>();
            number = this.transform.Find("number").GetComponent<Text>();
            bd1 = this.transform.Find("bd1").GetComponent<Image>();
            bd2 = this.transform.Find("bd2").GetComponent<Image>();
            bd3 = this.transform.Find("bd3").GetComponent<Image>();
            bd4 = this.transform.Find("bd4").GetComponent<Image>();
        }

        public void setData(int ruleid,int deskbg, ArenaLockRule rule)
        {
            this.isSelect = ruleid==rule.uuid;
            this.deskbg = deskbg;
            this.rule = rule;
        }


        protected override void xrefresh()
        {
            selected.gameObject.SetActive(isSelect);
            nameText.text = rule.name;
            number.text = index_space + 1 + "";
            bd2.gameObject.SetActive(false);
            bd3.gameObject.SetActive(false);
            bd4.gameObject.SetActive(false);

            int playernum = rule.rule.playerCount;
            if (playernum == 2)
            {
                bd4.gameObject.SetActive(true);
            }
            else if (playernum == 3)
            {
                bd2.gameObject.SetActive(true);
                bd3.gameObject.SetActive(true);
            }
            else
            {
                bd2.gameObject.SetActive(true);
                bd3.gameObject.SetActive(true);
                bd4.gameObject.SetActive(true);
            }
            table.ShowIndex(deskbg,false);
        }
    }
}
