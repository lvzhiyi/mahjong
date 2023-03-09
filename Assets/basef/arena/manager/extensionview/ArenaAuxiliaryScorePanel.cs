using cambrian.common;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary> 辅分设置界面 </summary>
    public class ArenaAuxiliaryScorePanel : UnrealLuaPanel
    {
        /// <summary> 辅分 </summary>
        Text auxiliary;
        /// <summary> 目标金豆 </summary>
        Text mygoldpeas;
        /// <summary> 名下所有成员金豆 </summary>
        Text allgoldpeas;
        /// <summary> 推广员辅分预警 </summary>
        UnrealInputTextField earlywarning;
        /// <summary> 推广员辅分设置 </summary>
        UnrealInputTextField setting;
        [HideInInspector] public long userid;
        protected override void xinit()
        {
            auxiliary = this.transform.Find("Canvas").Find("top_all").Find("auxiliary").Find("Text").GetComponent<Text>();
            mygoldpeas = this.transform.Find("Canvas").Find("top_all").Find("mygoldpeas").Find("Text").GetComponent<Text>();
            allgoldpeas = this.transform.Find("Canvas").Find("top_all").Find("allgoldpeas").Find("Text").GetComponent<Text>();

            earlywarning = this.transform.Find("Canvas").Find("details").Find("earlywarning").Find("inputusersid").GetComponent<UnrealInputTextField>();
            setting = this.transform.Find("Canvas").Find("details").Find("setting").Find("inputusersid").GetComponent<UnrealInputTextField>();

        }

        long fufen;
        long warning;
        protected override void xrefresh()
        {
            earlywarning.text = warning/100+"";
            setting.text = fufen/100+"";
        }

        public void setData(long money, long task, long totalMoney, long totalTask,long fufen,long warning)
        {
            auxiliary.text = MathKit.getRound2Long(money + totalMoney) + "";
            mygoldpeas.text = MathKit.getRound2Long(money) + "";
            allgoldpeas.text = "" + MathKit.getRound2Long(totalMoney);
            this.fufen = fufen;
            this.warning = warning;
        }

        /// <summary> 获取辅分预警值 </summary>
        public long getearlywarningtext()
        {
            if (earlywarning.text == null || earlywarning.text == "")
            {
                return 0;
            }
            return long.Parse(earlywarning.text) * 100;
        }
        /// <summary> 获取辅分 </summary>
        public long getsettingtext()
        {
            if (setting.text == null || setting.text == "")
            {
                return 0;
            }
            return long.Parse(setting.text) * 100;
        }
       
    }
}