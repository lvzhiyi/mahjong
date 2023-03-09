using UnityEngine.UI;

namespace basef.arena
{
    /// <summary> 按钮类型 </summary>
    public class ArenaExchangeTypeBtnBar : IdBar
    {
        Text normal_text;
        Text active_text;

        protected override void xinit()
        {
            base.xinit();
            this.normal_text = this.transform.Find("text").GetComponent<Text>();
            this.active_text = this.transform.Find("text_1").GetComponent<Text>();
        }

        public void setTitle(string title)
        {
            this.normal_text.text = title;
            this.active_text.text = title;
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
