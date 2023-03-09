using UnityEngine;
using UnityEngine.UI;

namespace basef.rank
{
    /// <summary>
    /// 金币排行榜时间Bar
    /// </summary>
    public class GoldRankDayBar:IdBar
    {
        Text normal_text;

        Text active_text;

        [HideInInspector] public int type;

        protected override void xinit()
        {
            base.xinit();
            this.normal_text = this.transform.Find("text").GetComponent<Text>();
            this.active_text = this.transform.Find("text_1").GetComponent<Text>();
        }

        public void setTitle(string title,int type)
        {
            this.normal_text.text = title;
            this.active_text.text = title;
            this.type = type;
            setId(index_space);
        }

        public override void setState(bool state)
        {
            base.setState(state);
            this.normal_text.gameObject.SetActive(!state);
            this.active_text.gameObject.SetActive(state);
        }
    }
}
